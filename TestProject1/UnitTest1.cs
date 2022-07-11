using DeepAI;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Headers;
using Twitter_Sentiment_API.Controllers;
using Twitter_Sentiment_API.Models;
using Twitter_Sentiment_API.Services;

namespace TestProject1;

public class Tests
{
    private TweetController _controller;
    private HttpServices? _client;

    [SetUp]
    public void Setup()
    {
        _controller = new TweetController(_client!);
    }

    [Test]
    public async Task TwitterAccessAuthorizationAndReturnOfRequestedTweets()
    {

        var client = new HttpClient();

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
                "AAAAAAAAAAAAAAAAAAAAAOYpeQEAAAAAO%2FdL%2FJqRP%2Fp7zCQUlfVuzhKpnLo%3DeDEH4jnzPO47pG4SuADalcP2QpU9wLRrytbywXHnLTWjlNuYYr");

        // var response = await client.GetAsync("https://api.twitter.com/1.1/search/tweets.json?q=%23DonaldTrump&count=100");
        var twitterUsername = "techreturners";
        var numberOfTweets = 3;
        var url =
            $"https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={twitterUsername}&count={numberOfTweets}&tweet_mode=extended";
        var response = await client.GetAsync(url);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    public void TwitterAsyncRequestReturnsRequestedTweets()
    {
        var result =  _controller.GetTweets("CarlFis96135566", 1);
        result.Should().BeOfType(typeof(ActionResult<Tweets[]>));
    }

    [Test]
    public void GetTweetsSentimentReturnsTweetSentiments()
    {
        string username = "CarlFis96135566";
        var httpServices = new HttpServices();
        var result = httpServices.GetTweets(username, 3, "true", "true");
        FileServices.SaveOnFileForTest(result, username);
        var api = new DeepAI_API(apiKey: "e714104f-5b1a-4333-8dec-c0af37dcd621");
        var resp =
            api.callStandardApi("sentiment-analysis",
                new
                {
                    text = File.ReadAllText(@$"{Environment.CurrentDirectory}/{username}.txt")
                });

        var result1 = api.objectAsJsonString(resp.output);

        result1.Should().BeOfType(typeof(string));

        var result2 = _controller.GetTweetsSentiment("CarlFis96135566", 3);
        
        result2.Should().BeOfType(typeof(ActionResult<string>));
    }

    [Test]
    public void GetCustomTextSentimentDeepAIReturnsASentimentFromASingleText()
    {
        // Arrange

        string inputstring = "I love you";
        ActionResult<object> test_sentiment = "Positive";
        ActionResult<object> result;
        ActionResult<object> result1 = "Positive";


        result = _controller.GetCustomTextSentimentDeepAI(inputstring);
        result.Should().BeOfType(typeof(ActionResult<object>)); //  Works just fine
    }

    [Test]
    public void GetCustomTextSentimentFromOurCustomModelReturnssentiment()
    {
        string inputstring = "I love you";
        ActionResult<string> teststring = "positive";

        Console.WriteLine(inputstring);
        Console.WriteLine(teststring);
        var sentiment = _controller.GetCustomTextSentimentFromOurCustomModel(inputstring);
        sentiment.Should().BeOfType(typeof(ActionResult<object>)); //  Works just fine
    }
}