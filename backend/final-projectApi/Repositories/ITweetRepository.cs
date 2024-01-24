using System.Collections.Generic;
using final_projectAPI.Models;

namespace final_projectAPI.Repositories;

public interface ITweetRepository
{
    IEnumerable<Tweet> GetAllTweet();
    Tweet? GetTweetById(int tweetId);
    Tweet CreateTweet(Tweet newTweet);
    Tweet? UpdateTweet(Tweet newTweet);
    void DeleteTweetById(int tweetId);

}