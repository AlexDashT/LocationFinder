using LocationFinder.Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
