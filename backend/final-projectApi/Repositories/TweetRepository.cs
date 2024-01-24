using final_projectAPI.Migrations;
using final_projectAPI.Models;

namespace final_projectAPI.Repositories;

public class TweetRepository : ITweetRepository 
{
    private readonly TweetDbContext _context;

    public TweetRepository(TweetDbContext context)
    {
        _context = context;
    }

    public Tweet CreateTweet(Tweet newTweet)
    {
        _context.Tweet.Add(newTweet);
        _context.SaveChanges();
        return newTweet;
    }

    public void DeleteTweetById(int tweetId)
    {
        var tweet = _context.Tweet.Find(tweetId);
        if (tweet != null) {
            _context.Tweet.Remove(tweet); 
            _context.SaveChanges(); 
        }
    }

    public IEnumerable<Tweet> GetAllTweet()
    {
        return _context.Tweet.ToList();
    }

    public Tweet? GetTweetById(int tweetId)
    {
        return _context.Tweet.SingleOrDefault(c => c.TweetId == tweetId);
    }

    public Tweet? UpdateTweet(Tweet newTweet)
    {
        var originalTweet = _context.Tweet.Find(newTweet.TweetId);
        if (originalTweet != null) {
            originalTweet.Content = newTweet.Content;
            originalTweet.TTime = newTweet.TTime;
            _context.SaveChanges();
        }
        return originalTweet;
    }
}