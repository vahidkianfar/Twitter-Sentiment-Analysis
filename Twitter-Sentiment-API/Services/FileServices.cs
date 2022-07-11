using Twitter_Sentiment_API.Models;
using static Twitter_Sentiment_API.Methods.CleanTheText;

namespace Twitter_Sentiment_API.Services;

public static class FileServices
{
    public static void SaveOnFile(Tweets[]? tweets, string username)
    {
        var saveToText = new List<string>();
        saveToText.AddRange(tweets!.Select(x => x.Tweet)!);
        saveToText.Where(x=>Clean(x) != null).ToList().ForEach(x => saveToText.Add(Clean(x)));
        File.WriteAllLines(@$"{Environment.CurrentDirectory}/Datasets/{username}.txt", saveToText);
    }
    public static void SaveOnFileForTest(Tweets[]? tweets, string username)
    {
        var saveToText = new List<string>();
        saveToText.AddRange(tweets!.Select(x => x.Tweet)!);
        File.WriteAllLines(@$"{Environment.CurrentDirectory}/{username}.txt", saveToText);
    }
}