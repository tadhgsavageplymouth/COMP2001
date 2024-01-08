using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfileServiceApp.Dto;
using ProfileServiceApp.Interfaces;
using ProfileServiceApp.Models;
using System.Collections.Generic; // Importing the List collection type

namespace ProfileServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowingController : Controller
    {
        private readonly FollowingInterface _followingRepository;
        private readonly IMapper _mapper;

        public FollowingController(FollowingInterface followingRepository, IMapper mapper)
        {
            _followingRepository = followingRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Following>))]
        public IActionResult GetFollowings()
        {
            // Retrieve followings and map them to DTOs
            var followings = _mapper.Map<List<FollowingDto>>(_followingRepository.GetFollowings());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(followings);
        }

        [HttpGet("{followingId}")]
        [ProducesResponseType(200, Type = typeof(Following))]
        [ProducesResponseType(400)]
        public IActionResult GetFollowing(int followingId)
        {
            // Check if the following with the given ID exists
            if (!_followingRepository.FollowingExists(followingId))
                return NotFound(); // Return a 404 Not Found response

            // Retrieve the following and map it to a DTO
            var following = _mapper.Map<FollowingDto>(_followingRepository.GetFollowing(followingId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(following);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateFollowing([FromBody] FollowingDto followingCreate)
        {
            if (followingCreate == null)
                return BadRequest(ModelState);

            // Check if a following with the same name already exists
            var following = _followingRepository.GetFollowings()
                .Where(f => f.Name.Trim().ToUpper() == followingCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (following != null)
            {
                ModelState.AddModelError("", "Following already exists");
                return StatusCode(422, ModelState); // Return a 422 Unprocessable Entity response
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map the DTO to a Following object and attempt to create it
            var followingMap = _mapper.Map<Following>(followingCreate);

            if (!_followingRepository.CreateFollowing(followingMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState); // Return a 500 Internal Server Error response
            }

            return Ok("Successfully created");
        }

        [HttpPut("{followingId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFollowing(int followingId, [FromBody] FollowingDto updatedFollowing)
        {
            if (updatedFollowing == null)
                return BadRequest(ModelState);

            // Check if the provided following ID matches the request URL
            if (followingId != updatedFollowing.Id)
                return BadRequest(ModelState);

            // Check if the following with the given ID exists
            if (!_followingRepository.FollowingExists(followingId))
                return NotFound(); // Return a 404 Not Found response

            if (!ModelState.IsValid)
                return BadRequest();

            // Map the DTO to a Following object and attempt to update it
            var followingMap = _mapper.Map<Following>(updatedFollowing);

            if (!_followingRepository.UpdateFollowing(followingMap))
            {
                ModelState.AddModelError("", "Something went wrong updating following");
                return StatusCode(500, ModelState); // Return a 500 Internal Server Error response
            }

            return NoContent(); // Return a 204 No Content response
        }

        [HttpDelete("{followingId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFollowing(int followingId)
        {
            // Check if the following with the given ID exists
            if (!_followingRepository.FollowingExists(followingId))
            {
                return NotFound(); // Return a 404 Not Found response
            }

            // Retrieve the following to delete
            var followingToDelete = _followingRepository.GetFollowing(followingId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Attempt to delete the following
            if (!_followingRepository.DeleteFollowing(followingToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting following");
            }

            return NoContent(); // Return a 204 No Content response
        }
    }
}
