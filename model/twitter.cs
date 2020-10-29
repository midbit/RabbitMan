using Interface.Model;
using System;
namespace Model
{
    enum Source
    {
        BTS,
        Mob
    }
    class Image
    {
        public string url {get; set;}
        public int width {get; set;}
        public int height {get; set;}
    }
    class Url
    {
        public int start {get; set;}
        public int end {get; set;}
        public string url{get; set;}
        public string expanded_url{get; set;}
        public string display_url{get; set;}
        public Image[] images {get; set;}
        public int status {get; set;}
        public string title {get; set;}
        public string description {get; set;}
        public string unwound_url {get; set;}
    }
    class Hashtag
    {
        public int start {get; set;}
        public int end {get; set;}
        public string tag {get; set;}

    }
    class TweetEntity
    {
        public Hashtag[] hashtags {get; set;}
        public Url[] urls {get; set;}
    }
    class TweetUser
    {
        public string id{get; set;}
        public string name{get; set;}
        public string username {get; set;}
    }
    class TweetMedia
    {
        public int height{get; set;}
        public int width{get; set;}
        public string url{get; set;}
        public string type{get; set;}
        public string media_key{get; set;}

        public TweetMedia(){}
    }
    class TweetInclude
    {
        public TweetUser[] users {get; set;}
        public TweetMedia[] media{get; set;}
        public TweetInclude(){}
    }
    class TweetMatchingRule
    {
        public long id {get; set;}
        public string tag {get; set;}
    }
    class TweetData 
    {
        public string id {get; set;}
        public TweetEntity entities {get; set;}
        public string text {get; set;}
        public string author_id {get; set;}
        
    }
    class Tweet: IStreamable
    {
        private static readonly string defaultImage = "https://am22.mediaite.com/tms/cnt/uploads/2020/10/OTM_1100_0020_ALPHA_201569.1023-1200x649.jpg";
        public TweetData data {get; set;}
        public TweetInclude includes {get; set;}
        public TweetMatchingRule[] matching_rules {get; set;}
        public Tweet(){}
        public bool ShouldBroadcast()
        {
         
            try
            {
                if(this.includes.users[0].username != "BTS_SkyTrain")
                {
                    if(this.data.entities.hashtags != null && this.data.entities.hashtags.Length != 0)
                    {
                        foreach (Hashtag hashtag in this.data.entities.hashtags)
                        {
                            Console.WriteLine(hashtag.tag);
                            if(hashtag.tag.Contains("ม็อบ"))
                            {

                                return true;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine(data.text);
                    return this.data.text.Contains("ปิดบริการ") || this.data.text.Contains("เปิดให้บริการ");
                }
            }
            catch(NullReferenceException)
            {
                return false;
            }
            return false;
        }
        private string FindImage()
        {
            try
            {
                if(this.includes != null)
                {
                    if(this.includes.media != null  && this.includes.media.Length > 0)
                    {
                        foreach(TweetMedia media in this.includes.media)
                        {
                            if(media.type == "photo")
                            {
                                return media.url;
                            }
                        }
                    }
                }
                return defaultImage;
            }
            catch(NullReferenceException)
            {
                return defaultImage;
            }
           
        }
        public IMessage ConvertToMessage()
        {
            string imageUrl = this.FindImage(); 
            string headline = this.includes.users[0].username;
            string text = this.data.text;
            string altText = headline;
            return new LineFlexMessage(imageUrl, text, headline, altText);
        }
    }
}