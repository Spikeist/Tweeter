using System.ComponentModel.DataAnnotations;

namespace final_projectAPI.Models;

public class Tweet 
{
    public User? User { get; set; }
    public int TweetId { get; set; }
    [Required]
    public string? Content { get; set; }

    [Required]
    public DateTime TTime { get; set; }
}