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
    public ActionResult<Tweets[]?> GetTweets(string username, int numberOfTweets=10, string retweets="true", string replies="true")
    {
        try
        {
            var tweets = _httpServices.GetTweets(username, numberOfTweets, retweets, replies);
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
    public ActionResult<string> GetTweetsSentiment(string username, int numberOfTweets=10, string retweets="true", string replies="true")
    {
        try
        {
            var tweetSentiment = _httpServices.GetSentimentDeepAI(username, numberOfTweets, retweets, replies );
            var finalPercentage = _httpServices.GetPercentage(tweetSentiment);
            return finalPercentage ;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
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
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("SentimentAnalysisWordCloud/{username}")]
    [Consumes("image/svg+xml")]
    public Task<object> SentimentAnalysisWordCloud(string username, int numberOfTweets=10, string retweets="true", string replies="true")
    {
        return _httpServices.SentimentAnalysisWordCloud(username, numberOfTweets, retweets, replies);
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
            return BadRequest(e.Message);
        }
    }
}