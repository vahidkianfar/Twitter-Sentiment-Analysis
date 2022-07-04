using System.Net;
using System.Net.Http.Headers;
using NUnit.Framework;
using FluentAssertions;
namespace TwitterTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task ResponseCode_Should_Be_200_For_Authorization()
    {
        var client = new HttpClient();
        
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", "AAAAAAAAAAAAAAAAAAAAAOYpeQEAAAAAO%2FdL%2FJqRP%2Fp7zCQUlfVuzhKpnLo%3DeDEH4jnzPO47pG4SuADalcP2QpU9wLRrytbywXHnLTWjlNuYYr");

         // var response = await client.GetAsync("https://api.twitter.com/1.1/search/tweets.json?q=%23DonaldTrump&count=100");
        var twitterUsername = "techreturners";
        var numberOfTweets = 3;
        var url = $"https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={twitterUsername}&count={numberOfTweets+2}&tweet_mode=extended&include_rts=0&&exclude_replies=1";
        var response = await client.GetAsync(url);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);

    }
}