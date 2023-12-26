
using Domain.Models;

namespace Domain.EntityFramework.Configurations
{
    public class StationRoute
    {
        public Station Station { get; set; }
        public Route Route { get; set; }

        public int StationId { get; set; }
        public int RouteId { get; set; }
        
        public int Order { get; set; }
    }
}
