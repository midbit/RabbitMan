# RabbitMan
RabbitMan is a system which stream updates about the mob progress from Twitter, and then broadcast it to a Line bot with the same name. It is made for office workers and students who simply just wanted to get home on time.

## Disclaimer
This was originally a toy project for me to practice C# and .NET Core framework in a few days. Many of the lines are not exactly an industrial standard grades, but most of the sensitive information are hidden and is safe to be deploy elsewhere.

## Adding the bot
Adding the RabbitMan line bot is easy, you can do it from QR code or this link.

## Extending RabbitMan
Although RabbitMan is made in a few days, its design make it easy for any other people to extend its functionality eg: stream from other Twitter events, Changes the Line message.

### Requirements
RabbitMan utilized Line Messasging API and Twitter Developer API, thus it is essnetial for anyone who wish to extend its functionality to create a developer account for both platforms.

### Environments
To be added

### Deploying RabbitMan
The standard way to deploy RabbitMan is via Docker. You may publish the application first and then mount it into a container with all its environment variables setted up.
