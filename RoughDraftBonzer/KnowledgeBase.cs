using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace RoughDraftBonzer
{
    class KnowledgeBase
    {
        private Dictionary<string, string> knowledgeBase;

        public KnowledgeBase()
        {
            knowledgeBase = new Dictionary<string, string>();
            string connectionString = "SERVER=localhost;DATABASE=sakila;UID=root;PASSWORD=iLovekendra1234;";
            using (MySqlConnection connection = new MySqlConnection()) ;

        }
    }
}
