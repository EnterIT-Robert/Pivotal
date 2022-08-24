using GeoService.Models;

namespace GeoService.ApiClients
{
    public interface IGeoApiClient
    {
        Task<CityDetailsResponse> GetCitiesAsync(string name, int maxCount, int? delayRequest);
    }
}
