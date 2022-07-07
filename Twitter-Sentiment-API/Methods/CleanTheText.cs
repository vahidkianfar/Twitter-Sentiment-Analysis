namespace Twitter_Sentiment_API.Methods;

public static class CleanTheText
{
    public static List<string> StopWordsFilter()
    {
        var stopWords = new List<string>
            { "i", "me", "my", "myself", "we", "our", "ours", "ourselves", "you", "your",
                "yours", "yourself","we’ll","it’s",
                "yourselves", "he", "him", "his", "himself", "she", "her", "hers", "herself", "it", "its", "itself",
                "they", "them", "their", "theirs", "themselves", "what", "which", "who", "whom", "this", "that",
                "these","doesn't", "all","it's",
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
                "we'll", "didn't", "they'll",
                "the","haven’t", "you’ve", "they’ve", "we’ve", "i’ve", "you’ll", "they’ll", "we’ll", "i’ll", "you’re",
                "they’re", "we’re", "i’m", "you’ve", "they’ve", "we’ve", "doesn’t","https","t","co"
            };

        return stopWords;
    }
}