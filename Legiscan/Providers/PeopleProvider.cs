using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using static Legiscan.Data.Models;

namespace Legiscan.Providers
{
    public class PeopleProvider
    {
        public async static Task<List<Person>> GetPeople()
        {
            HttpClient client = new HttpClient();
            List<Person> People = new List<Person>();
            string url = "https://api.legiscan.com/?key=4e45f3bb39f1ff75ccc84d094d4182fe&op=getSessionPeople&id=2008";
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            ApiResultPeople all = JsonConvert.DeserializeObject<ApiResultPeople>(responseBody);
            People = all.sessionpeople.people;

            return People;
        }

    }
}
