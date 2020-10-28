using Interface.Model;
using System.Text.Json;

namespace Model
{
    
    enum Weight
    {
        regular,
        bold
    }
    enum FontSize 
    {
        xl,
        md 
    }
    class Content
    {
        public string type { get; set; }
        public string text { get; set; }
        public string weight { get; set; }
        public string size { get; set; }
        public bool wrap { get; set; }

        public Content(string setType, string setText, string  setWeight, string setSize, bool setWrap)
        {
            type = setType;
            text = setText;
            weight = setWeight;
            size = setSize;
            wrap = setWrap;
        }
    }
    class Hero
    {
        public string type { get; set; }
        public string url { get; set; }
        public string size { get; set; }
        public string aspectRatio { get; set; }
        public string aspectMode { get; set; }

        public Hero(string setType, string setUrl, string setSize, string setAspectRatio, string setAspectMode) 
        {
            type = setType;
            url = setUrl;
            size = setSize;
            aspectRatio  = setAspectRatio;
            aspectMode = setAspectMode;
        }
    }
    class Body
    {
        public string type { get; set; }
        public string layout { get; set; }
        public Content[] contents { get; set; }

        public Body(string setType, string setLayout, Content[] setContents)
        {
            type = setType;
            layout = setLayout;
            contents = setContents;
        }

    }
    class LineMessage: IMessage {
        public LineFlexMessage[]  messages  { get; set; }
        public LineMessage(LineFlexMessage[] messages)
        {
            this.messages = messages;
        }
         public string ToJson()
        {

            return JsonSerializer.Serialize(this);
        }
    }

    class LineContainer
    {
        const string FLEX_TYPE = "bubble";
        const string ASPECT_RATIO = "20:13";
        const string ASPECT_MODE = "cover";
        const string HERO_IMAGE_TYPE = "image";
        const string HERO_SIZE = "full";
        const string  BODY_TYPE = "box";
        const string BODY_LAYOUT = "vertical";
        const string CONTENT_TYPE = "text";

        const string CONTENT_WEIGHT = "bold";
        const string CONTENT_SIZE = "xl";
        public string type  { get; set; }
        public Hero hero  { get; set; }
        public Body body  { get; set; }

        public LineContainer(string imageUrl, string text, string headline)
        {
            type = FLEX_TYPE;
            hero = new Hero(HERO_IMAGE_TYPE, imageUrl, HERO_SIZE, ASPECT_RATIO, ASPECT_MODE);
            Content headline_content = new Content(CONTENT_TYPE, headline, Weight.bold.ToString(), FontSize.xl.ToString(),false);
            Content bodyText = new Content(CONTENT_TYPE, text, Weight.regular.ToString(), FontSize.md.ToString(), true);
            Content[] contents = {headline_content, bodyText};
            body = new Body(BODY_TYPE, BODY_LAYOUT, contents);
        }

     
    } 
    class LineFlexMessage: IMessage {
        const string TYPE = "flex";
        public string altText { get; set; }
        public LineContainer contents { get; set; }
        public string type {get; set;}
        public LineFlexMessage(string imageUrl, string text, string headline, string altText)
        {
            this.contents = new LineContainer(imageUrl, text, headline);
            this.altText = altText;
            this.type = TYPE;
        }
        public string ToJson()
        {
            LineFlexMessage[] messages = {this};
            LineMessage broadcast = new LineMessage(messages);
            return  JsonSerializer.Serialize(broadcast);
        }
    }
}