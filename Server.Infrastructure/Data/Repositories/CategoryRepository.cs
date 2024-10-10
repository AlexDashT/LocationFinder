using LocationFinder.Server.Core.Interfaces;
using LocationFinder.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LocationFinder.Server.Infrastructure.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories
                             .Include(c => c.Subcategories)
                             .ToListAsync();
    }

    public async Task AddCategoryAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}