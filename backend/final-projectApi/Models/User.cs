using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace final_projectAPI.Models;

public class User 
{
    [JsonIgnore]
    public int UserId { get; set; }

    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? City { get; set; }
    [Required]
    public string? State { get; set; }
    [Required]
    public DateTime UTime { get; set; }
}