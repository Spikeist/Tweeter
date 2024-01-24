using final_projectAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace final_projectAPI.Migrations;

public class TweetDbContext : DbContext
{
    public DbSet<Tweet> Tweet { get; set; }
    public DbSet<User> Users { get; set; }

    public TweetDbContext(DbContextOptions<TweetDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tweet>(entity =>
        {
            entity.HasKey(e => e.User);
            entity.HasKey(e => e.TweetId);
            entity.Property(e => e.Content).IsRequired();
            entity.HasKey(e => e.TTime);

        });

        modelBuilder.Entity<Tweet>().HasData(
            new Tweet { 
                TweetId = 1,
                Content = "This is my first Tweet!",
                TTime = new DateTime(2022, 03, 17, 2, 01, 25)
            },
            new Tweet { 
                TweetId = 2,
                Content = "I am a full-stack developer!",
                TTime = new DateTime(2022, 03, 17, 2, 55, 57)
            },
            new Tweet { 
                TweetId = 3,
                Content = "omg this app is the coolest!!!!!!!",
                TTime = new DateTime(2022, 03, 17, 3, 06, 23)
            }
        );

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.Username).IsRequired();
            entity.HasIndex(x => x.Username).IsUnique();
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
            entity.Property(e => e.City).IsRequired();
            entity.Property(e => e.State).IsRequired();
            entity.Property(e => e.UTime).IsRequired();
        });
    }
}