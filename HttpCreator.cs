using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Cache;
using System.Net.Http;
using System.Text;

namespace crawler.manager.httpcreator
{
    class HttpCreator
    {
        private static string[] UserAgentsList = {
        "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/534.3 (KHTML, like Gecko) BlackHawk/1.0.195.0 Chrome/127.0.0.1 Safari/62439616.534",
        "Mozilla/5.0 (Windows; U; Windows NT 5.2; en-US; rv:1.9.1.3) Gecko/20090824 Firefox/3.5.3 (.NET CLR 3.5.30729)",
        "Mozilla/5.0 (PlayStation 4 1.52) AppleWebKit/536.26 (KHTML, like Gecko)",
        "Mozilla/5.0 (Windows NT 6.1; rv:26.0) Gecko/20100101 Firefox/26.0 IceDragon/26.0.0.2",
        "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; InfoPath.2)",
        "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0; SLCC1; .NET CLR 2.0.50727; .NET CLR 1.1.4322; .NET CLR 3.5.30729; .NET CLR 3.0.30729)",
        "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.2; Win64; x64; Trident/4.0)",
        "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; SV1; .NET CLR 2.0.50727; InfoPath.2)",
        "Mozilla/5.0 (Windows; U; MSIE 7.0; Windows NT 6.0; en-US)",
        "Mozilla/4.0 (compatible; MSIE 6.1; Windows XP)",
        "Opera/9.80 (Windows NT 5.2; U; ru) Presto/2.5.22 Version/10.51",
        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36	51	Very common",
        "Mozilla/5.0 (Linux; Android 6.0; vivo 1610 Build/MMB29M) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.124 Mobile Safari/537.36	53	Very common",
        "Mozilla/5.0 (Linux; Android 6.0.1; SM-G532G Build/MMB29T) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.83 Mobile Safari/537.36	63	Very common",
        "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36	55	Very common",
        "Mozilla/5.0 (Linux; Android 5.1.1; vivo X7 Build/LMY47V; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/48.0.2564.116 Mobile Safari/537.36 baiduboxapp/8.6.5 (Baidu; P1 5.1.1)	48	Very common",
        "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Safari/537.36	62	Very common",
        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36	45	Very common",
        "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.65 Safari/537.36	43	Very common",
        "Mozilla/5.0 (Windows NT 6.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.112 Safari/537.36	49	Very common",
        "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36	41	Very common",
        "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.80 Safari/537.36	46	Very common",
        "Mozilla/5.0 (Windows NT 6.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.80 Safari/537.36	46	Very common",
        "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36	51	Very common",
        "Mozilla/5.0 (Windows NT 5.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.80 Safari/537.36	46	Common",
        "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36	50	Common",
        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.87 Safari/537.36	49	Common",
        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.31 (KHTML, like Gecko) Chrome/26.0.1410.64 Safari/537.31	26	Common",
        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36	47	Common",
        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36	54	Common",
        "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36	54	Common",
        "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.81 Safari/537.36	58	Common",
        "Mozilla/5.0 (Windows NT 6.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36	51	Common",
        "Mozilla/5.0 (Windows NT 5.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36	51	Common",
        "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.90 Safari/537.36	60	Common",
        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.110 Safari/537.36	49	Common",
        "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36	63	Common",
        "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.89 Safari/537.36	62	Common",
        "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36	56	Common",
        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36	46	Common",
        "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.87 Safari/537.36	49	Common",
        "Mozilla/5.0 (Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.89 Safari/537.36	62	Common",
        "Mozilla/5.0 (Windows NT 6.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.89 Safari/537.36	62	Common",
        "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.133 Safari/537.36	57	Common",
        "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36	65	Common",
        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36	48	Common",
        "Mozilla/5.0 (Windows NT 5.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.89 Safari/537.36	62	Common",
        "Mozilla/5.0 (Windows NT 6.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.109 Safari/537.36	48	Common",
        "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.109 Safari/537.36	48	Common",
        "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.90 Safari/537.36	42	Common",
        "Mozilla/5.0 (Windows NT 5.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.109 Safari/537.36	48	Common",
        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.133 Safari/537.36	57	Common",
        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36	50	Common",
        "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.65 Safari/537.36	43	Common",
        "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.63 Safari/537.36	51	Common",
        "Mozilla/5.0 (Windows NT 6.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.63 Safari/537.36	51	Common",
        "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36	52	Common",
        "Mozilla/5.0 (Windows NT 5.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.63 Safari/537.36	51	Common",
        "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.133 Safari/537.36	57	Common",
        "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36	56	Common",
        "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.186 Safari/537.36	64	Common",
        "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36	52	Common",
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.81 Safari/537.36	58	Common",
        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.85 Safari/537.36	45	Common",
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.167 Safari/537.36	64	Common",
        "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36	53	Common",
        "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36	54	Common",
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.78 Safari/537.36	60	Common",
        "Mozilla/5.0 (Linux; U) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.79 Safari/537.4	22	Common",
        "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.86 Safari/537.36	46	Common",
        "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2623.110 Safari/537.36	49	Common",
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36	52	Common",
        "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36	53	Common",
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.101 Safari/537.36	60	Common",
        "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36	55	Common",
        "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.80 Safari/537.36"
        };

