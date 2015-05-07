using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace TerehovNews.Logic
{
    public static class HTMLParser
    {
        public static string GetText(string url)
        {
            WebRequest myWebRequest = WebRequest.Create(url);
            Task<WebResponse> myWebResponse = myWebRequest.GetResponseAsync();
            myWebResponse.Wait();
            Stream ReceiveStream = myWebResponse.Result.GetResponseStream();
            Encoding encode = Encoding.GetEncoding("utf-8");
            TextReader reader = new StreamReader(ReceiveStream, encode);
            var a =  reader.ReadToEnd();
            var preFinal = a.Substring(a.IndexOf("articleBody")+13, a.Length - a.IndexOf("articleBody")-13);
            var final = preFinal.Substring(0, preFinal.IndexOf("</div>"));
            final = final.Replace("<p>", "");
            final = final.Replace("</p>", "");
            var forDelete = final.Substring(final.IndexOf("<"), final.Length - final.IndexOf(">")-1);
            final = final.Replace(forDelete, "");
            return final;


        }
    }
}
