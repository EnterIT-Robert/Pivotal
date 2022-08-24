using GeoService.ApiClients;
using GeoService.Models;

namespace GeoService.Services
{
    public class SoapApiGeoService : ISoapApiGeoService
    {
        private readonly IGeoApiClient _geoApiClient;

        public SoapApiGeoService(IGeoApiClient geoApiClient)
        {
            _geoApiClient = geoApiClient; 
        }

        public async Task<CityDetailsResponse> GetCities(string name, int maxCount, int? delayRequest)
        {
            var response = await _geoApiClient.GetCitiesAsync(name, maxCount, delayRequest);
            
            return response;
        }
    }
}
