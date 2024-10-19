using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Embedded_Home_Screen.Controller
{
    public class UpdateController
    {
        public string repo = "/BoTech-Development/VarnishMachine";
        public const string thisVersion = "v1.0.0";
        public List<> CheckForUpdates()
        {

        }
        public void UpdateAll()
        {

        }
        private string RequestApiJson(string url, string urlParams)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(urlParams).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return "!!!Error!!!: StatusCode: " + response.StatusCode + "; Result: " + response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
