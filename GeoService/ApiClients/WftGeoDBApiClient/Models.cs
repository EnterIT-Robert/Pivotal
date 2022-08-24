namespace GeoService.ApiClients.WftGeoDBApiClient
{

    public class PopulatedPlacesResponse : BaseCollectionResponse
    {
        public List<PopulatedPlaceSummary> data { get; init; } = new();
    }

    public class PopulatedPlaceSummary
    {
        public string? country { get; set; }
        public string? countryCode { get; set; }
        public double? distance { get; set; }
        public int? id { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public string? name { get; set; }
        public int? population { get; set; }
        public string? region { get; set; }
        public string? regionCode { get; set; }
        public string? type { get; set; }
        public string? wikiDataId { get; set; }
    }

    public class PopulatedPlaceResponce : BaseResponse
    {
        public PopulatedPlaceDetails data { get; init; } = new();
    }

    public class PopulatedPlaceDetails
    {
        public string? country { get; set; }
        public string? countryCode { get; set; }
        public bool? deleted { get; set; }
        public int? elevationMeters { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public string? name { get; set; }
        public int? population { get; set; }
        public string? region { get; set; }
        public string? regionCode { get; set; }
        public string? timezone { get; set; }
        public string? type { get; set; }
        public string? wikiDataId { get; set; }
    }

    public class BaseCollectionResponse : BaseResponse
    {
        public List<Link> links { get; init; } = new();
        public Metadata? metadata { get; set; }
    }

    public class Link
    {
        public string? href { get; set; }
        public string? rel { get; set; }
    }

    public class Metadata
    {
        public int? currentOset { get; set; }
        public int? totalCount { get; set; }

    }

    public class BaseResponse
    {
        public List<Error> errors { get; init; } = new();
    }

    public class Error
    {
        public string? code { get; set; }
        public string? message { get; set; }
    }

}
