# Project-Bonzer-Draft

NLI to Database, Research based project

# H1 SYSTEM PREQUISITES:

1. Must have Visual Studio 2017 installed, or Visual Studio for Mac.
2. Must have input and output (Your standard input device for your computer and speakers should do just find, no headphones, mics, etc. are needed)

# H1 STEPS FOR SET UP:

1. Download the project
2. Go to https://sergey-tihon.github.io/Stanford.NLP.NET/StanfordParser.html, and follow the instructions found here.
3. Install the MySQL sample database called 'sakila'. To get started, go here https://dev.mysql.com/doc/sakila/en/
4. Open up the project by clicking on the .sln file
5. Go to this sight, https://www.rhyous.com/2014/10/20/splitting-sentences-in-c-using-stanford-nlp/, and please follow the instructions found in steps 5-13
6. Open the file DBInfoRetrieval.cs, go to line 16, and configure the connection string with the appropriate parameters. Chances are, you just need to put in the appropriate password for the database access.
7. Open the file MainWindow.xaml.cs, go to lines 21, and repeat what you did in step 6.
8. Congratulations, you should now be able to run the code!

# H1 HOW TO USE:

1. Build and run the project
2. The program will greet you, after this, click on the button 'Listen'
3. Supports NLI such as "Show all ###" or "List the ###" where ### is a name of one of the tables in the database.
4. Supports NLI such as "Show all ### and their ###" where ### is again a name of the tables in the database. Here you can see a natural join occuring.
5. Future support will be continually added, and updated here.
