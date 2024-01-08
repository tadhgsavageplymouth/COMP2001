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
    public class TrailController : Controller
    {
        private readonly TrailInterface _trailRepository;
        private readonly IMapper _mapper;
        private readonly UserStatInterface _userStatRepository;
        private readonly ProfileInterface _profileRepository;

        public TrailController(TrailInterface trailRepository,
            IMapper mapper,
            ProfileInterface profileRepository,
            UserStatInterface userStatRepository)
        {
            _trailRepository = trailRepository;
            _mapper = mapper;
            _userStatRepository = userStatRepository;
            _profileRepository = profileRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Trail>))]
        public IActionResult GetTrails()
        {
            // Retrieve trails and map them to DTOs
            var trails = _mapper.Map<List<TrailDto>>(_trailRepository.GetTrails());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(trails);
        }

        [HttpGet("{trailId}")]
        [ProducesResponseType(200, Type = typeof(Trail))]
        [ProducesResponseType(400)]
        public IActionResult GetTrail(int trailId)
        {
            // Check if the trail with the given ID exists
            if (!_trailRepository.TrailExists(trailId))
                return NotFound(); // Return a 404 Not Found response

            // Retrieve the trail and map it to a DTO
            var trail = _mapper.Map<TrailDto>(_trailRepository.GetTrail(trailId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(trail);
        }

        [HttpGet("profile/{profileId}")]
        [ProducesResponseType(200, Type = typeof(Trail))]
        [ProducesResponseType(400)]
        public IActionResult GetTrailsForAProfile(int profileId)
        {
            // Retrieve trails associated with the specified profile and map them to DTOs
            var trails = _mapper.Map<List<TrailDto>>(_trailRepository.GetTrailsOfAProfile(profileId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(trails);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTrail([FromQuery] int userStatId, [FromQuery] int profileId, [FromBody] TrailDto trailCreate)
        {
            if (trailCreate == null)
                return BadRequest(ModelState);

            // Check if a trail with the same title already exists
            var trails = _trailRepository.GetTrails()
                .Where(c => c.Title.Trim().ToUpper() == trailCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (trails != null)
            {
                ModelState.AddModelError("", "Trail already exists");
                return StatusCode(422, ModelState); // Return a 422 Unprocessable Entity response
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map the DTO to a Trail object and attempt to create it
            var trailMap = _mapper.Map<Trail>(trailCreate);

            // Assign the profile and userStat to the trail
            trailMap.Profile = _profileRepository.GetProfile(profileId);
            trailMap.UserStat = _userStatRepository.GetUserStat(userStatId);

            if (!_trailRepository.CreateTrail(trailMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState); // Return a 500 Internal Server Error response
            }

            return Ok("Successfully created");
        }

        [HttpPut("{trailId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTrail(int trailId, [FromBody] TrailDto updatedTrail)
        {
            if (updatedTrail == null)
                return BadRequest(ModelState);

            // Check if the provided trail ID matches the request URL
            if (trailId != updatedTrail.Id)
                return BadRequest(ModelState);

            // Check if the trail with the given ID exists
            if (!_trailRepository.TrailExists(trailId))
                return NotFound(); // Return a 404 Not Found response

            if (!ModelState.IsValid)
                return BadRequest();

            // Map the DTO to a Trail object and attempt to update it
            var trailMap = _mapper.Map<Trail>(updatedTrail);

            if (!_trailRepository.UpdateTrail(trailMap))
            {
                ModelState.AddModelError("", "Something went wrong updating trail");
                return StatusCode(500, ModelState); // Return a 500 Internal Server Error response
            }

            return NoContent(); // Return a 204 No Content response
        }

        [HttpDelete("{trailId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTrail(int trailId)
        {
            // Check if the trail with the given ID exists
            if (!_trailRepository.TrailExists(trailId))
            {
                return NotFound(); // Return a 404 Not Found response
            }

            // Retrieve the trail to be deleted
            var trailToDelete = _trailRepository.GetTrail(trailId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Attempt to delete the trail
            if (!_trailRepository.DeleteTrail(trailToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting trail");
            }

            return NoContent(); // Return a 204 No Content response
        }

        [HttpDelete("/DeleteTrailsByUserStat/{userStatId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTrailsByUserStat(int userStatId)
        {
            // Check if the userStat with the given ID exists
            if (!_userStatRepository.UserStatExists(userStatId))
                return NotFound(); // Return a 404 Not Found response

            // Retrieve trails associated with the userStat to be deleted
            var trailsToDelete = _userStatRepository.GetTrailsByUserStat(userStatId).ToList();

            if (!ModelState.IsValid)
                return BadRequest();

            // Attempt to delete the associated trails
            if (!_trailRepository.DeleteTrails(trailsToDelete))
            {
                ModelState.AddModelError("", "Error deleting trails");
                return StatusCode(500, ModelState); // Return a 500 Internal Server Error response
            }

            return NoContent(); // Return a 204 No Content response
        }
    }
}
