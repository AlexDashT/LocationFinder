namespace LocationFinder.Shared.Domain.Dtos
{
    public class LocationDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }

        public Guid? CategoryId { get; set; }
    }
}
