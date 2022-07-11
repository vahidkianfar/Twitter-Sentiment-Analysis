using System.Net.Http.Headers;
using System.Text.Json;
using DeepAI;
using Twitter_Sentiment_API.Methods;
using Twitter_Sentiment_API.Models;

namespace Twitter_Sentiment_API.Services;

public class HttpServices
{
    public static HttpClient EstablishConnection()
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer",
                "AAAAAAAAAAAAAAAAAAAAAOYpeQEAAAAAO%2FdL%2FJqRP%2Fp7zCQUlfVuzhKpnLo%3DeDEH4jnzPO47pG4SuADalcP2QpU9wLRrytbywXHnLTWjlNuYYr");

        return client;
    }

    public Tweets[]? GetTweets(string twitterUsername, int numberOfTweets, string retweets, string replies)
    {
        var client = EstablishConnection();
        var rtsValue = retweets == "true" ? 1 : 0;
        var repliesValue = replies == "true" ? 0 : 1;
        var url =
            $"https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={twitterUsername}&count={numberOfTweets}&tweet_mode=extended&include_rts={rtsValue}&&exclude_replies={repliesValue}";
        var response = client.GetAsync(url);
        var tweetJson = response.Result.Content.ReadAsStream();
        var alltweets = JsonSerializer.Deserialize<Tweets[]?>(tweetJson);
        return alltweets;
    }

    public async Task<(HttpResponseMessage, Tweets[]?)> GetTweetsAsync(string twitterUsername, int numberOfTweets)
    {
        var client = EstablishConnection();
        var url =
            $"https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={twitterUsername}&count={numberOfTweets}&tweet_mode=extended&include_rts=0&&exclude_replies=0";
        var response = await client.GetAsync(url);

        var tweetJson = await response.Content.ReadAsStringAsync();

        var alltweets = JsonSerializer.Deserialize<Tweets[]>(tweetJson);
        return (response, alltweets);
    }
    
    public string GetSentimentDeepAI(string username, int numberOfTweets, string retweets, string replies)
    {
        var result = GetTweets(username, numberOfTweets, retweets, replies);
        FileServices.SaveOnFile(result, username);
        var api = new DeepAI_API(apiKey: "e714104f-5b1a-4333-8dec-c0af37dcd621");
        var resp =
            api.callStandardApi("sentiment-analysis",
                new
                {
                    text = File.ReadAllText(@$"{Environment.CurrentDirectory}/Datasets/{username}.txt")

                });
        Console.WriteLine(resp);
        //_context.SaveChanges();
        return api.objectAsJsonString(resp.output);
    }

    public string GetSentimentDeepAIForText(string inputText)
    {
        var api = new DeepAI_API(apiKey: "e714104f-5b1a-4333-8dec-c0af37dcd621");

        var resp = api.callStandardApi("sentiment-analysis", new
        {
            text = inputText,
        });
        //_context.SaveChanges();
        return api.objectAsJsonString(resp.output);
    }

    public string GetCustomTextSentimentFromOurCustomModel(string inputText)
    {
        return CreateMLModel.Program.Start(inputText);
    }
    
    public string GetCustomTextSentimentFromOurCustomModelForBatchInput(string username, int numberOfTweets=10, string retweets="true", string replies="true")
    {
        var result = GetTweets(username, numberOfTweets, retweets, replies);
        FileServices.SaveOnFile(result, username);
        return CreateMLModel.Program.Start(File.ReadAllText(@$"{Environment.CurrentDirectory}/Datasets/{username}.txt"));
    }

    public async Task<object> SentimentAnalysisWordCloud(string username, int numberOfTweets, string retweets,
        string replies)
    {
        var result = GetTweets(username, numberOfTweets, retweets, replies);

        FileServices.SaveOnFile(result, username);
        var text = File.ReadAllText(@$"{Environment.CurrentDirectory}/Datasets/{username}.txt");
        var cleanedText = CleanTheText.Clean(text);
        var client = new HttpClient();
        client.BaseAddress = new Uri($"https://quickchart.io/wordcloud?maxNumWords=50&&text={cleanedText}");
        var response = await client.GetByteArrayAsync(client.BaseAddress);
        var file = new FileStream(@$"{Environment.CurrentDirectory}/Datasets/{username}.svg", FileMode.Create);
        file.Write(response, 0, response.Length);
        file.Close();

        //return file.ReadAsync(  response, 0, response.Length);
        return @$"{Environment.CurrentDirectory}/Datasets/{username}.svg";
    }

    public string GetPercentage(string tweetSentiment)
    {
        double positive = 0;
        double negative = 0;
        double neutral = 0;
        tweetSentiment = tweetSentiment.Replace("\"", "");
        var sentiments = tweetSentiment.ToLower().Split(",").ToList();

        foreach (var sentiment in sentiments)
        {
            if (sentiment.Contains("positive"))
            {
                positive++;
            }
            else if (sentiment.Contains("negative"))
            {
                negative++;
            }
            else if (sentiment.Contains("neutral"))
            {
                neutral++;
            }
        }

        var total = positive + negative + neutral;
        var positivePercentage = (positive / total!) * 100;
        var negativePercentage = (negative / total) * 100;
        var neutralPercentage = (neutral / total) * 100;

        return
            $"Positive: {positivePercentage:F} %\nNegative: {negativePercentage:F} %\nNeutral: {neutralPercentage:F} %";
    }

}