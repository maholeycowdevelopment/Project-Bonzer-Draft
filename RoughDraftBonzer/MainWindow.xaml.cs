using System;
using System.Globalization;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Media;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace RoughDraftBonzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SpeechRecognitionEngine speechRecognizer = new SpeechRecognitionEngine();
        private SpeechSynthesizer synth = new SpeechSynthesizer();
        private string databaseAccessInfo = "SERVER=localhost;DATABASE=sakila;UID=root;PASSWORD=iLovekendra1234;";

        public MainWindow()
        {
            InitializeComponent();
            speechRecognizer.SpeechRecognized += speechRecognizer_SpeechRecognized;
            synth.SelectVoiceByHints(VoiceGender.Male, VoiceAge.Child);
            synth.SetOutputToDefaultAudioDevice();
            synth.Speak("Whats up and welcome to project Bonzer, my name is Darrell and I will be helping you today.");

            DBInfoRetrieval tableInfo = new DBInfoRetrieval();
            tableInfo.PrintTableNames();
            tableInfo.PrintColumnsAndTables();

           // LinguisticAnalyzer myAnalyzer = new LinguisticAnalyzer();
            //myAnalyzer.DemoParse();

            GrammarBuilder grammarBuilder = new GrammarBuilder();
            Choices commandChoices = new Choices("show all", "list the", "list all", "find me", "print the");
            grammarBuilder.Append(commandChoices);

            Choices valueChoices = new Choices();
            AddChoices(valueChoices, tableInfo.ReturnTableNames());
            //valueChoices.Add("actors", "films", "stores");
            valueChoices.Add("and", "there", "their", "with");
            grammarBuilder.Append(valueChoices);

            speechRecognizer.LoadGrammar(new Grammar(grammarBuilder));
            speechRecognizer.SetInputToDefaultAudioDevice();
        }

        private void AddChoices(Choices userChoices, List<string> tableNames)
        {
            foreach(string s in tableNames)
            {
                userChoices.Add(s);
            }
        }

        private void btnToggleListening_Click(object sender, RoutedEventArgs e)
        {
            if (btnToggleListening.IsChecked == true)
            {
                synth.Speak("What would you like to know Doctor Jamil?");
                speechRecognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
            else
            {
                synth.Speak("Talk to you later, give Patrick an A");
                speechRecognizer.RecognizeAsyncStop();
            }
        }

        private void speechRecognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            LinguisticAnalyzer myAnalyzer = new LinguisticAnalyzer();
            myAnalyzer.ProcessText(e.Result.Text);
            lblDemo.Content = e.Result.Text;
            if (e.Result.Words.Count == 6)
            {
                DataTable dtOutB = new DataTable();
                string commandOneB = e.Result.Words[0].Text.ToLower();
                string commandTwoB = e.Result.Words[1].Text.ToLower();
                string commandThreeB = e.Result.Words[2].Text.ToLower();
                string commandSixB = e.Result.Words[5].Text.ToLower();
                if ((commandOneB.Equals("show") || commandOneB.Equals("list"))
                    && (commandTwoB.Equals("all") || commandTwoB.Equals("the")))
                //&& (commandThree.Equals("actors") || commandThree.Equals("films")))
                {
                    string connectionString = databaseAccessInfo;
                    MySqlConnection connection = new MySqlConnection(connectionString);
                    MySqlCommand cmd = new MySqlCommand($"select * from {commandThreeB} natural join {commandSixB};", connection);
                    connection.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    connection.Close();
                    dtGrid.DataContext = dt;
                    dtOutB = dt;
                    PopulateDataGrid(dt);
                }
            } else
            if (e.Result.Words.Count == 3)
            {
                DataTable dtOut = new DataTable();
                string commandOne = e.Result.Words[0].Text.ToLower();
                string commandTwo = e.Result.Words[1].Text.ToLower();
                string commandThree = e.Result.Words[2].Text.ToLower();
                if ((commandOne.Equals("show") || commandOne.Equals("list"))
                    && (commandTwo.Equals("all") || commandTwo.Equals("the")))
                    //&& (commandThree.Equals("actors") || commandThree.Equals("films")))
                {
                    string connectionString = databaseAccessInfo;
                    MySqlConnection connection = new MySqlConnection(connectionString);
                    MySqlCommand cmd = new MySqlCommand($"select * from {commandThree};", connection);
                    connection.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    connection.Close();
                    dtGrid.DataContext = dt;
                    dtOut = dt;
                    PopulateDataGrid(dt);
                }
                    //SayResults(dtOut);
            }
            //lblDemo.Content = e.Result.Text;
            //DataTable dtOut = new DataTable();
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            speechRecognizer.Dispose();
        }

        private void SayResults(DataTable table)
        {
            for(int i = 0; i<3; i++)//foreach(DataRow row in table.Rows) 
            {
                DataRow row = table.Rows[i];
                System.Threading.Thread.Sleep(500);
                foreach (DataColumn column in table.Columns)
                {
                    synth.Speak(Convert.ToString(row[column]));
                    System.Threading.Thread.Sleep(200);
                }
            }
        }

        private void PopulateDataGrid(DataTable table)
        {
            dtGrid.DataContext = table;
        }
    }
}
