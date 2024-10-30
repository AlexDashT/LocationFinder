using LocationFinder.Server.Core.Interfaces;
using LocationFinder.Shared.Domain.Dtos;
using LocationFinder.Shared.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LocationFinder.Server.Controllers
{
    // Controllers/LocationController.cs
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;

        public LocationController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        // GET: api/location
        [HttpGet]
        public async Task<ActionResult<List<Location>>> GetAllLocations()
        {
            var locations = await _locationRepository.GetAllLocationsAsync();
            return Ok(locations);
        }

        // POST: api/location
        [HttpPost]
        public async Task<ActionResult> AddLocation([FromBody] LocationDto locationDto)
        {
            if (locationDto == null || string.IsNullOrEmpty(locationDto.Description))
            {
                return BadRequest("Invalid location data.");
            }

            var location = new Location
            {
                CategoryId = locationDto.CategoryId,
                Latitude = locationDto.Latitude,
                Longitude = locationDto.Longitude,
                Description = locationDto.Description,
            };
            await _locationRepository.AddLocationAsync(location);
            await _locationRepository.SaveChangesAsync();

            return Ok();
        }
    }


}
