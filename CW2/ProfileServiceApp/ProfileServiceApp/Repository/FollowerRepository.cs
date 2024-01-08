using ProfileServiceApp.Data;
using ProfileServiceApp.Interfaces;
using ProfileServiceApp.Models;

namespace ProfileServiceApp.Repository
{
    public class FollowerRepository : FollowerInterface
    {
        private DataContext _context;

        // Constructor to initialize the repository with a DataContext
        public FollowerRepository(DataContext context)
        {
            _context = context;
        }

        // Check if a follower with a specific ID exists
        public bool FollowerExists(int id)
        {
            return _context.Followers.Any(f => f.Id == id);
        }

        // Create a new follower
        public bool CreateFollower(Follower follower)
        {
            _context.Add(follower);
            return Save(); // Save changes to the database
        }

        // Delete a follower
        public bool DeleteFollower(Follower follower)
        {
            _context.Remove(follower);
            return Save(); // Save changes to the database
        }

        // Retrieve all followers
        public ICollection<Follower> GetFollowers()
        {
            return _context.Followers.ToList();
        }

        // Retrieve a follower by ID
        public Follower GetFollower(int id)
        {
            return _context.Followers.FirstOrDefault(f => f.Id == id);
        }

        // Save changes to the database
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; // Check if changes were successfully saved
        }

        // Update a follower's information
        public bool UpdateFollower(Follower follower)
        {
            _context.Update(follower);
            return Save(); // Save changes to the database
        }
    }
}
