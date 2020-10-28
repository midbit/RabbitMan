using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Interface.Service;
using Model;
using System.Threading.Tasks;

namespace Service 
{
    class TwitterClient:IStream
    {
        static readonly HttpClient client = new HttpClient();
        private string token;
        private string url;
        private IBroadcastService broadcastService;

        public string Token 
        {
            get
            {
                return this.token;
            } 
            set
            {
                this.token = Token;
            }
        }
        public string Url
        {
            get
            {
                return this.url;
            }
            set
            {
                this.url = Url;
            }
        }
        public TwitterClient(string token, string url, IBroadcastService broadcastService)
        {
            this.url = url;
            this.broadcastService = broadcastService;
            string twitter_token = String.Format("Bearer {0}", token);
            client.DefaultRequestHeaders.Add("Authorization", twitter_token);        
        }
        private async Task<String> Broadcast(Tweet tweet)
        {
            if(ShouldBroadcast(tweet))
            {
                return await broadcastService.Broadcast(tweet.ConvertToMessage());
            }
            return("404");
        }

        private bool ShouldBroadcast(Tweet tweet)
        {
            try
            {
                if(tweet.includes.users[0].username == "FreeYOUTHth")
                {
                    if(tweet.data.entities.hashtags != null && tweet.data.entities.hashtags.Length != 0)
                    {
                        foreach (Hashtag hashtag in tweet.data.entities.hashtags)
                        {
                            if(hashtag.tag.Contains("ม๊อบ"))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch(NullReferenceException)
            {
                return false;
            }
            return false;
        }
        public async Task Stream()
        {
            HttpResponseMessage response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                while (!reader.EndOfStream)
                {
                        var jsonData = reader.ReadLine();
                        if(jsonData.Length > 0)
                        {
                            try
                            {
                                Console.WriteLine(jsonData);
                                Tweet tweet = JsonSerializer.Deserialize<Tweet>(jsonData);
                                var result = await Broadcast(tweet);
                                Console.WriteLine(result);
                            }
                            catch (JsonException e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                        else
                        {
                            Console.WriteLine("<3");
                        }
                }
            }
            
        }
    }
}