        private static string[] HeaderReferers = {
        "http://www.google.com/?q=",
        "http://www.usatoday.com/search/results?q=",
        "http://engadget.search.aol.com/search?q=",
        "http://www.google.com/?q=",
        "http://www.usatoday.com/search/results?q=",
        "http://engadget.search.aol.com/search?q=",
        "http://www.bing.com/search?q=",
        "http://search.yahoo.com/search?p=",
        "http://www.ask.com/web?q=",
        "http://search.lycos.com/web/?q=",
        "http://busca.uol.com.br/web/?q=",
        "http://us.yhs4.search.yahoo.com/yhs/search?p=",
        "http://www.dmoz.org/search/search?q=",
        "http://www.baidu.com.br/s?usm=1&rn=100&wd=",
        "http://yandex.ru/yandsearch?text=",
        "http://www.zhongsou.com/third?w=",
        "http://hksearch.timway.com/search.php?query=",
        "http://find.ezilon.com/search.php?q=",
        "http://www.sogou.com/web?query=",
        "http://api.duckduckgo.com/html/?q=",
        "http://boorow.com/Pages/site_br_aspx?query=",
        "http://validator.w3.org/check?uri=",
        "http://validator.w3.org/checklink?uri=",
        "http://validator.w3.org/unicorn/check?ucn_task=conformance&ucn_uri=",
        "http://validator.w3.org/nu/?doc=",
        "http://validator.w3.org/mobile/check?docAddr=",
        "http://validator.w3.org/p3p/20020128/p3p.pl?uri=",
        "http://www.icap2014.com/cms/sites/all/modules/ckeditor_link/proxy.php?url=",
        "http://www.rssboard.org/rss-validator/check.cgi?url=",
        "http://www2.ogs.state.ny.us/help/urlstatusgo.html?url=",
        "http://prodvigator.bg/redirect.php?url=",
        "http://validator.w3.org/feed/check.cgi?url=",
        "http://www.ccm.edu/redirect/goto.asp?myURL=",
        "http://forum.buffed.de/redirect.php?url=",
        "http://rissa.kommune.no/engine/redirect.php?url=",
        "http://www.sadsong.net/redirect.php?url=",
        "https://www.fvsbank.com/redirect.php?url=",
        "http://www.jerrywho.de/?s=/redirect.php?url=",
        "http://www.inow.co.nz/redirect.php?url=",
        "http://www.automation-drive.com/redirect.php?url=",
        "http://mytinyfile.com/redirect.php?url=",
        "http://ruforum.mt5.com/redirect.php?url=",
        "http://www.websiteperformance.info/redirect.php?url=",
        "http://www.airberlin.com/site/redirect.php?url=",
        "http://www.rpz-ekhn.de/mail2date/ServiceCenter/redirect.php?url=",
        "http://evoec.com/review/redirect.php?url=",
        "http://www.crystalxp.net/redirect.php?url=",
        "http://watchmovies.cba.pl/articles/includes/redirect.php?url=",
        "http://www.seowizard.ir/redirect.php?url=",
        "http://apke.ru/redirect.php?url=",
        "http://seodrum.com/redirect.php?url=",
        "http://redrool.com/redirect.php?url=",
        "http://blog.eduzones.com/redirect.php?url=",
        "http://www.onlineseoreportcard.com/redirect.php?url=",
        "http://www.wickedfire.com/redirect.php?url=",
        "http://searchtoday.info/redirect.php?url=",
        "http://www.bobsoccer.ru/redirect.php?url=",
        "http://newsdiffs.org/article-history/iowaairs.org/redirect.php?url=",
        "http://seo.qalebfa.ir/%D8%B3%D8%A6%D9%88%DA%A9%D8%A7%D8%B1/redirect.php?url=",
        "http://www.firmia.cz/redirect.php?url=",
        "http://www.e39-forum.de/redir.php?url=",
        "http://www.wopus.org/wp-content/themes/begin/inc/go.php?url=",
        "http://www.selectsmart.com/plus/select.php?url=",
        "http://www.taichinh2a.com/forum/links.php?url=",
        "http://facenama.com/go.php?url=",
        "http://www.internet-abc.de/eltern/118732.php?url=",
        "http://g.makebd.com/index.php?url=",
        "https://blog.eduzones.com/redirect.php?url=",
        "http://www.mientay24h.vn/redirector.php?url=",
        "http://www.kapook.com/webout.php?url=",
        "http://lue4.ddns.name/pk/index.php?url=",
        "http://747.ddns.ms/pk/index.php?url=",
        "http://737.ddns.us/pk/index.php?url=",
        "http://a30.m1.4irc.com/pk/index.php?url="
        };

