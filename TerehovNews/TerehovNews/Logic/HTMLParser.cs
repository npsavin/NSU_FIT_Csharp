using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using xNet.Text;

namespace TerehovNews.Logic
{
    public static class HTMLParser
    {
        public static string GetText(string url)
        {
            WebRequest myWebRequest = WebRequest.Create(url);
            Task<WebResponse> myWebResponse = myWebRequest.GetResponseAsync();
            myWebResponse.Wait();
            Stream receiveStream = myWebResponse.Result.GetResponseStream();
            Encoding encode = Encoding.GetEncoding("utf-8");
            TextReader reader = new StreamReader(receiveStream, encode);
            var a =  reader.ReadToEnd();
            //var first = new Regex(@"\barticleBody\b[^]*\bb-topic__socials\b", RegexOptions.IgnoreCase);
            //var firstResult = first.Match(a);
            var second = new Regex(@"<p\b[^>]*>(.*?)</p>", RegexOptions.IgnoreCase);
            var finalResult = second.Match(a);
            StringBuilder sb = new StringBuilder();
            var hu = new Regex(@"<\b[^>]*>(.*?)>", RegexOptions.IgnoreCase);
            foreach (Match match in second.Matches(a))
            {
                sb.AppendLine(match.Groups[1].Value);
            }
            var PreFinal = sb.ToString();
            sb.Clear();
            foreach (Match el in hu.Matches(PreFinal))
            {
                PreFinal.Replace(el.Groups[1].Value, "");
            }

            //var finalResult = secondResult.ToString().Substrings("<p>", "</p>");
            //StringBuilder sb = new StringBuilder();
            //foreach (var el in finalResult)
            //{
            //    sb.AppendLine(el);
            //}
            return PreFinal;
            //var preFinal = htmnlPage.Substrings("<div class=\"b-text clearfix\" itemprop=\"articleBody\">",
            //    "<section class=\"b-topic__socials\"><div class=\"b-socials\" id=\"socials\">");
            //var textNews = htmnlPage.Substrings("<p>", "</p>");
            //var sb = new StringBuilder();
            //foreach (var el in textNews)
            //{
            //    sb.AppendLine(el);
                
            //}
            //var preFinal = a.Substring(a.IndexOf("articleBody") + 13, a.Length - a.IndexOf("articleBody") - 13);
            //var final = preFinal.Substring(0, preFinal.IndexOf("</div>"));
            //final = final.Replace("<p>", "");
            //final = final.Replace("</p>", "");
            //var forDelete = final.Substring(final.IndexOf("<"), final.Length - (final.IndexOf(">") - final.IndexOf("<")));
            //final = final.Replace(forDelete, "");
            //var foDelete = sb.ToString().Substrings("<", ">");
            //var FinalText = sb.ToString();
        }
    }
}
