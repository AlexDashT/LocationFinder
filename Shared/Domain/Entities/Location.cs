namespace LocationFinder.Shared.Domain.Entities;

public class Location : BaseEntity
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Description { get; set; }

    public Guid? CategoryId { get; set; } // Optional: associate a location with a category

    public virtual Category Category { get; set; } // Navigation property
}