using System.Net;

namespace Legiscan.Helpers
{
    public class ProxyHelper
    {
        public static List<WebProxy> GenerateProxyList()
        {
            List<WebProxy> proxies = new List<WebProxy>();
            List<string> working = new List<string>();
            List<string> allLinesText = File.ReadAllLines(@"D:\ProxyList1.txt").ToList();
            int i = 0;
            while (i < allLinesText.Count)
            {
                string[] split = allLinesText[i].Split(':');
                string ip = split[0];
                string port = split[1];
                var proxy = new WebProxy
                {
                    Address = new Uri($"http://{ip}:{port}"),
                    BypassProxyOnLocal = false,
                    UseDefaultCredentials = false,

                    Credentials = new NetworkCredential(
                            userName: "pthgkvlk",
                            password: "z2553foc64xq")
                };
                proxies.Add(proxy);
                i++;
            }
            return proxies;
        }
    }
}
