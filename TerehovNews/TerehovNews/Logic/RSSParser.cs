using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;
using TerehovNews.Data;

namespace TerehovNews.Logic
{
    public static class RSSParser
    {
        private static List<SampleDataItem> _listNews = new List<SampleDataItem>();
        private static SyndicationFeed feedsLenta = new SyndicationFeed();

        private const string Lenta = "http://lenta.ru/rss/news";

        public static List<SampleDataItem> GetListNews()
        {
            return _listNews;
        }

        public static async Task Update()
        {
            feedsLenta = await new SyndicationClient().RetrieveFeedAsync(new Uri(Lenta));
            
        }

        public static void UpdateList()
        {
            foreach (var i in  feedsLenta.Items)
            {
                var xml = i.GetXmlDocument(feedsLenta.SourceFormat);
                var uniqid = Windows.Data.Html.HtmlUtilities.ConvertToText(i.Id);
                var title = Windows.Data.Html.HtmlUtilities.ConvertToText(i.Title.Text);
                var summary = Windows.Data.Html.HtmlUtilities.ConvertToText(i.Summary.Text);
                var subtittle = Windows.Data.Html.HtmlUtilities.ConvertToText(i.PublishedDate.ToString());
                
                var xmlfirst = xml.GetElementsByTagName("category");
                string category;
                if (xmlfirst.Length > 0)
                {
                    category = xmlfirst[0].InnerText;
                }
                else
                {
                    category = "";
                }
                
                
                

                var feed = new SampleDataItem(uniqid, title, summary, subtittle, category);

                _listNews.Add(feed);
            }
            
        }
    }
}


