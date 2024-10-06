using System.Collections.Generic;
using InformationSystem.Domain.Models;

namespace InformationSystem.Domain.RepositoryInterfaces;

public interface IRepositoryBase<TEntity>
    where TEntity : EntityBase
{
    int Create(TEntity entity);
    void Update(int id, TEntity entity);
    void Remove(int id);
    TEntity GetById(int id);
    IEnumerable<TEntity> GetAll();
}