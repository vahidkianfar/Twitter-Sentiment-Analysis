using Twitter_Sentiment_Analysis.Services;

Console.Write("Enter Username: ");
var username = Console.ReadLine()!;

Console.Write("Enter number of tweets: ");
var numberOfTweets = int.Parse(Console.ReadLine()!);

try
{
    var result = await HttpGet.GetTweetsAsync(username, numberOfTweets);

    var counter = 1;
    foreach (var item in result)
    {
        Console.WriteLine($"******* Tweet Number {counter}: " + item.Tweet);
        counter++;
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}