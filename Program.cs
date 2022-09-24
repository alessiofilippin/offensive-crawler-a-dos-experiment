using crawler.manager;
using crawler.menu;
using crawler.spider;
using System;
using System.Collections.Generic;

namespace crawler
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
                Menu.StartMenu();
            else
            {
                Console.WriteLine("--- CLI Mode ---");

                if(args[0] == "BulkDownload")
                {
                    Manager.StartDownload(args[1], args[2]);
                }

                if (args[0] == "BulkCall")
                {
                    if(args.Length == 5)
                        Manager.StartBulkCall(args[1], args[2], args[3], args[4]);
                    else
                        Manager.StartBulkCall(args[1], args[2], args[3]);
                }

                if (args[0] == "help" || args[0] == "/h")
                {
                    Console.WriteLine(" To run in Interactive mode, RUN WITHOUT ARGUMENTS. Ex. .\\crawler.exe");
                    Console.WriteLine(" To run in CLI mode, RUN WITH ARGUMENTS.");
                    Console.WriteLine(" CLI Bulk Download -> .\\crawler.exe BulkDownload [URL] [Number of Threads]");
                    Console.WriteLine(" CLI Bulk Download -> .\\crawler.exe BulkDownload https://myurl.com 3");
                    Console.WriteLine(" CLI Bulk Download -> .\\crawler.exe BulkCall [URL] [Number of Threads] [DURATION]");
                    Console.WriteLine(" CLI Bulk Calls    -> .\\crawler.exe BulkCall https://myurl.com 3 60");
                }

            }
        }
    }
}