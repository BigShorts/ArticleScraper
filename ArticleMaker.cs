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
        public SmartReader.Article[] MakeArticlesFromLinks(string[] links)
        {
            List<SmartReader.Article> articles = new();
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

        private Article? MakeArticle(string link)
        {
            var sr = new SmartReader.Reader(link);
            var article = sr.GetArticle();

            if (article.IsReadable)
            {
                return article;
            }
            return null;
        }
    }
}
