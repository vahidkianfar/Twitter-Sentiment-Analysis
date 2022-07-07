using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;
using DeepAI;
using Twitter_Sentiment_API.Models;

namespace Twitter_Sentiment_API.Services;

public class HttpServices
{
    private static HttpClient EstablishConnection()
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", "AAAAAAAAAAAAAAAAAAAAAOYpeQEAAAAAO%2FdL%2FJqRP%2Fp7zCQUlfVuzhKpnLo%3DeDEH4jnzPO47pG4SuADalcP2QpU9wLRrytbywXHnLTWjlNuYYr");

        return client;
    }
    public Tweets[]? GetTweets(string twitterUsername, int numberOfTweets)
    {
        var client = EstablishConnection();
        var url = $"https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={twitterUsername}&count={numberOfTweets}&tweet_mode=extended&include_rts=0&&exclude_replies=1";
        var response =  client.GetAsync(url);
        var tweetJson = response.Result.Content.ReadAsStream();
        var alltweets = JsonSerializer.Deserialize<Tweets[]?>(tweetJson);
        return alltweets;
    }
    public async Task<Tweets[]?> GetTweetsAsync(string twitterUsername, int numberOfTweets)
    {
        var client = EstablishConnection();

        //## V2 is not working with Username! I don't know why.

        // var url =
        //     $"https://api.twitter.com/2/users/by/username/:{twitterUsername}/tweets?max_results={numberOfTweets}&tweet_mode=extended&exclude=retweets,replies";

        var url =
            $"https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={twitterUsername}&count={numberOfTweets}&tweet_mode=extended&include_rts=0&&exclude_replies=1";
        var response = await client.GetAsync(url);

        var tweetJson = await response.Content.ReadAsStringAsync();

        var alltweets = JsonSerializer.Deserialize<Tweets[]>(tweetJson);
        return alltweets;
    }
    public string GetSentimentDeepAI(string username, int count)
    {
        var result = GetTweets(username, count);
        FileServices.SaveOnFile(result, username);
        var api = new DeepAI_API(apiKey: "e714104f-5b1a-4333-8dec-c0af37dcd621");
        var resp =
            api.callStandardApi("sentiment-analysis",
                new
                {
                    text = File.OpenRead(@$"{Environment.CurrentDirectory}/Datasets/{username}.txt")
                });
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
    
    public async Task<string> SentimentAnalysisWordCloud(string username, int count)
    {
        var result = await GetTweetsAsync(username, count);
        FileServices.SaveOnFile(result, username);
        var text = await File.ReadAllTextAsync(@$"{Environment.CurrentDirectory}/Datasets/{username}.txt");
        var words = text.Split();
        List<string> stopWordsList = new List<string>();
        stopWordsList = StopWordsFilter();
        var newWords = words.Where(word => !stopWordsList.Contains(word));
        text = string.Join(" ", newWords).ToLower();
        Console.WriteLine(text);
        var client = new HttpClient();
        client.BaseAddress = new Uri($"https://quickchart.io/wordcloud?text={text}");
        var response = await client.GetByteArrayAsync(client.BaseAddress);
        var file = new FileStream(@$"{Environment.CurrentDirectory}/Datasets/{username}.svg", FileMode.Create);
        file.Write(response, 0, response.Length);
        file.Close();
        return @$"{Environment.CurrentDirectory}/Datasets/{username}.svg";
    }

    public List<string> StopWordsFilter()
    {
        var stop_words = new List<string>
            { "i", "me", "my", "myself", "we", "our", "ours", "ourselves", "you", "your",
                "yours", "yourself","we'll",
                "yourselves", "he", "him", "his", "himself", "she", "her", "hers", "herself", "it", "its", "itself",
                "they", "them", "their", "theirs", "themselves", "what", "which", "who", "whom", "this", "that",
                "these",
                "those", "am", "is", "are", "was", "were", "be", "been", "being", "have", "has", "had", "having", "do",
                "does", "did", "doing", "a", "an", "the", "and", "but", "if", "or", "because", "as", "until", "while",
                "of", "at", "by", "for", "with", "about", "against", "between", "into", "through", "during", "before",
                "after", "above", "below", "to", "from", "up", "down", "in", "out", "on", "off", "over", "under",
                "again","join", "all", "any", "both", "each", "few", "more", "most", "other", "some", "such", "no", "nor","us",
                "further", "then", "once", "here", "there", "when", "where", "why", "how", "all", "any", "both", "each",
                "few", "more", "most", "other", "some", "such", "no", "nor", "not", "only", "own", "same", "so", "than",
                "too", "very", "s", "t", "can", "will", "just", "don", "should", "now", "'", "nh", "we'll", "didn't", "they'll",
                "the","haven't", "you've", "they've", "we've", "i've", "you'll", "they'll", "we'll", "i'll", "you're",
                "they're", "we're", "i'm", "you've", "they've", "we've","if", "https", "http", "get", "please","come", "here", "there", "when", "where", "why", "how", "all", "any", "both", "each", "few", "more", "most", "other", "some", "such", "no", "nor", "not", "only", "own", "same", "so", "than", "too", "very", "s", "t", "can", "will", "just", "don", "should", "now", "'", "nh", "we'll", "didn't", "they'll", "the", "haven't", "you've", "they've", "we've", "i've", "you'll", "they'll", "we'll", "i'll", "you're", "they're", "we're", "i'm", "you've", "they've", "we've", "if", "https", "http", "get", "please", "come", "here", "there", "when", "where", "why", "how", "all", "any", "both", "each", "few", "more", "most", "other", "some", "such", "no", "nor", "not", "only", "own", "same", "so", "than", "too", "very", "s", "t", "can", "will", "just", "don", "should", "now", "'", "nh", "we'll", "didn't", "they'll", "the", "haven't", "you've", "they've", "we've", "i've", "you'll", "they'll", "we'll", "i'll", "you're", "they're", "we're", "i'm", "you've", "they've", "we've", "if", "https", "http", "get", "please", "come", "here", "there", "when", "where", "why", "how", "all", "any", "both", "each", "few", "more", "most", "other", "some", "such", "no", "nor", "not", "only", "own", "same", "so", "than", "too", "very", "s", "t", "can",
                
            };

        return stop_words;
    }


}