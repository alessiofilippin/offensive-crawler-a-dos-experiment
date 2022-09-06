using crawler.manager;
using crawler.spider;
using System;
using System.Collections.Generic;
using System.Text;

namespace crawler.menu
{
    class Menu
    {
        private static int menuChoice = 1;
        public static void StartMenu()
        {
            int menuChoice = 1;
            while (menuChoice != 0)
            {
                Console.WriteLine("--- INTERACTIVE MODE ---");
                Console.WriteLine("Select an Option from the Menu:");
                Console.WriteLine("1) CrawlUrl and print results in CSV.");
                Console.WriteLine("2) Mass Download. (bandwidth starvation)");
                Console.WriteLine("3) Bulk Call Url (DDoS)");
                Console.WriteLine("0) Exit");

                menuChoice = int.Parse(Console.ReadLine());
                Console.Clear();

                if(menuChoice == 1)
                { StartCrawlMenu(); }
                else if(menuChoice == 2)
                { StartBulkDownloadMenu(); }
                else if (menuChoice == 3)
                { StartBulkCallsMenu(); }
                
            }
        }

        public static void StartCrawlMenu()
        {
            string rootUrl;
            string keysInclude_input;
            string keysExclude_input;
            string depth_input;
            string[] keysInclude;
            string[] keysExclude;

            Console.WriteLine("Enter URL to Crawl..");
            rootUrl = Console.ReadLine();

            Console.WriteLine("Enter Keywords to include (comma-separated)..");
            keysInclude_input = Console.ReadLine();
            keysInclude = keysInclude_input.Split(",");

            Console.WriteLine("Enter Keywords to exclude (comma-separated)..");
            keysExclude_input = Console.ReadLine();
            keysExclude = keysExclude_input.Split(",");

            Console.WriteLine("Enter depth..");
            depth_input = Console.ReadLine();

            List<string> keywordsIncludeList = new List<string>();
            //string[] keys = { "mde", "zip", "marine" };
            //string[] keys = { "marinedataexchange", "mde" };
            //string[] keys = { };
            keywordsIncludeList.AddRange(keysInclude);

            List<string> keywordsExcludeList = new List<string>();
            //string[] keysE = { "facebook", "instagram", "twitter", "linkedin", "google", "youtube", "apple" };
            keywordsExcludeList.AddRange(keysExclude);

            Crawler spider = new Crawler(rootUrl, int.Parse(depth_input), keywordsIncludeList, keywordsExcludeList);

            spider.StartRootUrlCrawl();

            Manager.UrlsAnalysis();

            Manager.PrintResultsToCSV();

            Console.WriteLine("-- Press a key to continue --");
            Console.ReadKey();
            Console.Clear();
        }

        public static void StartBulkDownloadMenu()
        {
            string rootUrl;
            string threads;

            Console.WriteLine("Enter URL to download (it should point to a file)..");
            rootUrl = Console.ReadLine();

            Console.WriteLine("Number of parallel threads for the download (More threads will consume more CPU)..");
            threads = Console.ReadLine();

            Manager.StartDownload(rootUrl, threads);

            Console.WriteLine("-- Press a key to continue --");
            Console.ReadKey();
            Console.Clear();
        }

        public static void StartBulkCallsMenu()
        {
            string rootUrl;
            string threads;
            string duration;

            Console.WriteLine("Enter URL to call..");
            rootUrl = Console.ReadLine();

            Console.WriteLine("Number of parallel threads for the calls, similar to virtual users (More threads will consume more CPU)..");
            threads = Console.ReadLine();

            Console.WriteLine("Duration in seconds of test, each thread will keep opening the link till the end of the duration..");
            duration = Console.ReadLine();

            Manager.StartBulkCall(rootUrl, threads, duration);

            Console.WriteLine("-- Press a key to continue --");
            Console.ReadKey();
            Console.Clear();
        }
    }
    
}
