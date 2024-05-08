using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleScraper
{
    internal class Database
    {
        public static void WriteToDatabase(SmartReader.Article article)
        {
            var title = article.Title;
            var textContent = article.TextContent;

            using (var connection = new SqliteConnection("Data Source=articles.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                    @"
                        INSERT INTO Articles
                        VALUES ($title, $textContent);
                    ";

            }
        }
    }
}