        private static string[] Keywords = {
        "propose",
        "powerful",
        "entertaining",
        "aberrant",
        "mindless",
        "impartial",
        "omniscient",
        "balance",
        "propose",
        "mixed",
        "favour",
        "silent",
        "talented",
        "store",
        "attempt",
        "even",
        "trouble",
        "bust",
        "different",
        "hurry",
        "dysfunctional",
        "wrong",
        "present",
        "lazy",
        "skillful",
        "bridge",
        "internal",
        "clean",
        "humdrum",
        "second-hand",
        "scared",
        "trousers",
        "seat",
        "truthful"
        };

        private static string GetRandomHeaderReferers()
        {
            Random rnd = new Random();
            int index = rnd.Next(0, HeaderReferers.Length);

            return HeaderReferers[index];
        }

        private static string GetRandomUserAgent()
        {
            Random rnd = new Random();
            int index = rnd.Next(0, UserAgentsList.Length);

            return UserAgentsList[index];
        }

        private static string GetRandomKeyword()
        {
            Random rnd = new Random();
            int index = rnd.Next(0, Keywords.Length);

            return Keywords[index];
        }

        public static HttpWebRequest GetWebRequest(string _url, string _proxy)
        {
            string queryParam = "?" + GetRandomKeyword() + "=" + DateTime.Now.Ticks + new Random().Next(0, 10000).ToString();
            HttpWebRequest request = HttpWebRequest.CreateHttp(_url + queryParam);

            if(_proxy != "")
            {
                WebProxy myProxy = new WebProxy(_proxy, true);
                request.Proxy = myProxy;
            }

            request.Method = "GET";
            HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            request.CachePolicy = noCachePolicy;

            WebHeaderCollection headers = new WebHeaderCollection();
            headers.Add("User-Agent", GetRandomUserAgent());
            headers.Add("Cache-Control", "no-cache");
            headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
            headers.Add("Referer", GetRandomHeaderReferers());
            headers.Add("Keep-Alive", new Random().Next(110,160).ToString());
            headers.Add("Connection", "keep-alive");
            request.Headers = headers;

            return request;
        }
    }
}
