using Domain.EntityFramework.Entities;
using Domain.Models;

namespace Domain.EntityFramework.Mappers
{
    public static class StationMapper
    {
        public static Station ToDomain(StationEntity entity)
        {
            return new Station(
                entity.Id,
                entity.Name,
                entity.Address);
        }

        public static StationEntity ToEntity(Station model)
        {
            return new StationEntity()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address
            };
        }
    }
}
