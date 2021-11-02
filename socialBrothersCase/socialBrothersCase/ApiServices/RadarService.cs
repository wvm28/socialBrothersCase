using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using socialBrothersCase.Models;

namespace socialBrothersCase.ApiServices
{
    public class RadarService
    {
        private HttpClient _client;
        private IConfiguration _configuration;
        //Set up the client and the authorization header, api key can be found in
        public RadarService(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_configuration.GetSection("ApiKeys")["RadarApiKey"]);         
        }

        //Return the coordinates for the given address
        public async Task<Coordinates> GetCoordinatesAsync(Address address)
        {
            var response = _client.GetStreamAsync("https://api.radar.io/v1/geocode/forward?query=" + address.Street + "+" + address.HouseNumber + "+" + address.Location).Result;
            var radarForwardGeocodeResponse = await JsonSerializer.DeserializeAsync<RadarForwardGeocodeResponse>(response);
            return new Coordinates
            {
                Latitude = radarForwardGeocodeResponse.addresses.First().Latitude,
                Longitude = radarForwardGeocodeResponse.addresses.First().Longitude
            };

        }

        //Return the distance between the 2 given points
        public async Task<RadarRouteDistanceResponse> GetDistance(Coordinates startingPoint, Coordinates endPoint)
        {
            var response = _client.GetStreamAsync("https://api.radar.io/v1/route/distance?origin=" + 
                startingPoint.Latitude + 
                "," +
                startingPoint.Longitude +
                "&destination=" + 
                endPoint.Latitude +
                "," +
                endPoint.Longitude +
                "&modes=foot,car&units=metric").Result;
            return await JsonSerializer.DeserializeAsync<RadarRouteDistanceResponse>(response);

        }
    }

    public class Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class RadarForwardGeocodeResponse
    {
        [JsonPropertyName("addresses")]
        public List<Addresses> addresses { get; set; }

        public class Addresses
        {
            [JsonPropertyName("latitude")]
            public double Latitude { get; set; }
            [JsonPropertyName("longitude")]
            public double Longitude { get; set; }
        }
    }

    public class RadarRouteDistanceResponse
    {
        [JsonPropertyName("routes")]
        public Route route { get; set; }

        public class Route
        {
            [JsonPropertyName("foot")]
            public Mode modeFoot { get; set; }

            [JsonPropertyName("car")]
            public Mode modeCar { get; set; }

            public class Mode
            {
                [JsonPropertyName("distance")]
                public RadarReponseAtribute distance { get; set; }

                [JsonPropertyName("duration")]
                public RadarReponseAtribute duration { get; set; }
                public class RadarReponseAtribute
                {
                    [JsonPropertyName("value")]
                    public double value { get; set; }

                    [JsonPropertyName("text")]
                    public string text { get; set; }
                }
            }
            
        }
    }
}
