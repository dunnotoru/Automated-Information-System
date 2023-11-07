namespace Domain.Entities
{
    public class Vehicle
    {
        public Vehicle(string licensePlateNumber, string model,
            string brand, int capacity,
            DateOnly manufacture, DateOnly lastRepair, 
            string lastRepairType, int mileage, 
            string photography, string freighter, 
            string insuranceDetails)
        {
            LicensePlateNumber = licensePlateNumber;
            Model = model;
            Brand = brand;
            Capacity = capacity;
            Manufacture = manufacture;
            LastRepair = lastRepair;
            LastRepairType = lastRepairType;
            Mileage = mileage;
            Photography = photography;
            Freighter = freighter;
            InsuranceDetails = insuranceDetails;
        }

        public string LicensePlateNumber { get; }
        public string Model { get; }
        public string Brand { get; }
        public int Capacity { get; }
        public DateOnly Manufacture { get; }
        public DateOnly LastRepair { get; }
        public string LastRepairType { get; }
        public int Mileage { get; }
        public string Photography { get; }
        public string Freighter { get; }
        public string InsuranceDetails { get; }
    }
}
