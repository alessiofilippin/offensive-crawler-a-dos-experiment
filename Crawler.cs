using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using crawler.manager;
using HtmlAgilityPack;

namespace crawler.spider
{
    class Crawler
    {
        private int maxDepth;
        private string rootUrl;
        private bool depthReached = false;
        private List<string> keyWordsToInclude;
        private List<string> keyWordsToExclude;

        public Crawler(string _url)
        {
            rootUrl = _url;
            maxDepth = 10;
            keyWordsToInclude = null;
            keyWordsToExclude = null;
        }

        public Crawler(string _url, int depth)
        {
            rootUrl = _url;
            maxDepth = depth;
            keyWordsToInclude = null;            
            keyWordsToExclude = null;
        }

        public Crawler(string _url, int depth, List<string> _keywordsToInclude, List<string> _keywordsToExclude)
        {
            rootUrl = _url;
            maxDepth = depth;
            keyWordsToInclude = _keywordsToInclude;
            keyWordsToExclude = _keywordsToExclude;
        }

        public void StartRootUrlCrawl()
        {
            CrawlUrl(rootUrl);
            while (Manager.ListLenght() > 0)
            {
                CrawlUrl(Manager.GetNextUrl());
            }

            Console.WriteLine("CrawlQueue: " + Manager.GetCrawlList().Count);
            Console.WriteLine("PerformanceQueue: " + Manager.GetReqDurationList().Count);
        }

        private bool IsUrlValid(string url)
        {
            if (Manager.IsUrlAlreadyInList(url))
                return false;

            if (keyWordsToExclude.Count > 0)
            {
                bool isKeyFound = false;
                foreach (string key in keyWordsToExclude)
                {
                    if (url.Contains(key))
                    {
                        isKeyFound = true; break;
                    }
                }

                if (isKeyFound)
                    return false;
            }

            if (keyWordsToInclude.Count > 0)
            {
                bool isKeyFound = false;
                foreach (string key in keyWordsToInclude)
                {
                    if (url.Contains(key))
                    {
                        isKeyFound = true; break;
                    }
                }

                if (!isKeyFound)
                    return false;
                else
                    return true;
            }
            else
                return true;
        }

        private void CrawlUrl(string url)
        {
            if (!IsUrlValid(url))
                return;

            var web = new HtmlWeb();
            Console.WriteLine("Crawling..." + url);
            var doc = new HtmlDocument();
            try
            {
                doc = web.Load(url);

            }
            catch
            {}

            //Check new Urls differnt from rootUrl
            var linkTags = doc.DocumentNode.Descendants("link");
            var linkedPages = doc.DocumentNode.Descendants("a")
                                              .Select(a => a.GetAttributeValue("href", null))
                                              .Where(u => !String.IsNullOrEmpty(u))
                                              .Where(u => u.StartsWith("http"));

            foreach (string e in linkedPages)
            {
                if (IsUrlValid(e))
                {
                    if (Manager.ListLenght() >= maxDepth)
                        depthReached = true;

                    if (!depthReached)
                        Manager.AddNewUrl(e);
                }
            }


            //Check new path from domainUrl --> domainUrl/SomethingElse
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            var linkTagsDomainUrl = doc.DocumentNode.Descendants("link");
            var linkedPagesDomainUrl = doc.DocumentNode.Descendants("a")
                                              .Select(a => a.GetAttributeValue("href", null))
                                              .Where(u => !String.IsNullOrEmpty(u))
                                              .Where(u => u.StartsWith("/"));

            foreach (string e in linkedPagesDomainUrl)
            {
                string newUrl = "https://" + request.Host + e;
                if (IsUrlValid(newUrl))
                {
                    if (Manager.ListLenght() >= maxDepth)
                        depthReached = true;

                    if (!depthReached)
                        Manager.AddNewUrl(newUrl);
                }

            }

            //Check new path from rootUrl --> RootUrl/SomethingElse
            var linkTagsCompleteUrl = doc.DocumentNode.Descendants("link");
            var linkedPagesCompleteUrl = doc.DocumentNode.Descendants("a")
                                              .Select(a => a.GetAttributeValue("href", null))
                                              .Where(u => !String.IsNullOrEmpty(u))
                                              .Where(u => u.StartsWith("/"));

            foreach (string e in linkedPagesCompleteUrl)
            {
                string newUrl = rootUrl + e;
                if (IsUrlValid(newUrl))
                {
                    if (Manager.ListLenght() >= maxDepth)
                        depthReached = true;

                    if (!depthReached)
                        Manager.AddNewUrl(newUrl);
                }
            }
        }
    }
}
