using LocationFinder.Shared.Domain.Entities;

namespace LocationFinder.Server.Core.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task AddCategoryAsync(Category category);
    Task SaveChangesAsync();
}