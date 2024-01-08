namespace ProfileServiceApp.Models
{
    public class UserStat
    {
        public int UserID { get; set; } // Identifier for the user
        public DateTime? MembershipStartDate { get; set; } // Start date of membership

        public User User { get; set; } // User associated with this user statistic

        public ICollection<UserStat> Userstats { get; set; } // Collection of user statistics
    }
}
