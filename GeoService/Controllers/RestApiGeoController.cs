using GeoService.ApiClients;
using GeoService.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeoService.Controllers
{
    [ApiController]
    [Route("api/rest/[action]")]
    //https://localhost:7259/api/rest/krak/3
    public class RestApiGeoController : ControllerBase
    {
        private readonly IGeoApiClient _geoApiClient;

        public RestApiGeoController(IGeoApiClient geoApiClient)
        {
            _geoApiClient = geoApiClient;
        }

        [HttpGet("{name}/{maxCount}", Name = "GetCities")]
        public async Task<ActionResult<CityDetailsResponse>> GetCities(string name, int maxCount, int? delayRequest)
        {
            var response = await _geoApiClient.GetCitiesAsync(name, maxCount, delayRequest);
            if (response == null)
                return NotFound();

            return Ok(response);
        }
    }
}
