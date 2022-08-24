using System.Runtime.Serialization;

namespace GeoService.Models
{
    [DataContract]
    public class CityDetailsResponse
    {
        [DataMember]
        public List<CityDetails> Data { get; init; } = new();

        [DataMember]
        public int RecordCount { get; set; }

        [DataMember]
        public DateTime Timestamp { get; set; }

    }
}
