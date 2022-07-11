namespace Twitter_Sentiment_API.Methods;

public static class SentimentPercentages
{
    public static string GetPercentage(string tweetSentiment)
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
    public static string GetPercentageForBatchInput(List<string> tweetSentiment)
    {
        double positive = 0;
        double negative = 0;
        foreach (var sentiment1 in tweetSentiment.Select(sentiment => 
                     sentiment.ToLower().Split(",").ToList()).SelectMany(sentiments => sentiments))
        {
            if (sentiment1.Contains("positive"))
            {
                positive++;
            }
            else if (sentiment1.Contains("negative"))
            {
                negative++;
            }
        }
        var total = positive + negative;
        var positivePercentage = (positive / total!) * 100;
        var negativePercentage = (negative / total) * 100;
        return
            $"Positive: {positivePercentage:F} %\nNegative: {negativePercentage:F} %" + tweetSentiment.Last();
    }

}