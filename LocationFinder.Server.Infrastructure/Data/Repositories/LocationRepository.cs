using LocationFinder.Server.Core.Interfaces;
using LocationFinder.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LocationFinder.Server.Infrastructure.Data.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly ApplicationDbContext _context;

    public LocationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Location>> GetAllLocationsAsync()
    {
        return await _context.Locations
                             .Include(l => l.Category) // Include the category if needed
                             .ToListAsync();
    }

    public async Task AddLocationAsync(Location location)
    {
        await _context.Locations.AddAsync(location);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}