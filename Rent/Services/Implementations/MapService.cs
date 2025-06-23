using System.Text.Json;
using System.Web;
using Data.DBContexts;
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


    private async Task<object> GetAddressCoordinates(string address)
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
                var lat = geometry.GetProperty("lat").GetDouble();
                var lng = geometry.GetProperty("lng").GetDouble();
                return new { lat, lng };
            }

            throw new Exception("No coordinates found for the given address.");
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while fetching coordinates: {e.Message}");
        }
    }
}