using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using crawler.datastructure;
using DnsClient;

namespace crawler.manager
{
    class Manager
    {
        private static List<string> crawlList = new List<string>();
        private static List<CrawlerResult> reqDurationList = new List<CrawlerResult>();
        private static List<string> crawledList = new List<string>();

        public static List<string> GetCrawlList()
        {
            return crawlList;
        }

        public static List<CrawlerResult> GetReqDurationList()
        {
            return reqDurationList;
        }


        public static bool AddNewUrl(string _url)
        {
            bool isFound = false;
            foreach(CrawlerResult cRes in reqDurationList)
            {
                if(cRes.GetUri() == _url)
                {
                    isFound = true; break;
                }
            }

            if(!isFound)
            { crawlList.Add(_url); reqDurationList.Add(new CrawlerResult(_url)); return true; }

            return false;
        }

        public static int ListLenght()
        {
            return crawlList.Count;
        }

        public static string GetNextUrl()
        {
            string nextUrl = crawlList[0];
            crawledList.Add(nextUrl);
            crawlList.RemoveAt(0);
            return nextUrl;
        }

        public static void UrlsAnalysis()
        {
            if (reqDurationList.Count == 0)
                return;

            foreach(CrawlerResult cres in reqDurationList)
            {
                Console.WriteLine("TestPerf..." + cres.GetUri());
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(cres.GetUri());
                string errorMsg = "";
                string host = "";
                System.Diagnostics.Stopwatch timer = new Stopwatch();

                timer.Start();

                try {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    host = response.ResponseUri.Host;
                    response.Close();
                }
                catch(System.Net.WebException exc)
                {
                    errorMsg = exc.Message;
                }


                timer.Stop();

                TimeSpan timeTaken = timer.Elapsed;

                cres.SetResponseTime(timeTaken.TotalMilliseconds);

                if(errorMsg != "")
                { cres.SetInfo(errorMsg); }

                cres.SetCloudData(CheckCloudEnumDns(host));
            }
        }
        
        public static bool IsUrlAlreadyInList(string _url)
        {
            if (crawledList.Count == 0)
                return false;

            try
            {
                crawledList.Find(x => x == _url);
            }
            catch(ArgumentNullException exc)
            { return false; }

            return true;
        }

        public static string CheckCloudEnumDns(string url)
        {
            string data = "";
            var lookup = new LookupClient();
            var result = lookup.Query(url, QueryType.A);

            Console.WriteLine("CheckDNSInfo..." + url);
            foreach (DnsClient.Protocol.ARecord dnsARecord in result.Answers.ARecords())
            {
                data = data + " | A Records: " + dnsARecord.Address;
            }

            foreach (DnsClient.Protocol.CNameRecord dnsCnameRecord in result.Answers.CnameRecords())
            {
                if (dnsCnameRecord.CanonicalName.ToString().Contains("azure"))
                {
                    data = data + " | Possible Azure CNAME: " + dnsCnameRecord.CanonicalName;
                }

                if (dnsCnameRecord.CanonicalName.ToString().Contains("azureedge"))
                {
                    data = data + " | Endpoint might be behind a CDN! |";
                }

                if (dnsCnameRecord.CanonicalName.ToString().Contains("aws"))
                {
                    data = data + " | Possible AWS CNAME: " + dnsCnameRecord.CanonicalName;
                }
            }

            foreach (DnsClient.Protocol.MxRecord dnsMxRecord in result.Answers.MxRecords())
            {
                data = data + " | MX Records: " + dnsMxRecord.DomainName;
            }

            var record = result.Answers.ARecords().FirstOrDefault();
            var ip = record?.Address;

            return data;
        }

        private static void FileDownload(string _rootUrl, int _iterator)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(_rootUrl, Environment.CurrentDirectory + "/myFile" + _iterator + ".file");
            }
        }

        public static void StartDownload(string _rootUrl, string _threads)
        {
            List<Task> tasksList = new List<Task>();
            tasksList.Add(Task.Factory.StartNew(() => Parallel.For(0, int.Parse(_threads), (i, state) =>
            {
                Console.WriteLine("Start task number: " + i);
                FileDownload(_rootUrl, i);
            })));

            bool isFinished = false;
            while (isFinished != true)
            {
               Task.Factory.ContinueWhenAll(tasksList.ToArray(), Continue => isFinished = true);

                if (isFinished)
                    Console.WriteLine("All Threads Completed.");
            }
            
        }

        private static void CallUrl(string _rootUrl, string _duration, int _thread)
        {
            System.Diagnostics.Stopwatch timer = new Stopwatch();
            timer.Start();
            int i = 0;
            while (timer.ElapsedMilliseconds < (int.Parse(_duration) * 1000))
            {
                using (var client = new WebClient())
                {
                    i++;
                    long remainTime = (int.Parse(_duration) * 1000) - timer.ElapsedMilliseconds;
                    Console.WriteLine("Thread: " + _thread + " Call Number: " + i + " Remaining milliseconds: " + remainTime);
                    
                    try
                    {
                        string url = _rootUrl + "?random=" + i + DateTime.Now.Ticks;
                        Console.WriteLine("try " + url);
                        client.OpenRead(url);
                    }
                    catch
                    { }
                    Thread.Sleep(500);
                }
            }

            timer.Stop();
        }

        public static void StartBulkCall(string _rootUrl, string _threads, string _duration)
        {
            List<Task> tasksList = new List<Task>();
            tasksList.Add(Task.Factory.StartNew(() => Parallel.For(0, int.Parse(_threads), (i, state) =>
            {
                Console.WriteLine("Start task number: " + i);
                CallUrl(_rootUrl, _duration, i);
            })));

            bool isFinished = false;
            Task.Factory.ContinueWhenAll(tasksList.ToArray(), Continue => isFinished = true);
            while (isFinished != true)
            {
                if(isFinished)
                    Console.WriteLine("All Threads Completed.");
            }

        }        

        public static void PrintResultsToCSV()
        {
            //before your loop
            var csv = new StringBuilder();

            foreach (CrawlerResult cres in reqDurationList)
            {
                //in your loop
                var first = cres.GetUri();
                var second = cres.GetResponseTime().ToString();
                var third = cres.GetInfo();
                var fourth = cres.GetCloudData();
                var newLine = string.Format("{0},{1},{2},{3}", first, second, third, fourth);
                csv.AppendLine(newLine);
            }

            //after your loop
            string filePath = Environment.CurrentDirectory + "\\export.csv";
            File.WriteAllText(filePath, csv.ToString());
            Console.WriteLine("Report generated At " + filePath);
        }
        
    }
}
