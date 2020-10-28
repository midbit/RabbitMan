using System;
using Service;
using System.Threading.Tasks;


namespace RabbitMan
{
    class Program 
    {

        static async Task Main(string[] args)
        {
            DotNetEnv.Env.Load();
            string TWITTER = Environment.GetEnvironmentVariable("TWITTER_TOKEN");
            string TWITTER_URL = Environment.GetEnvironmentVariable("TWITTER_URL");
            string LINE_URL = Environment.GetEnvironmentVariable("LINE_TOKEN");
            string LINE_TOKEN = Environment.GetEnvironmentVariable("LINE_URL");
          
            LineClient client = new LineClient(LINE_URL, LINE_TOKEN);
            TwitterClient twitterClient = new TwitterClient(TWITTER,TWITTER_URL, client);
            await twitterClient.Stream();
        }
    }
}
