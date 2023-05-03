using Newtonsoft.Json;
using System.Net;
using static Legiscan.Data.Models;

namespace Legiscan.Providers
{
    public class MasterListProvider
    {
        public async static Task<List<string>> GetMasterList()
        {
            HttpClient client = new HttpClient();
            List<MasterList> People = new List<MasterList>();
            string url = "https://api.legiscan.com/?key=4e45f3bb39f1ff75ccc84d094d4182fe&op=getMasterListRaw&id=2008";
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var x = responseBody.Split(',');
            int i = 0;
            List<string> ids = new List<string>();
            while (i < x.Length)
            {
                var thi = x[i];
                if (thi.Contains("bill_id"))
                {
                    var y = thi.Split(':');
                    ids.Add(y[y.Length - 1]);
                }


                i++;
            }
            return ids;
        }

        public async static Task<ApiResultBill> GetThisBill(string bill, WebProxy proxy)
        {
            HttpClient client = new HttpClient();
            HttpClientHandler handler = new HttpClientHandler();
            handler.Proxy = proxy;
            string url = "https://api.legiscan.com/?key=4e45f3bb39f1ff75ccc84d094d4182fe&op=getBill&id="+bill;
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            ApiResultBill resultBill = JsonConvert.DeserializeObject<ApiResultBill>(responseBody);
            return resultBill;
        }
    }
}
