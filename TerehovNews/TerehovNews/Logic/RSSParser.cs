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
        private static SyndicationFeed feeds = new SyndicationFeed();

        private const string Chanel = "http://lenta.ru/rss/news";

        public static List<SampleDataItem> GetListNews()
        {
            return _listNews;
        }

        public static async Task Update()
        {
            feeds = await new SyndicationClient().RetrieveFeedAsync(new Uri(Chanel));
            
        }

        public static void UpdateList()
        {
            foreach (var i in  feeds.Items)
            {
                var a = i.ToString();
                var uniqid = Windows.Data.Html.HtmlUtilities.ConvertToText(i.Id);
                var title = Windows.Data.Html.HtmlUtilities.ConvertToText(i.Title.Text);
                var summary = Windows.Data.Html.HtmlUtilities.ConvertToText(i.Summary.Text);
                var subtittle = Windows.Data.Html.HtmlUtilities.ConvertToText(i.PublishedDate.ToString());
                
                
                
                

                var feed = new SampleDataItem(uniqid, title, summary, subtittle);

                _listNews.Add(feed);
            }
            
        }
    }
}


