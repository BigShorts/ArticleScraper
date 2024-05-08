using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartReader;

namespace ArticleScraper
{
    internal class Database
    {
        private static void MakeDatabase()
        {
            SQLiteConnection.CreateFile("articles.db");

            using (SQLiteConnection connection = new SQLiteConnection("Data Source=articles.db"))
            {
                string query = @"CREATE TABLE ""Articles"" (
	                        ""id""	INTEGER NOT NULL,
	                        ""title""	TEXT,
	                        ""link""	TEXT,
	                        ""textContent""	TEXT,
	                        PRIMARY KEY(""id"" AUTOINCREMENT)
                            );";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {

                    connection.Open();
                    int result = command.ExecuteNonQuery();


                    if (result < 0) Console.WriteLine("Error making table");
                }
            }
        }

        public static void WriteArticleToDatabase(Article article)
        {
            if (!File.Exists("articles.db"))
            {
                Console.WriteLine("Database not found, creating");
                MakeDatabase();
            }

            var title = article.Title;
            var textContent = article.TextContent;
            var link = article.Uri.OriginalString;

            if (link.Contains(@"http://127.0.0.1:5000/"))
            {
                link = link.Replace(@"http://127.0.0.1:5000/", "");
            }
            
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=articles.db"))
            {
                string query = @"INSERT INTO Articles (title, link, textContent) VALUES ($title, $link, $textContent);";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("$title", title);
                    command.Parameters.AddWithValue("$link", link);
                    command.Parameters.AddWithValue("$textContent", textContent);


                    connection.Open();
                    int result = command.ExecuteNonQuery();


                    if (result < 0) Console.WriteLine("Error inserting");
                }
            }
        }
    }
}
