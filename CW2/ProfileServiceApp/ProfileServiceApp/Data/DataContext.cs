using System.Diagnostics.Metrics;
using Microsoft.EntityFrameworkCore;
using ProfileServiceApp.Models;

namespace ProfileServiceApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Follower> Followers { get; set; } // Database set for followers
        public DbSet<Following> Followings { get; set; } // Database set for followings
        public DbSet<User> Users { get; set; } // Database set for users
        public DbSet<Profile> Profiles { get; set; } // Database set for profiles
        public DbSet<UserProfile> UserProfiles { get; set; } // Database set for user profiles
        public DbSet<UserProfileStat> UserProfileStats { get; set; } // Database set for user profile statistics
    }
}
