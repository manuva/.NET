using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Xml.Linq;


namespace TweetParser
{
    public class twitterScrape
    {
        public static List<TweetInfo> GetSearchResults(string searchString)
        {
            using (WebClient web = new WebClient())
            {
                //store the search string in url var
                //change rpp to 100 or less to display the amount of results
                string url = string.Format("http://search.twitter.com/search.atom?lang=en&q={0}&rpp=100", HttpUtility.UrlEncode(searchString));

                WebClient client = new WebClient();

                //Pretend to be Google Chrome
                //Pretending to be a browser instead of an app seems to make twitter respond faster
                client.Headers.Add("Accept: application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5");
                client.Headers.Add("Accept-Charset: ISO-8859-1,utf-8;q=0.7,*;q=0.3");
                client.Headers.Add("Accept-Language: en-US,en;q=0.8");
                client.Headers.Add("User-Agent: Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/534.16 (KHTML, like Gecko) Chrome/10.0.648.204 Safari/534.16");

                XDocument doc = XDocument.Load(url);

                XNamespace ns = "http://www.w3.org/2005/Atom";

                List<TweetInfo> tweets = (from item in doc.Descendants(ns + "entry")
                                      select new TweetInfo
                                      {
                                          Id = item.Element(ns + "id").Value,
                                          Published = DateTime.Parse(item.Element(ns + "published").Value),
                                          Title = item.Element(ns + "title").Value,
                                          Content = item.Element(ns + "content").Value,
                                          Link = (from XElement x in item.Descendants(ns + "link")
                                                  where x.Attribute("type").Value == "text/html"
                                                  select x.Attribute("href").Value).First(),
                                          Image = (from XElement x in item.Descendants(ns + "link")
                                                   where x.Attribute("type").Value == "image/png"
                                                   select x.Attribute("href").Value).First(),
                                          Author = new Author()
                                          {
                                              Name = item.Element(ns + "author").Element(ns + "name").Value,
                                              Uri = item.Element(ns + "author").Element(ns + "uri").Value
                                          }

                                      }).ToList();

                return tweets;
            }
        }
    }
}
