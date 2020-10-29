# RabbitMan
RabbitMan is a system which stream updates about the mob progress from Twitter, and then broadcast it to a Line bot with the same name. It is made for office workers and students who simply just wanted to get home on time.

## Disclaimer
This was originally a toy project for me to practice C# and .NET Core framework in a few days. Many of the lines are not exactly an industrial standard grades, but most of the sensitive information are hidden and is safe to be deploy elsewhere. This is by a no mean a proper framework, but I did tried to make it as adaptive as possible. 

## Adding the bot
Adding the RabbitMan line bot is easy, you can do it from  the following QR code.

![alt text](RabbitMan_QR.png)


## Extending RabbitMan
Although RabbitMan is made in a few days, its design make it easy for any other people to extend its functionality eg: stream from other Twitter events, change the format of the Line message.

### Requirements
RabbitMan utilized Line Messasging API and Twitter Developer API, thus it is essnetial for anyone who wish to extend its functionality to create a developer account for both platforms.

### Environments
All the environments variable that use within the system.


| Environement        | Description     |
| ------------- |:-------------:|
| TWITTER_TOKEN      | The bearer token for Twitter API   |
| TWITTER_URL     |    Twitter's streaming API   |
| LINE_TOKEN |     The bearer token for Line API |
| LINE_URL | Line broadcast API |


### Deploying RabbitMan
The standard way to deploy RabbitMan is via Docker. You may publish the application first and then mount it into a container with all its environment variables setted up.

Publishing the application
```
dotnet publish -c Release
```

Build docker image
```
docker build . -t {TagName}
```

Running the container
```
docker run -d -t -i -e {Environment...} --name {ContainerName} {ImageName}
```

### Changing the Stream Rule
The system use the Twitter V2 Streaming API so in order to change the streaming rule. You can do it via the [Twitter API](https://developer.twitter.com/en/docs/twitter-api/tweets/filtered-stream/introduction). 

### Writing Your Own Custom Line Message
If the line message's template does not suit your need, you can make a new Twitter data object model by implementing the IStreamable interface and a new Line Message by implementing the IMessage interface. You can then change the generic input parameter in Program.cs file to your newly created Twitter data object model.

Replacing your newly created model in Program.cs in line 21
```
TwitterClient<YourIStreamableModel> twitterClient = new TwitterClient<YourIStreamableModel>(TWITTER,TWITTER_URL, client);
```

