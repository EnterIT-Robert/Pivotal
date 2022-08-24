using GeoService.Models;
using System.Net.Http.Headers;

namespace GeoService.ApiClients.WftGeoDBApiClient
{
    public class WftGeoDBApiCLient : IGeoApiClient
    {
        private const string UriCities = "/v1/geo/cities";
        private const string GeoEndpoint = "https://wft-geo-db.p.rapidapi.com";
        private const string HostHeader = "X-RapidAPI-Host";
        private const string HostHeaderValue = "wft-geo-db.p.rapidapi.com";
        private const string KeyHeader = "X-RapidAPI-Key";
        private const string KeyHeaderValue = "f0bd87afe4msh06ba220f660631dp18a49ejsne3ec020eb064";
        private const int defaultDelayRequest = 100;

        private readonly HttpClient _httpClient;

        private DateTime? tsRequest = null;

        public WftGeoDBApiCLient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
            this._httpClient.BaseAddress = new Uri(GeoEndpoint);
            var headers = this._httpClient.DefaultRequestHeaders;
            headers.Accept.Clear();
            headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            headers.Add(HostHeader, HostHeaderValue);
            headers.Add(KeyHeader, KeyHeaderValue);
        }       

        public async Task<CityDetailsResponse> GetCitiesAsync(string name, int maxCount, int? delayRequest)
        {
            
            int limit = 10;
            int offset = 0;

            if (delayRequest == null)
                delayRequest = defaultDelayRequest;

            CityDetailsResponse result = new CityDetailsResponse();
            result.RecordCount = 0;
            result.Timestamp = DateTime.UtcNow.ToLocalTime();

            bool endOfLoop = false;

            // loop ByOffset
            while (!endOfLoop)
            {
                if (tsRequest != null && tsRequest.Value.AddMilliseconds(delayRequest.Value) > DateTime.Now)
                {
                    TimeSpan ts = tsRequest.Value.AddMilliseconds(delayRequest.Value) - DateTime.Now;
                    await Task.Delay((int)ts.TotalMilliseconds);
                }
                var cities = await this._httpClient.GetFromJsonAsync<PopulatedPlacesResponse>($"{UriCities}?limit={limit}&offset={offset}&namePrefix={name}");

                tsRequest = DateTime.Now;

                if (cities == null || cities.data == null || cities.data.Count == 0)
                    endOfLoop = true;
                else
                {
                    foreach (var city in cities.data)
                    {
                        result.Data.Add(GetResultData(city));
                        result.RecordCount++;
                        if (result.RecordCount == maxCount)
                        {
                            endOfLoop = true;
                            break;
                        }
                    }
                    offset += cities.data.Count;
                }
            }

            if (result.RecordCount == 0)
                return result;

            // loop ByCity
            foreach(var city in result.Data)
            {
                if (tsRequest!.Value.AddMilliseconds(delayRequest.Value) > DateTime.Now)
                {
                    TimeSpan ts = tsRequest.Value.AddMilliseconds(delayRequest.Value) - DateTime.Now;
                    await Task.Delay((int)ts.TotalMilliseconds);
                }
                var cityDetails = await this._httpClient.GetFromJsonAsync<PopulatedPlaceResponce>($"{UriCities}/{city.Id}");
                tsRequest = DateTime.Now;

                if (cityDetails == null || cityDetails.data == null)
                    continue;

                city.Elevation = cityDetails.data.elevationMeters;
                city.Timezone = cityDetails.data.timezone;
            }            

            return result;
        }

        private CityDetails GetResultData(PopulatedPlaceSummary data)
        {
            CityDetails cd = new CityDetails()
            {
                Id = data.id,
                CityId = data.wikiDataId,
                CityType = data.type,
                Name = data.name,
                Country = data.country,
                CountryCode = data.countryCode,
                Region = data.region,
                RegionCode = data.regionCode,
                Latitude = data.longitude != null ? GetDegreesToMiliseconds(data.longitude.Value) : null,
                Longitude = data.longitude != null ? GetDegreesToMiliseconds(data.longitude.Value) : null,
                Population = data.population                
            };
            return cd;
        }

        private int GetDegreesToMiliseconds(double degrees)
        {
            int sign = degrees < 0 ? -1 : 1;
            degrees = sign * degrees;
            int deg = (int)degrees;
            double tmp = (degrees - deg) * 60.0;
            int min = (int)tmp;
            tmp = (tmp - min) * 60;
            int sec = (int)tmp;
            tmp = (tmp - sec) * 1000;
            int msec = (int)tmp;

            return sign * (msec + sec * 1000 + min * 60000 + deg * 3600000);
        }
    }
}
