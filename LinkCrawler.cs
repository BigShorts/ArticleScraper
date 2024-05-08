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
        public static string[] GetUrlsFromRSS(string url)
        {
            List<string> urls = new List<string>();

            XmlReader reader = XmlReader.Create(url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            foreach(SyndicationItem item in feed.Items)
            {
                var links = item.Links;
                foreach(var link in links)
                {
                    urls.Add(link.Uri.OriginalString);
                }
            }

            return urls.ToArray();
        }
    }
}
