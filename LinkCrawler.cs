using AngleSharp.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace ArticleScraper
{
    internal class LinkCrawler
    {
        public static string[] GetUrlsFromRSS(string url, bool usingProxy=false)
        {
            List<string> urls = new List<string>();

            try
            {
                XmlReader reader = XmlReader.Create(url);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                reader.Close();

                foreach (SyndicationItem item in feed.Items)
                {
                    var links = item.Links;
                    foreach (var link in links)
                    {
                        string ogString = link.Uri.OriginalString;
                        if (usingProxy)
                        {
                            ogString = $"http://127.0.0.1:5000/{ogString}";
                        }
                        if (!urls.Contains(ogString))
                        {
                            urls.Add(ogString);
                        }
                    }
                }

                return urls.ToArray();
            }
            catch (HttpRequestException)
            {
                Console.Write(" | Recieved HttpRequestException, maybe bad RSS feed?");
                return urls.ToArray();
            }
        }
    }
}
