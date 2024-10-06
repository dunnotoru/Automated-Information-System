using InformationSystem.Domain.Models;

namespace InformationSystem.Domain.RepositoryInterfaces;

public interface IPassportRepository : IRepositoryBase<IdentityDocument>
{
    IdentityDocument Get(string number, string series);
    bool IsExist(IdentityDocument document);
}