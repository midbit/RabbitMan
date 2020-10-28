using Interface.Service;
using Interface.Model;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class LineClient: IBroadcastService
    {
        static readonly HttpClient client = new HttpClient();
        private string url;
        private string access_token;

        public string Url {
            get{
                return url;
            }
        }

        public string AccessToken {
            get{
                return access_token;
            }
        }

        public async Task<string> Broadcast(IMessage message)
        {
            StringContent stringContent = new StringContent(message.ToJson(), Encoding.UTF8, "application/json");
            Console.WriteLine(message.ToJson());
            HttpResponseMessage response =  await client.PostAsync(url, stringContent);
            return response.StatusCode.ToString();
        }
        public LineClient(string setUrl, string setAccessToken){
            string token = String.Format("Bearer {0}", setAccessToken);
            client.DefaultRequestHeaders.Add("Authorization", token);
            url = setUrl;
            access_token = setAccessToken;
        }
    }
}