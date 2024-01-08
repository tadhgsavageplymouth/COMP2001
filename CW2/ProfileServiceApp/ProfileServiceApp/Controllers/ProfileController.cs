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
    public class ProfileController : Controller
    {
        private readonly ProfileInterface _profileRepository;
        private readonly TrailInterface _trailRepository;
        private readonly IMapper _mapper;

        public ProfileController(ProfileInterface profileRepository,
            TrailInterface trailRepository,
            IMapper mapper)
        {
            _profileRepository = profileRepository;
            _trailRepository = trailRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Profile>))]
        public IActionResult GetProfiles()
        {
            // Retrieve profiles and map them to DTOs
            var profiles = _mapper.Map<List<ProfileDto>>(_profileRepository.GetProfiles());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(profiles);
        }

        [HttpGet("{profileId}")]
        [ProducesResponseType(200, Type = typeof(Profile))]
        [ProducesResponseType(400)]
        public IActionResult GetProfile(int profileId)
        {
            // Check if the profile with the given ID exists
            if (!_profileRepository.ProfileExists(profileId))
                return NotFound(); // Return a 404 Not Found response

            // Retrieve the profile and map it to a DTO
            var profile = _mapper.Map<ProfileDto>(_profileRepository.GetProfile(profileId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(profile);
        }

        [HttpGet("{profileId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetProfileRating(int profileId)
        {
            // Check if the profile with the given ID exists
            if (!_profileRepository.ProfileExists(profileId))
                return NotFound(); // Return a 404 Not Found response

            // Retrieve the profile's rating
            var rating = _profileRepository.GetProfileRating(profileId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProfile([FromQuery] int userId, [FromQuery] int followerId, [FromBody] ProfileDto profileCreate)
        {
            if (profileCreate == null)
                return BadRequest(ModelState);

            // Check if a profile with the same user ID already exists
            var profiles = _profileRepository.GetProfileTrimToUpper(profileCreate);

            if (profiles != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState); // Return a 422 Unprocessable Entity response
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map the DTO to a Profile object and attempt to create it
            var profileMap = _mapper.Map<Profile>(profileCreate);

            if (!_profileRepository.CreateProfile(userId, followerId, profileMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState); // Return a 500 Internal Server Error response
            }

            return Ok("Successfully created");
        }

        [HttpPut("{profileId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProfile(int profileId,
            [FromQuery] int userId, [FromQuery] int followerId,
            [FromBody] ProfileDto updatedProfile)
        {
            if (updatedProfile == null)
                return BadRequest(ModelState);

            // Check if the provided profile ID matches the request URL
            if (profileId != updatedProfile.Id)
                return BadRequest(ModelState);

            // Check if the profile with the given ID exists
            if (!_profileRepository.ProfileExists(profileId))
                return NotFound(); // Return a 404 Not Found response

            if (!ModelState.IsValid)
                return BadRequest();

            // Map the DTO to a Profile object and attempt to update it
            var profileMap = _mapper.Map<Profile>(updatedProfile);

            if (!_profileRepository.UpdateProfile(userId, followerId, profileMap))
            {
                ModelState.AddModelError("", "Something went wrong updating user");
                return StatusCode(500, ModelState); // Return a 500 Internal Server Error response
            }

            return NoContent(); // Return a 204 No Content response
        }

        [HttpDelete("{profileId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProfile(int profileId)
        {
            // Check if the profile with the given ID exists
            if (!_profileRepository.ProfileExists(profileId))
            {
                return NotFound(); // Return a 404 Not Found response
            }

            // Retrieve trails associated with the profile to be deleted
            var trailsToDelete = _trailRepository.GetTrailsOfAProfile(profileId);

            // Retrieve the profile to be deleted
            var profileToDelete = _profileRepository.GetProfile(profileId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Attempt to delete the associated trails
            if (!_trailRepository.DeleteTrails(trailsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting trails");
            }

            // Attempt to delete the profile
            if (!_profileRepository.DeleteProfile(profileToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting user");
            }

            return NoContent(); // Return a 204 No Content response
        }
    }
}
