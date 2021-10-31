using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using socialBrothersCase.Models;

namespace socialBrothersCase.ApiServices
{
    public class RadarService
    {
        private HttpClient _client;
        public RadarService(HttpClient client)
        {
            _client = client;
        }

        public async Coordinates GetCoordinates(Address address)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://api.radar.io/v1/geocode/forward?query=" + address.Street + "+" + address.HouseNumber + "+" + address.Location))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("", "mytoken");
            }
        }

        public void GetDistance(Coordinates startingPoint, Coordinates endPoint)
        {

        }
    }

    public class Coordinates
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class RadarForwardGeocodeResponse
    {
        [JsonPropertyName("addresses")]
        public Addresses addresses { get; set; }

        public class Addresses
        {
            [JsonPropertyName("latitude")]
            public string Latitude { get; set; }
            [JsonPropertyName("longitude")]
            public string Longitude { get; set; }
        }
    }
}
