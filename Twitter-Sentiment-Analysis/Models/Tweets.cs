using System.Text.Json.Serialization;

namespace Twitter_Sentiment_Analysis.Models;

public class Tweets
{
    [JsonPropertyName("full_text")]
    public string? Tweet { get; set; }

    [JsonPropertyName("screen_name")]
    public int Username { get; set; }

}