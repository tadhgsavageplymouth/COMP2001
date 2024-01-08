using AutoMapper;
using ProfileServiceApp.Data;
using ProfileServiceApp.Interfaces;
using ProfileServiceApp.Models;

namespace ProfileServiceApp.Repository
{
    public class UserStatRepository : UserStatInterface
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        // Constructor to initialize the repository with a DataContext and IMapper
        public UserStatRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create a new user statistic
        public bool CreateUserStat(UserStat userStat)
        {
            _context.Add(userStat);
            return Save(); // Save changes to the database
        }

        // Delete a user statistic
        public bool DeleteUserStat(UserStat userStat)
        {
            _context.Remove(userStat);
            return Save(); // Save changes to the database
        }

        // Delete a list of user statistics
        public bool DeleteUserStats(List<UserStat> userStats)
        {
            _context.RemoveRange(userStats);
            return Save(); // Save changes to the database
        }

        // Retrieve a user statistic by ID
        public UserStat GetUserStat(int userStatId)
        {
            return _context.UserStats.FirstOrDefault(r => r.Id == userStatId);
        }

        // Retrieve all user statistics
        public ICollection<UserStat> GetUserStats()
        {
            return _context.UserStats.ToList();
        }

        // Retrieve user statistics associated with a profile
        public ICollection<UserStat> GetUserStatsOfAProfile(int profileId)
        {
            return _context.UserStats.Where(r => r.Profile.Id == profileId).ToList();
        }

        // Check if a user statistic with a specific ID exists
        public bool UserStatExists(int userStatId)
        {
            return _context.UserStats.Any(r => r.Id == userStatId);
        }

        // Save changes to the database
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; // Check if changes were successfully saved
        }

        // Update a user statistic's information
        public bool UpdateUserStat(UserStat userStat)
        {
            _context.Update(userStat);
            return Save(); // Save changes to the database
        }
    }
}
