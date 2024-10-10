using LocationFinder.Shared.Domain.Entities;

namespace LocationFinder.Server.Core.Interfaces;

public interface ILocationRepository
{
    Task<List<Location>> GetAllLocationsAsync();
    Task AddLocationAsync(Location location);
    Task SaveChangesAsync();
}