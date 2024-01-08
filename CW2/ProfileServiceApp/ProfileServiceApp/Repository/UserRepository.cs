using ProfileServiceApp.Data;
using ProfileServiceApp.Interfaces;
using ProfileServiceApp.Models;

namespace ProfileServiceApp.Repository
{
    public class UserRepository : UserInterface
    {
        private readonly DataContext _context;

        // Constructor to initialize the repository with a DataContext
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        // Create a new user
        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save(); // Save changes to the database
        }

        // Delete a user
        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save(); // Save changes to the database
        }

        // Retrieve a user by ID
        public User GetUser(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == userId);
        }

        // Retrieve users associated with a profile
        public ICollection<User> GetUserOfAProfile(int profileId)
        {
            return _context.ProfileUsers.Where(p => p.Profile.Id == profileId).Select(u => u.User).ToList();
        }

        // Retrieve all users
        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        // Retrieve profiles associated with a user
        public ICollection<Profile> GetProfileByUser(int userId)
        {
            return _context.ProfileUsers.Where(p => p.User.Id == userId).Select(p => p.Profile).ToList();
        }

        // Check if a user with a specific ID exists
        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }

        // Save changes to the database
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; // Check if changes were successfully saved
        }

        // Update a user's information
        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save(); // Save changes to the database
        }
    }
}
