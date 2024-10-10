using LocationFinder.Server.Core.Interfaces;
using LocationFinder.Shared.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LocationFinder.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("add")]
        public async Task<ActionResult> AddCategory(string name, Guid? parentId)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Category name is required.");
            }

            // Create a new category and generate a GUID for it
            var newCategory = new Category
            {
                Id = Guid.NewGuid(),
                Name = name,
                ParentId = parentId
            };

            try
            {
                await _categoryRepository.AddCategoryAsync(newCategory);
                await _categoryRepository.SaveChangesAsync();
                return Ok(newCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}