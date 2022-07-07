using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Twitter_Sentiment_API.Models;
using Twitter_Sentiment_API.Services;

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
    public ActionResult<Tweets[]?> GetTweets(string username, int count=10)
    {
        try
        {
            var tweets = _httpServices.GetTweets(username, count);
            return tweets ;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Connection Error");
        }
    }
    // GET: /twitter/1.1/TwitterSentimentAnalysisDeepAI/TechReturners?count=100
    [HttpGet("SentimentAnalysisDeepAI/{username}")]
    public ActionResult<object> GetTweetsSentiment(string username, int count=10)
    {
        try
        {
            var tweets = _httpServices.GetSentimentDeepAI(username, count);
            return tweets ;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Connection Error");
        }
    }
    
    [HttpGet("SentimentAnalysisDeepAIcustomText/{text}")]
    public ActionResult<object> GetCustomTextSentimentDeepAI(string text)
    {
        try
        {
            var sentiment = _httpServices.GetSentimentDeepAIForText(text);
            return sentiment ;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Under Construction!");
        }
    }
    
    [HttpGet("SentimentAnalysisWordCloud/{username}")]
    [Consumes("image/svg+xml" , "image/png","application/json")]
    public Task<object> SentimentAnalysisWordCloud(string username, int count=10)
    {
        return _httpServices.SentimentAnalysisWordCloud(username, count);
       // return System.IO.File.Open($"{path}", FileMode.Open);
    }
    
    [HttpGet("OurCustomModel/{text}")]
    public ActionResult<string> GetCustomTextSentimentFromOurCustomModel(string text)
    {
        try
        {
            var sentiment = _httpServices.GetCustomTextSentimentFromOurCustomModel(text);
            return sentiment ;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Under Construction!");
        }
    }
}