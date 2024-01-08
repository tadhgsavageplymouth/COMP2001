using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfileServiceApp.Dto;
using ProfileServiceApp.Interfaces;
using ProfileServiceApp.Models;
using System.Collections.Generic; // Importing the List collection type
using System.Linq; // Importing LINQ for query operations

namespace ProfileServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserInterface _userRepository;
        private readonly FollowingInterface _followingRepository;
        private readonly IMapper _mapper;

        public UserController(UserInterface userRepository,
            FollowingInterface followingRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _followingRepository = followingRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            // Retrieve users and map them to DTOs
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int userId)
        {
            // Check if the user with the given ID exists
            if (!_userRepository.UserExists(userId))
                return NotFound(); // Return a 404 Not Found response

            // Retrieve the user and map it to a DTO
            var user = _mapper.Map<UserDto>(_userRepository.GetUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet("{userId}/profile")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetProfileByUser(int userId)
        {
            // Check if the user with the given ID exists
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            // Retrieve profiles associated with the specified user and map them to DTOs
            var profiles = _mapper.Map<List<ProfileDto>>(
                _userRepository.GetProfileByUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(profiles);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromQuery] int followingId, [FromBody] UserDto userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);

            // Check if a user with the same last name already exists
            var users = _userRepository.GetUsers()
                .Where(c => c.LastName.Trim().ToUpper() == userCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (users != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState); // Return a 422 Unprocessable Entity response
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map the DTO to a User object and attempt to create it
            var userMap = _mapper.Map<User>(userCreate);

            userMap.Following = _followingRepository.GetFollowing(followingId);

            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState); // Return a 500 Internal Server Error response
            }

            return Ok("Successfully created");
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto updatedUser)
        {
            if (updatedUser == null)
                return BadRequest(ModelState);

            // Check if the provided user ID matches the request URL
            if (userId != updatedUser.Id)
                return BadRequest(ModelState);

            // Check if the user with the given ID exists
            if (!_userRepository.UserExists(userId))
                return NotFound(); // Return a 404 Not Found response

            if (!ModelState.IsValid)
                return BadRequest();

            // Map the DTO to a User object and attempt to update it
            var userMap = _mapper.Map<User>(updatedUser);

            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong updating user");
                return StatusCode(500, ModelState); // Return a 500 Internal Server Error response
            }

            return NoContent(); // Return a 204 No Content response
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            // Check if the user with the given ID exists
            if (!_userRepository.UserExists(userId))
            {
                return NotFound(); // Return a 404 Not Found response
            }

            // Retrieve the user to be deleted
            var userToDelete = _userRepository.GetUser(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Attempt to delete the user
            if (!_userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting user");
            }

            return NoContent(); // Return a 204 No Content response
        }
    }
}
