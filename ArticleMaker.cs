using SmartReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleScraper
{
    internal class ArticleMaker
    {
        public static Article[] MakeArticlesFromLinks(string[] links)
        {
            List<Article> articles = new();
            foreach (string link in links)
            {
                var article = MakeArticle(link);
                if (article != null)
                {
                    articles.Add(article);
                }
            }
            return articles.ToArray();
        }

        private static Article? MakeArticle(string link)
        {
            var sr = new Reader(link);
            var article = sr.GetArticle();

            if (article.IsReadable)
            {
                Console.Write($"\n\t{link}");
                return article;
            }
            Console.Write(" | There was an error making this article.");
            return null;
        }
    }
}
