using System.Net.Http.Json;
using LocationFinder.Shared.Domain.Dtos;
using LocationFinder.Shared.Domain.Entities;

namespace LocationFinder.Client.Services;

public class LocationService
{
    private readonly HttpClient _httpClient;

    public LocationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Get all locations
    public async Task<List<Location>> GetAllLocationsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Location>>("api/location");
    }

    // Add a new location
    public async Task AddLocationAsync(LocationDto location)
    {
        var response = await _httpClient.PostAsJsonAsync("api/location", location);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to add location: {response.ReasonPhrase}");
        }
    }
}