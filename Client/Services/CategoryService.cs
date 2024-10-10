using System.Net.Http.Json;
using LocationFinder.Shared.Domain.Entities;

namespace LocationFinder.Client.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Category>>("api/category");
            return response ?? new List<Category>();
        }

        public async Task AddCategoryAsync(string name, Guid? parentId)
        {
            var requestUrl = $"api/category/add?name={Uri.EscapeDataString(name)}";

            if (parentId.HasValue)
            {
                requestUrl += $"&parentId={parentId}";
            }

            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to add category: {response.ReasonPhrase}");
            }
        }
    }
}