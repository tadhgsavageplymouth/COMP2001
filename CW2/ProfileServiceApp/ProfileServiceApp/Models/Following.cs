namespace ProfileServiceApp.Models
{
    public class Following
    {
        public int UserID { get; set; } // User ID of the user being followed
        public int? NumberOfFollowing { get; set; } // Number of users being followed by this user

        public User User { get; set; } // User associated with this following

        public ICollection<Following> Followings { get; set; } // Collection of followings
    }
}
