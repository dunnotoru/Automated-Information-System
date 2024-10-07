﻿using System.Collections.Generic;

namespace InformationSystem.Domain.Models;

public class Brand : EntityBase
{
    public string Name { get; set; }

    public ICollection<VehicleModel> VehicleModels { get; set; } = new List<VehicleModel>();
}