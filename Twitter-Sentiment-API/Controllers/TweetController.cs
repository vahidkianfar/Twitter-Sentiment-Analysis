using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Twitter_Sentiment_API.Models;
using Twitter_Sentiment_API.Services;
using System.Net.Http;
using System.Net;
using Twitter_Sentiment_API.Methods;

namespace Twitter_Sentiment_API.Controllers;


[ApiController]
[Route("/twitter/1.1/")]
public class TweetController : Controller
{
    private readonly HttpServices _httpServices;

    public TweetController(HttpServices connectToTwitter)
    {
        _httpServices = connectToTwitter;
    }

    // GET: /twitter/1.1/username/TechReturners?count=100
    [HttpGet("tweets/{username}")]
    public ActionResult<Tweets[]?> GetTweets(string username, int numberOfTweets=10, string retweets="true", string replies="true")
    {
        try
        {
            return _httpServices.GetTweets(username, numberOfTweets, retweets, replies);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("GetTweets: Connection to Twitter Error");
        }
    }
    
    // GET: /twitter/1.1/TwitterSentimentAnalysisDeepAI/TechReturners?count=100
    [HttpGet("SentimentAnalysisDeepAI/{username}")]
    public ActionResult<string> GetTweetsSentiment(string username, int numberOfTweets=10, string retweets="true", string replies="true")
    {
        try
        {
            return _httpServices.GetSentimentDeepAI(username, numberOfTweets, retweets, replies );
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("SentimentAnalysisDeepAICustomText/{text}")]
    public ActionResult<object> GetCustomTextSentimentDeepAI(string text)
    {
        try
        {
            return _httpServices.GetSentimentDeepAIForText(text);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("SentimentAnalysisWordCloud/{username}")]
    [Consumes("image/svg+xml", "image/png", "application/json")]
    public Task<object> SentimentAnalysisWordCloud(string username, int numberOfTweets=10, string retweets="true", string replies="true")
    {
        return _httpServices.SentimentAnalysisWordCloud(username, numberOfTweets, retweets, replies);
    }

    [HttpGet("OurCustomModel/{text}")]
    public ActionResult<object> GetCustomTextSentimentFromOurCustomModel(string text)
    {
        try
        {
            return _httpServices.GetCustomTextSentimentFromOurCustomModel(text);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
    [HttpGet("OurCustomModelForBatchInput/{username}")]
    public ActionResult<object> GetCustomTextSentimentFromOurCustomModelForBatchInput(string username, int numberOfTweets=10, string retweets="true", string replies="true")
    {
        try
        {
            return _httpServices.GetCustomTextSentimentFromOurCustomModelForBatchInput( username,  numberOfTweets=10,  retweets="true",  replies="true");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }
}