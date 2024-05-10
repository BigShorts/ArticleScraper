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
            string fileName = link.Split(@"/").Last();
            //Console.WriteLine(fileName);
            if((fileName.Contains(".") && !fileName.EndsWith(".html")) || fileName.StartsWith("https://s3.amazonaws.com"))
            {
                // Not html, BOO GET THE FUCK OUT HERE FUCK YOU NVIDIA
                return null;
            }
            var sr = new Reader(link);
            try
            {
                var article = sr.GetArticle();
                if (article.IsReadable)
                {
                    Console.Write($"\n\t{link}");
                    return article;
                }
                Console.Write(" | There was an error making this article.");
            } catch (HttpRequestException e)
            {
                Console.Write($" | {e.Message}");
            }
            
            return null;
        }
    }
}
