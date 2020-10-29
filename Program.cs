using System;
using Service;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using Model;
namespace RabbitMan
{
    class Program 
    {

        static async Task Main(string[] args)
        {
            DotNetEnv.Env.Load();
            string TWITTER = Environment.GetEnvironmentVariable("TWITTER_TOKEN");
            string TWITTER_URL = Environment.GetEnvironmentVariable("TWITTER_URL");
            string LINE_TOKEN = Environment.GetEnvironmentVariable("LINE_TOKEN");
            string LINE_URL = Environment.GetEnvironmentVariable("LINE_URL");
          
            LineClient client = new LineClient(LINE_URL, LINE_TOKEN);
            TwitterClient<Tweet> twitterClient = new TwitterClient<Tweet>(TWITTER,TWITTER_URL, client);
            while(true)
            {
                try
                {
                    await twitterClient.Stream();
                }
                catch(IOException e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Attempting to reconnect waiting 10 second.");
                    System.Threading.Thread.Sleep(10000);
                }
                catch(HttpRequestException e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Attempting to reconnect waiting 10 second.");
                    System.Threading.Thread.Sleep(10000);
                }
            }
        }
    }
}
