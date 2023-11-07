namespace Domain.RepositoryInterfaces.VehicleRepository
{
    public class UpdateVehilceRepairDTO
    {
        public UpdateVehilceRepairDTO(string licensePlateNumber, DateOnly lastRepair,
            string lastRepairType)
        {
            LicensePlateNumber = licensePlateNumber;
            LastRepair = lastRepair;
            LastRepairType = lastRepairType;
        }

        public string LicensePlateNumber { get; }
        public DateOnly LastRepair { get; }
        public string LastRepairType { get; }
    }
}
