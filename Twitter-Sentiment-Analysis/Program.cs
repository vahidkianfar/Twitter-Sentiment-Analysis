using LinqToTwitter;
using LinqToTwitter.OAuth;
using TweetMode = LinqToTwitter.Common.TweetMode;



//Our Credentials
var consumerKey = "NnnBVBDRZCfdjnlMGnMvtExsn";
var consumerSecret = "qMazdCOVFpWOHpic1AKnzMhGT5uyfEKZZiLuQc2znmsRGa25pa";
var accessToken = "958716870890319872-EzAJT4U0Qj03oX8radNUABF6Pd8VRoK";
var accessTokenSecret = "nzFgDkP9T4iqzvFPi0xg0OzQABOvGy1Lr07Og1aFiiICX";

//Loading the Credentials
var auth = new PinAuthorizer
{
    CredentialStore = new InMemoryCredentialStore
    {
        ConsumerKey = consumerKey,
        ConsumerSecret = consumerSecret,
        OAuthToken = accessToken,
        OAuthTokenSecret = accessTokenSecret
    }
};

//Creating Twitter Client/Context
var twitterCtx = new TwitterContext(auth);

//Username and the number of tweets of the user we want to get the tweets from
var screenName= "NHSEngland";
var numberOfTweets = 5;


//Getting the tweets from the user with Query
var statusTweets =
    from tweet in twitterCtx.Status
    where tweet.Type == StatusType.User && tweet.ScreenName == screenName
                                        && tweet.TweetMode == TweetMode.Extended && tweet.Count == numberOfTweets
    select tweet;


//Printing the tweets
foreach(var tweet in statusTweets)
{
    Console.WriteLine($"{tweet.ScreenName}: {tweet.FullText}");
}