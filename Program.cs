using ArticleScraper;
using SmartReader;

string[] links = LinkCrawler.GetUrlsFromRSS("https://rss.nytimes.com/services/xml/rss/nyt/Movies.xml", true);
Article[] articles = ArticleMaker.MakeArticlesFromLinks(links);
foreach (Article article in articles)
{
    if (article != null)
    {
        Database.WriteArticleToDatabase(article);
    }
}