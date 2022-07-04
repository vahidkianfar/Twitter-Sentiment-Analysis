using Microsoft.AspNetCore.Mvc;

namespace Twitter_Sentiment_API.Controllers;


[ApiController]
[Route("/twitter/1.1/")]
public class TweetController : Controller
{
    // GET
    public IActionResult Index()
    {
        return NoContent();
    }
}