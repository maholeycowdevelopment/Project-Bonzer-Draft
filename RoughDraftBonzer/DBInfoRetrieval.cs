using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoughDraftBonzer
{
    class DBInfoRetrieval
    {
        List<string> tableNames = new List<string>();
        Dictionary<string, List<string>> attributeMap = new Dictionary<string, List<string>>();
        string connectionString = "SERVER=localhost;DATABASE=sakila;UID=root;PASSWORD=iLovekendra1234;";

        public DBInfoRetrieval()
        {
            GetTableNames();
            GetTableColumns();
        }

        private void GetTableNames()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "show tables;";
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tableNames.Add(reader.GetString(0));
                    }
                }
                connection.Close();
            }
        }

        private void GetTableColumns()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query;
                foreach (string tName in tableNames)
                {
                    if (tName.Equals("actor_info") || tName.Equals("film_list")
                        || tName.Equals("nicer_but_slower_film_list"))
                        continue;
                    query = "select * from " + tName + ";";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        for(int i = 0; i < reader.FieldCount; i++)
                        {
                            if (!attributeMap.ContainsKey(reader.GetName(i)))
                            {
                                List<string> newList = new List<string>();
                                newList.Add(tName);
                                attributeMap.Add(reader.GetName(i), newList);
                            } 
                            else
                            {
                                List<string> currentList = new List<string>();
                                attributeMap.TryGetValue(reader.GetName(i), out currentList);
                                currentList.Add(tName);
                            }
                        }
                    }
                    connection.Close();
                }
            }
        }

        public void PrintTableNames()
        {
            foreach(string s in tableNames)
            {
                Console.WriteLine(s);
            }
        }

        public void PrintColumnsAndTables()
        {
            foreach (KeyValuePair<string, List<string>> kvp in attributeMap)
            {
                List<string> currList = new List<string>();
                attributeMap.TryGetValue(kvp.Key, out currList);
                Console.Write("Attribute = {0}, Table Name(s) = ", kvp.Key);
                foreach(string s in currList)
                {
                    Console.Write(s + ", ");
                }
                Console.WriteLine();
            }
        }

        public List<string> ReturnTableNames()
        {
            return tableNames;
        }

        public Dictionary<string, List<string>> ReturnAttributeMap()
        {
            return attributeMap;
        }
    }
}
