using AutoMapper;
using ProfileServiceApp.Data;
using ProfileServiceApp.Interfaces;
using ProfileServiceApp.Models;

namespace ProfileServiceApp.Repository
{
    public class FollowingRepository : FollowingInterface
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        // Constructor to initialize the repository with a DataContext and IMapper
        public FollowingRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Check if a following with a specific ID exists
        public bool FollowingExists(int id)
        {
            return _context.Followings.Any(f => f.Id == id);
        }

        // Create a new following
        public bool CreateFollowing(Following following)
        {
            _context.Add(following);
            return Save(); // Save changes to the database
        }

        // Delete a following
        public bool DeleteFollowing(Following following)
        {
            _context.Remove(following);
            return Save(); // Save changes to the database
        }

        // Retrieve all followings
        public ICollection<Following> GetFollowings()
        {
            return _context.Followings.ToList();
        }

        // Retrieve a following by ID
        public Following GetFollowing(int id)
        {
            return _context.Followings.FirstOrDefault(f => f.Id == id);
        }

        // Retrieve a following by User ID
        public Following GetFollowingByUser(int userId)
        {
            return _context.Users.Where(u => u.Id == userId).Select(u => u.Following).FirstOrDefault();
        }

        // Retrieve users from a following
        public ICollection<User> GetUsersFromAFollowing(int followingId)
        {
            return _context.Users.Where(u => u.Following.Id == followingId).ToList();
        }

        // Save changes to the database
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; // Check if changes were successfully saved
        }

        // Update a following's information
        public bool UpdateFollowing(Following following)
        {
            _context.Update(following);
            return Save(); // Save changes to the database
        }
    }
}
