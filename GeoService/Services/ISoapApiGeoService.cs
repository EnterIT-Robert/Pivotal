using GeoService.Models;
using Microsoft.AspNetCore.Mvc;
using System.ServiceModel;

namespace GeoService.Services
{
    [ServiceContract]
    public interface ISoapApiGeoService
    {
        [OperationContract]
        Task<CityDetailsResponse> GetCities(string name, int maxCount, int? delayRequest);
    }
}
