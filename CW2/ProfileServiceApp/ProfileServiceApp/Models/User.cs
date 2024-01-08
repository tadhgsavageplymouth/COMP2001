namespace ProfileServiceApp.Models
{
    public class User
    {
        public int UserID { get; set; } // Identifier for the user
        public string Username { get; set; } // User's username
        public string ProfilePhoto { get; set; } // User's profile photo
        public string Location { get; set; } // User's location

        public ICollection<User> Users { get; set; } // Collection of users
    }
}
