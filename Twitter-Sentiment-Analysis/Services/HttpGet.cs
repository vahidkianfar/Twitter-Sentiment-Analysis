using System.Net.Http.Headers;
using System.Text.Json;
using Twitter_Sentiment_Analysis.Models;

namespace Twitter_Sentiment_Analysis.Services;

public static class HttpGet
{
    public static async Task<Tweets[]?> GetTweetsAsync(string twitterUsername, int numberOfTweets)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", "AAAAAAAAAAAAAAAAAAAAAOYpeQEAAAAAO%2FdL%2FJqRP%2Fp7zCQUlfVuzhKpnLo%3DeDEH4jnzPO47pG4SuADalcP2QpU9wLRrytbywXHnLTWjlNuYYr");

        
        //## V2 is not working with Username! I don't know why.
        
        // var url =
        //     $"https://api.twitter.com/2/users/by/username/:{twitterUsername}/tweets?max_results={numberOfTweets}&tweet_mode=extended&exclude=retweets,replies";
        
        
        var url = $"https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={twitterUsername}&count={numberOfTweets+2}&tweet_mode=extended&include_rts=0&&include_replies=0";
        var response = await client.GetAsync(url);
        var tweetJson = await response.Content.ReadAsStringAsync();
        var alltweets = JsonSerializer.Deserialize<Tweets[]>(tweetJson);
        return alltweets;
    }
}