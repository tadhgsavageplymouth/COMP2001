using ProfileServiceApp.Data;
using ProfileServiceApp.Dto;
using ProfileServiceApp.Interfaces;
using ProfileServiceApp.Models;

namespace ProfileServiceApp.Repository
{
    public class ProfileRepository : ProfileInterface
    {
        private readonly DataContext _context;

        // Constructor to initialize the repository with a DataContext
        public ProfileRepository(DataContext context)
        {
            _context = context;
        }

        // Create a new profile for a user and follower
        public bool CreateProfile(int userId, int followerId, Profile profile)
        {
            var userProfileEntity = _context.Users.FirstOrDefault(a => a.Id == userId);
            var follower = _context.Followers.FirstOrDefault(a => a.Id == followerId);

            // Create a UserProfile relationship
            var userProfile = new UserProfile()
            {
                User = userProfileEntity,
                Profile = profile,
            };
            _context.Add(userProfile);

            // Create a FollowerProfile relationship
            var followerProfile = new FollowerProfile()
            {
                Follower = follower,
                Profile = profile,
            };
            _context.Add(followerProfile);

            _context.Add(profile); // Add the profile entity

            return Save(); // Save changes to the database
        }

        // Delete a profile
        public bool DeleteProfile(Profile profile)
        {
            _context.Remove(profile);
            return Save(); // Save changes to the database
        }

        // Retrieve a profile by ID
        public Profile GetProfile(int id)
        {
            return _context.Profiles.FirstOrDefault(p => p.Id == id);
        }

        // Retrieve a profile by name
        public Profile GetProfile(string name)
        {
            return _context.Profiles.FirstOrDefault(p => p.Name == name);
        }

        // Calculate and return the rating of a profile based on associated trails
        public decimal GetProfileRating(int profileId)
        {
            var trails = _context.Trails.Where(p => p.Profile.Id == profileId);

            if (trails.Count() <= 0)
                return 0;

            return ((decimal)trails.Sum(r => r.Rating) / trails.Count());
        }

        // Retrieve all profiles
        public ICollection<Profile> GetProfiles()
        {
            return _context.Profiles.OrderBy(p => p.Id).ToList();
        }

        // Retrieve a profile by name (case-insensitive)
        public Profile GetProfileTrimToUpper(ProfileDto profileCreate)
        {
            return GetProfiles().FirstOrDefault(c => c.Name.Trim().ToUpper() == profileCreate.Name.TrimEnd().ToUpper());
        }

        // Check if a profile with a specific ID exists
        public bool ProfileExists(int profileId)
        {
            return _context.Profiles.Any(p => p.Id == profileId);
        }

        // Save changes to the database
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; // Check if changes were successfully saved
        }

        // Update a profile's information
        public bool UpdateProfile(int userId, int followerId, Profile profile)
        {
            _context.Update(profile);
            return Save(); // Save changes to the database
        }
    }
}
