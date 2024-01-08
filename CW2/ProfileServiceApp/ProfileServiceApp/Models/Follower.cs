namespace ProfileServiceApp.Models
{
    public class Follower
    {
        public int UserID { get; set; } // User ID of the follower
        public int? NumberOfFollowers { get; set; } // Number of followers for the user

        public User User { get; set; } // User associated with this follower

        public ICollection<Follower> Followers { get; set; } // Collection of followers
    }
}
