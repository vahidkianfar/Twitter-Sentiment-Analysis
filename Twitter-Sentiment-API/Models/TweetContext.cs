using Microsoft.EntityFrameworkCore;

namespace Twitter_Sentiment_API.Models;

public class TweetContext: DbContext
{ 
    public TweetContext(DbContextOptions<TweetContext> options) : base(options) 
    {
    }

    public DbSet<Tweets> Tweets { get; set; }
}