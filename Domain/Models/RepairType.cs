﻿namespace Domain.Models
{
    public class RepairType : EntityBase
    {
        public string Name { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
