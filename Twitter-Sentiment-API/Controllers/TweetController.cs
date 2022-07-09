using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Twitter_Sentiment_API.Models;
using Twitter_Sentiment_API.Services;
using System.Net.Http;
using System.Net;

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
            return BadRequest("GetTweets: Connection to Twitter Error");
        }
    }

    // GET: /twitter/1.1/username/Async/TechReturners?count=100
    [HttpGet("tweets/{username}/{count}/{dummy}")]
    // public async Task<ActionResult<Tweets[]?>> GetTweetsAsync(string username, int count = 10, int dummy = 1)
    public async Task<ActionResult<object>> GetTweetsAsync(string username, int count = 10, int dummy = 1)

    {
        HttpResponseMessage httpresponse;
        Tweets[]? tweets;

        try
        {
            (httpresponse, tweets) = await _httpServices.GetTweetsAsync(username, count);

            if (httpresponse.StatusCode == HttpStatusCode.OK)
            {
                return tweets!;
            }
            else
            {
                return Result(HttpStatusCode.NotFound, $"Tweets associated with user: {username} not retrieved");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("GetTweetsAsync: Tweets associated with user: {username} not retrieved: Connection Error");
        }
    }

    // GET: /twitter/1.1/TwitterSentimentAnalysisDeepAI/TechReturners?count=100
    [HttpGet("SentimentAnalysisDeepAI/{username}")]
    public ActionResult<object> GetTweetsSentiment(string username, int count=10)
    {
        try
        {
            var tweetssentiment = _httpServices.GetSentimentDeepAI(username, count);
            return tweetssentiment;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("GetTweetsSentiment: Tweets associated with user: {username} not retrieved: Connection Error");
        }
    }
    
    [HttpGet("SentimentAnalysisDeepAIcustomText/{text}")]
    public ActionResult<object> GetCustomTextSentimentDeepAI(string text)
    {
        try
        {
            // ActionResult<object> sentiment;
            var sentiment = _httpServices.GetSentimentDeepAIForText(text);
            return sentiment;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Under Construction! GetCustomTextSentimentDeepAI: Sentiment associated with text: {text} not retrieved: Connection Error");
        }
    }
    
    [HttpGet("SentimentAnalysisWordCloud/{username}")]
    [Consumes("image/svg+xml", "image/png", "application/json")]
    public async Task<ActionResult<object>> SentimentAnalysisWordCloud(string username, int count = 10)
    {
        HttpResponseMessage httpresponse;

        try
        {
            (httpresponse, var sentiment) = await _httpServices.SentimentAnalysisWordCloud(username, count);

            if (httpresponse.StatusCode == HttpStatusCode.OK)
            {
                return sentiment;
                // return System.IO.File.Open($"{path}", FileMode.Open);
            }
            else
            {
                return Result(HttpStatusCode.NotFound, $"SentimentAnalysisWordCloud: Tweet analysis associated with user: {username} not retrieved");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("SentimentAnalysisWordCloud: Tweet analysis associated with user: {username} not retrieved: Connection Error");
        }
    }

    [HttpGet("OurCustomModel/{text}")]
    public ActionResult<object> GetCustomTextSentimentFromOurCustomModel(string text)
    {
        try
        {
            var sentiment = _httpServices.GetCustomTextSentimentFromOurCustomModel(text);
            return sentiment ;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("Under Construction! GetCustomTextSentimentFromOurCustomModel: Sentiment associated with input text: {text} not retrieved: Connection Error");
        }
    }
    public static ActionResult Result(HttpStatusCode statusCode, string reason) => new ContentResult
    {
        StatusCode = (int)statusCode,
        Content = $"Status Code: {(int)statusCode} {statusCode}: {reason}",
        ContentType = "text/plain",
    };
}