using System.Diagnostics;
using DeepAI;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Headers;
using Twitter_Sentiment_API.Controllers;
using Twitter_Sentiment_API.Models;
using Twitter_Sentiment_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace TestProject1;

public class Tests
{
    private TweetController _controller;
    private Mock<HttpServices>? _mockTweetService;

    [SetUp]
    public void Setup()
    {
        _mockTweetService = new Mock<HttpServices>();
        _controller = new TweetController(_mockTweetService.Object);
    }

    [Test]
    public async Task GetTwitterAccessAuthorizationAndReturnOfRequestedTweets()
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
    public void GetTweetsReturnsRequestedTweets()
    {
        var tweet = _controller.GetTweets("CarlFis96135566", 1, "true", "true");

        var saveToText = new List<string>();
        var tweetSystemString = tweet.Value; 
        saveToText.AddRange(tweetSystemString!.Select(x => x.Tweet)!);
        saveToText[0].Should().Be("test 3");
    }

    [Test]
    public void GetTweetsSentimentReturnsTweetSentiment()
    {
        // Test: Tweet can be requested and sentiment of tweet can be obtained.
        // Percentage of +ve, -ve and +/- sentiments can be calculated for twitter username

        string TESTSTRING2 = "Positive: 0.00 %\nNegative: 0.00 %\nNeutral: 100.00 %";

        var result = _controller.GetTweetsSentiment("CarlFis96135566", 1);

        result.Should().BeOfType(typeof(ActionResult<string>));
        result.Value.Should().Be(TESTSTRING2);
    }

    [Test]
    public void GetSentimentAnalysisWordCloudReturnsVectorGraphicsFile()
    {
        string username = "CarlFis96135566";
        string svgLocationString1 = @$"{Environment.CurrentDirectory}/Datasets/{username}.svg";

        var result = _controller.SentimentAnalysisWordCloud(username, 3, "false", "false");

        result.Result.Should().Be(svgLocationString1);
    }

    [Test]
    public void GetCustomTextSentimentDeepAIReturnsASentimentFromASingleText1()
    {
        string inputstring = "I love you";
        string TESTSTRING2 = "[\r\n  \"Positive\"\r\n]";

        var tweet = _controller.GetCustomTextSentimentDeepAI(inputstring);
        var tweetSystemString = tweet.Value;

        tweetSystemString.Should().Be(TESTSTRING2);
    }

    [Test]
    public void GetCustomTextSentimentFromOurCustomModelReturnsSentiment()
    {
        string inputstring = "I love you";
        string TESTSTRING = "Prediction: Positive | Probability:";

        var sentiment = _controller.GetCustomTextSentimentFromOurCustomModel(inputstring);
        string sentimentSystemString = (string)sentiment.Value!;

        sentimentSystemString.Should().StartWith(TESTSTRING);
    }

    [Test]
    public void GetCustomTextSentimentFromOurCustomModelForBatchInputReturnsSentimentPercentages()
    {
        string username = "CarlFis96135566";
        string TESTSTRING = "Positive: ";
        
        var sentiment = _controller.GetCustomTextSentimentFromOurCustomModelForBatchInput(username, 1, "true", "true");
        string sentimentSystemString = (string)sentiment.Value!;

        sentimentSystemString.Should().StartWith(TESTSTRING);
    }
}