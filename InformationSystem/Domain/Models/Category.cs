using System.Collections.Generic;

namespace InformationSystem.Domain.Models;

public class Category : EntityBase
{
    public string Name { get; set; }
    public ICollection<DriverLicense> Licenses { get; set;} = new List<DriverLicense>();
}