using System.ComponentModel;
using System.Text;
using Mosaik.Core;
using Twitter_Sentiment_Analysis.Services;

Console.Write("Enter Username: ");
var username = Console.ReadLine()!;

Console.Write("Enter number of tweets: ");
var numberOfTweets = int.Parse(Console.ReadLine()!);
var listOfTweets = new List<string>();
try
{
    var result = await HttpGet.GetTweetsAsync(username, numberOfTweets);

    var counter = 1;
    foreach (var item in result)
    {
        Console.WriteLine($"******* Tweet Number {counter}: " + item.Tweet);
        counter++;
    }
    listOfTweets.AddRange(result.Select(x => x.Tweet)!);
    File.WriteAllLines(@$"{Environment.CurrentDirectory}/{username}.txt", listOfTweets);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

