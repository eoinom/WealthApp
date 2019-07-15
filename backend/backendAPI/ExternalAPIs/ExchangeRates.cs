using System.Net.Http;
using System.Threading.Tasks;

namespace backendAPI.ExternalAPIs
{
    public class ExchangeRates
    {        
        private readonly string baseUrl = "https://api.exchangeratesapi.io/";

        public async Task<string> GetExchangeRate(string requestStr)
        {
            requestStr = baseUrl + requestStr;
            
            HttpClient client = new HttpClient();
            
            using (HttpResponseMessage res = await client.GetAsync(requestStr))
            {
                if (res.IsSuccessStatusCode)
                {
                    using (HttpContent content = res.Content)
                    {
                        string response = await content.ReadAsStringAsync();
                        return response;
                    }
                }
                else
                {
                    return "Error: API call unsuccessful";
                }
            }
        }
    }
}
