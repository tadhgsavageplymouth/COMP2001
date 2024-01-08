using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProfileServiceApp.Dto;
using ProfileServiceApp.Interfaces;
using ProfileServiceApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProfileServiceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowerController : Controller
    {
        private readonly FollowerInterface _followerRepository;
        private readonly IMapper _mapper;

        public FollowerController(FollowerInterface followerRepository, IMapper mapper)
        {
            _followerRepository = followerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Follower>))]
        public IActionResult GetFollowers()
        {
            // Retrieve followers and map them to DTOs
            var followers = _mapper.Map<List<FollowerDto>>(_followerRepository.GetFollowers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(followers);
        }

        [HttpGet("{followerId}")]
        [ProducesResponseType(200, Type = typeof(Follower))]
        [ProducesResponseType(400)]
        public IActionResult GetFollower(int followerId)
        {
            // Check if the follower exists
            if (!_followerRepository.FollowerExists(followerId))
                return NotFound();

            // Retrieve the follower and map it to a DTO
            var follower = _mapper.Map<FollowerDto>(_followerRepository.GetFollower(followerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(follower);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateFollower([FromBody] FollowerDto followerCreate)
        {
            // Check if the request is valid
            if (followerCreate == null)
                return BadRequest(ModelState);

            // Check if the follower already exists
            var existingFollower = _followerRepository.GetFollowers()
                .FirstOrDefault(f => f.Name.Trim().ToUpper() == followerCreate.Name.TrimEnd().ToUpper());

            if (existingFollower != null)
            {
                ModelState.AddModelError("", "Follower already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map the DTO to a Follower object and attempt to create the follower
            var followerMap = _mapper.Map<Follower>(followerCreate);

            if (!_followerRepository.CreateFollower(followerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{followerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFollower(int followerId, [FromBody] FollowerDto updatedFollower)
        {
            // Check if the request is valid
            if (updatedFollower == null)
                return BadRequest(ModelState);

            if (followerId != updatedFollower.Id)
                return BadRequest(ModelState);

            // Check if the follower exists
            if (!_followerRepository.FollowerExists(followerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            // Map the DTO to a Follower object and attempt to update the follower
            var followerMap = _mapper.Map<Follower>(updatedFollower);

            if (!_followerRepository.UpdateFollower(followerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating follower");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{followerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFollower(int followerId)
        {
            // Check if the follower exists
            if (!_followerRepository.FollowerExists(followerId))
            {
                return NotFound();
            }

            // Retrieve the follower to delete
            var followerToDelete = _followerRepository.GetFollower(followerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Attempt to delete the follower
            if (!_followerRepository.DeleteFollower(followerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting follower");
            }

            return NoContent();
        }
    }
}
