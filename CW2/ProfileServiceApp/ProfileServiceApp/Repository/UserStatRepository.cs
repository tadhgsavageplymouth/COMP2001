using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        // Create a new UserStat entity
        public bool CreateUserStat(UserStat userStat)
        {
            _context.Add(userStat);
            return Save(); // Save changes to the database
        }

        // Delete a UserStat entity
        public bool DeleteUserStat(UserStat userStat)
        {
            _context.Remove(userStat);
            return Save(); // Save changes to the database
        }

        // Retrieve a UserStat by its ID, including associated Trails
        public UserStat GetUserStat(int userStatId)
        {
            return _context.UserStats
                .Where(u => u.Id == userStatId)
                .Include(e => e.Trails) // Include Trails related to the UserStat
                .FirstOrDefault();
        }

        // Retrieve all UserStats
        public ICollection<UserStat> GetUserStats()
        {
            return _context.UserStats.ToList();
        }

        // Retrieve Trails associated with a UserStat
        public ICollection<Trail> GetTrailsByUserStat(int userStatId)
        {
            return _context.Trails
                .Where(t => t.UserStat.Id == userStatId)
                .ToList();
        }

        // Check if a UserStat with a specific ID exists
        public bool UserStatExists(int userStatId)
        {
            return _context.UserStats.Any(u => u.Id == userStatId);
        }

        // Save changes to the database
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; // Check if changes were successfully saved
        }

        // Update a UserStat entity
        public bool UpdateUserStat(UserStat userStat)
        {
            _context.Update(userStat);
            return Save(); // Save changes to the database
        }
    }
}
