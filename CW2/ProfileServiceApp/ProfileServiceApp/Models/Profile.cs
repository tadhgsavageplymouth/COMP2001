namespace ProfileServiceApp.Models
{
    public class Profile
    {
        public int UserID { get; set; } // User ID associated with this profile
        public string Insights { get; set; } // Insights related to the profile
        public string ProfileFeed { get; set; } // Profile feed content

        public User User { get; set; } // User associated with this profile

        public ICollection<Profile> Profiles { get; set; } // Collection of profiles
    }
}
