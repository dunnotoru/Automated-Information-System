using System;

namespace InformationSystem.Domain.Models;

public class IdentityDocument : EntityBase
{
    public string Number { get; set; }
    public string Series { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public DateTime BirthDate { get; set; }

    public string GetFullName()
    {
        return $"{Name} {Patronymic} {Surname}";
    }
}