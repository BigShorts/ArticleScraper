using ArticleScraper;
using Newtonsoft.Json.Linq;
using SmartReader;

File.Delete("articles.db");
Console.WriteLine("Deleting database");

string fileContent = File.ReadAllText("rss.json");
JObject json = JObject.Parse(fileContent);
var feeds = json["Feeds"];

foreach (var item in feeds)
{
    string name = item["Name"].Value<string>();
    bool proxy = item["Proxy"].Value<bool>();
    JArray rsses = item["URLs"].Value<JArray>();

    Console.WriteLine("---------------------");
    Console.WriteLine($"Crawling {name}.");
    Console.Write("---------------------");
    foreach (var rss in rsses)
    {
        Console.Write($"\n{rss}");
        string[] urls = LinkCrawler.GetUrlsFromRSS(rss.ToString(), proxy);
        Article[] articles = ArticleMaker.MakeArticlesFromLinks(urls);
        Database.WriteArticlesToDatabase(articles);
    }
    Console.WriteLine();
}