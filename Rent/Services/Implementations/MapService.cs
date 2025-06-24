using System.Text.Json;
using System.Web;
using Data.DBContexts;
using Data.Dtos;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;

namespace Services.Implementations;

public class MapService : IMapService
{
    private readonly AppDBContext _context;
    private readonly IConfiguration _configuration;

    public MapService(AppDBContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    
    public async Task<object> GetCoordinate(string address)
    {
        try
        {
            return await GetAddressCoordinates(address);
        }
        catch (Exception e)
        {
            return $"An error occurred: {e.Message}";
        }
    }


    public async Task<object> GetDistanceAndTime(string origin, string destination)
    {
        try
        {
            var originCoordinates = await GetAddressCoordinates(origin);
            var destinationCoordinates = await GetAddressCoordinates(destination);
            
            if (originCoordinates == null || destinationCoordinates == null ||
                originCoordinates.lat == 0 || originCoordinates.lng == 0 ||
                destinationCoordinates.lat == 0 || destinationCoordinates.lng == 0)
            {
                throw new Exception("Invalid coordinates returned from address lookup.");
            }
            
            var originArray = new[] { originCoordinates.lat, originCoordinates.lng };
            var destinationArray = new[] { destinationCoordinates.lat, destinationCoordinates.lng };
            
            try
            {
                return await GetDistanceDirection(originArray, destinationArray);
            }
            catch (Exception e)
            {
                return $"An error occurred: {e.Message}";
            }
        }
        catch (Exception e)
        {
            return $"An error occurred: {e.Message}";
        }
    }


    private async Task<CoordinateDto> GetAddressCoordinates(string address)
    {
        var apiKey = _configuration["MapApiKey"];
        try
        {
            var query = $"{address}, Bangladesh";
            var url = "https://api.opencagedata.com/geocode/v1/json?" +
                      $"q={HttpUtility.UrlEncode(query)}" +
                      $"&key={apiKey}" +
                      "&limit=5" +
                      "&language=en";

            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(json);

            var root = document.RootElement;
            if (root.TryGetProperty("status", out var status) && 
                status.GetProperty("code").GetInt32() == 200 &&
                root.TryGetProperty("results", out var results) && 
                results.GetArrayLength() > 0)
            {
                var geometry = results[0].GetProperty("geometry");
                return new CoordinateDto
                {
                    lat = geometry.GetProperty("lat").GetDouble(),
                    lng = geometry.GetProperty("lng").GetDouble()
                };
            }

            throw new Exception("No coordinates found for the given address.");
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while fetching coordinates: {e.Message}");
        }
    }

    private async Task<DistanceTimeDto> GetDistanceDirection(double[] origin, double[] destination)
    {
        var OpenRoutesServiceKey =  _configuration["OpenRoutesServiceKey"];
        
        if (origin == null || destination == null || origin.Length != 2 || destination.Length != 2)
        {
            throw new ArgumentException("Origin and destination must be arrays of [longitude, latitude]");
        }
        if (!origin.Concat(destination).All(num => !double.IsNaN(num) && !double.IsInfinity(num)))
        {
            throw new ArgumentException("Coordinates must be valid numbers");
        }
        // if (origin[0] < -180 || origin[0] > 180 || origin[1] < -90 || origin[1] > 90 ||
        //    destination[0] < -180 || destination[0] > 180 || destination[1] < -90 || destination[1] > 90)
        // {
        //     throw new ArgumentException("Coordinates out of valid range: longitude [-180, 180], latitude [-90, 90]");
        // }
        
        try
        {
            var url = $"https://api.openrouteservice.org/v2/directions/driving-car?" +
                      $"start={origin[1]},{origin[0]}" +
                      $"&end={destination[1]},{destination[0]}";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", OpenRoutesServiceKey);
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(json);
            var root = document.RootElement;
            if (root.TryGetProperty("features", out var features) && features.GetArrayLength() > 0)
            {
                var feature = features[0];
                if (feature.TryGetProperty("properties", out var properties) &&
                    properties.TryGetProperty("summary", out var summary))
                {
                    var distance = summary.GetProperty("distance").GetDouble() / 1000; // Convert to km
                    var duration = summary.GetProperty("duration").GetDouble() / 60; // Convert to minutes

                    return new DistanceTimeDto
                    {
                        DistanceKm = distance,
                        EstimatedTimeMinutes = duration
                    };
                }
            }

            throw new Exception("No route found between the given coordinates.");
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while fetching distance and time: {e.Message}");
        }
    }
}