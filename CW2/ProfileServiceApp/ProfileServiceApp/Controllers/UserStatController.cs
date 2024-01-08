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
    public class UserStatController : Controller
    {
        private readonly UserStatInterface _userStatRepository;
        private readonly IMapper _mapper;

        public UserStatController(UserStatInterface userStatRepository, IMapper mapper)
        {
            _userStatRepository = userStatRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserStat>))]
        public IActionResult GetUserStats()
        {
            // Retrieve user stats and map them to DTOs
            var userStats = _mapper.Map<List<UserStatDto>>(_userStatRepository.GetUserStats());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userStats);
        }

        [HttpGet("{userStatId}")]
        [ProducesResponseType(200, Type = typeof(UserStat))]
        [ProducesResponseType(400)]
        public IActionResult GetUserStat(int userStatId)
        {
            // Check if the user stat with the given ID exists
            if (!_userStatRepository.UserStatExists(userStatId))
                return NotFound(); // Return a 404 Not Found response

            // Retrieve the user stat and map it to a DTO
            var userStat = _mapper.Map<UserStatDto>(_userStatRepository.GetUserStat(userStatId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userStat);
        }

        [HttpGet("{userStatId}/trails")]
        public IActionResult GetTrailsByAUserStat(int userStatId)
        {
            // Check if the user stat with the given ID exists
            if (!_userStatRepository.UserStatExists(userStatId))
                return NotFound(); // Return a 404 Not Found response

            // Retrieve trails associated with the specified user stat and map them to DTOs
            var trails = _mapper.Map<List<TrailDto>>(
                _userStatRepository.GetTrailsByUserStat(userStatId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(trails);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUserStat([FromBody] UserStatDto userStatCreate)
        {
            if (userStatCreate == null)
                return BadRequest(ModelState);

            // Check if a user stat with the same last name already exists
            var userStats = _userStatRepository.GetUserStats()
                .Where(c => c.LastName.Trim().ToUpper() == userStatCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (userStats != null)
            {
                ModelState.AddModelError("", "UserStat already exists");
                return StatusCode(422, ModelState); // Return a 422 Unprocessable Entity response
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map the DTO to a UserStat object and attempt to create it
            var userStatMap = _mapper.Map<UserStat>(userStatCreate);

            if (!_userStatRepository.CreateUserStat(userStatMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState); // Return a 500 Internal Server Error response
            }

            return Ok("Successfully created");
        }

        [HttpPut("{userStatId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUserStat(int userStatId, [FromBody] UserStatDto updatedUserStat)
        {
            if (updatedUserStat == null)
                return BadRequest(ModelState);

            // Check if the provided user stat ID matches the request URL
            if (userStatId != updatedUserStat.Id)
                return BadRequest(ModelState);

            // Check if the user stat with the given ID exists
            if (!_userStatRepository.UserStatExists(userStatId))
                return NotFound(); // Return a 404 Not Found response

            if (!ModelState.IsValid)
                return BadRequest();

            // Map the DTO to a UserStat object and attempt to update it
            var userStatMap = _mapper.Map<UserStat>(updatedUserStat);

            if (!_userStatRepository.UpdateUserStat(userStatMap))
            {
                ModelState.AddModelError("", "Something went wrong updating UserStat");
                return StatusCode(500, ModelState); // Return a 500 Internal Server Error response
            }

            return NoContent(); // Return a 204 No Content response
        }

        [HttpDelete("{userStatId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUserStat(int userStatId)
        {
            // Check if the user stat with the given ID exists
            if (!_userStatRepository.UserStatExists(userStatId))
            {
                return NotFound(); // Return a 404 Not Found response
            }

            // Retrieve the user stat to be deleted
            var userStatToDelete = _userStatRepository.GetUserStat(userStatId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Attempt to delete the user stat
            if (!_userStatRepository.DeleteUserStat(userStatToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting UserStat");
            }

            return NoContent(); // Return a 204 No Content response
        }
    }
}
