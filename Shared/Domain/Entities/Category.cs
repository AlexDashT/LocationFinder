using System.Text.Json.Serialization;

namespace LocationFinder.Shared.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public Guid? ParentId { get; set; } // Nullable to allow top-level categories

    // Navigation property for subcategories
    public virtual IEnumerable<Category>? Subcategories { get; set; } = new List<Category>();

    // Navigation property for parent category
    [JsonIgnore] // Prevents circular reference during serialization
    public virtual Category? Parent { get; set; }
}