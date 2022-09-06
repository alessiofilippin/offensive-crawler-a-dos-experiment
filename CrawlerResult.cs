using System;
using System.Collections.Generic;
using System.Text;

namespace crawler.datastructure
{
    class CrawlerResult
    {
        private string uri;
        private double responseTimeMs;
        private string info;
        private string cloudData;

        public CrawlerResult(string _uri)
        {
            uri = _uri;
            info = "";
        }        

        public string GetUri()
        {
            return uri;
        }

        public double GetResponseTime()
        {
            return responseTimeMs;
        }

        public string GetInfo()
        {
            return info;
        }

        public string GetCloudData()
        {
            return cloudData;
        }

        public void SetResponseTime(double _time)
        {
            responseTimeMs = _time;
        }

        public void SetInfo(string _info)
        {
            info = _info;
        }

        public void SetCloudData(string _cloudData)
        {
            cloudData = _cloudData;
        }
    }
}
