using System.Runtime.Serialization;

namespace GeoService.Models
{
    [DataContract]
    public class CityDetails
    {
        [DataMember]
        public string? CityId { get; set; }
        [DataMember]
        public string? CityType { get; set; }
        [DataMember]
        public string? Country { get; set; }
        [DataMember]
        public string? CountryCode { get; set; }
        [DataMember]
        public int? Elevation { get; set; }
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int? Latitude { get; set; }
        [DataMember]
        public int? Longitude { get; set; }
        [DataMember]
        public string? Name { get; set; }
        [DataMember]
        public int? Population { get; set; }
        [DataMember]
        public string? Region { get; set; }
        [DataMember]
        public string? RegionCode { get; set; }
        [DataMember]
        public string? Timezone { get; set; }

    }
}
