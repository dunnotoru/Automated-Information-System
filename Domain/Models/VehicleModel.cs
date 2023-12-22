namespace Domain.Models
{
    public class VehicleModel : EntityBase
    {
        public string Name { get; set; }
        public int Capacity { get; set; }


        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

    }
}
