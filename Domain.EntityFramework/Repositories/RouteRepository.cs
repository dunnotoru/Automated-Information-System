using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories;

public class RouteRepository : IRouteRepository
{
    public int Create(Route entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using (ApplicationContext context = new ApplicationContext())
        {
            context.Stations.AttachRange(entity.Stations);
            context.Add(entity);
            context.SaveChanges();
        }
        return entity.Id;
    }

    public void Update(int id, Route entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using (ApplicationContext context = new ApplicationContext())
        {
            Route stored = context.Routes.Include(r => r.Stations).First(r => r.Id == id);
            List<Station> entityStations = new List<Station>();

            context.StationRoute.RemoveRange(context.StationRoute.Where(o => o.RouteId == id));

            int i = 0;
            foreach (Station item in entity.Stations)
            {
                StationRoute sr = new StationRoute()
                {
                    RouteId = id,
                    StationId = item.Id,
                    Order = i
                };
                context.StationRoute.Add(sr);

                i++;
            }

            stored.Name = entity.Name;

            context.SaveChanges();
        }
    }

    public void Remove(int id)
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            Route stored = context.Routes.Include(o => o.Runs).First(r => r.Id == id);
            if(stored.Runs.Count > 0)
            {
                string message = "";
                foreach (Run run in stored.Runs)
                {
                    message += run.Number + " ";
                }
                throw new InvalidOperationException($"На этом маршруте назначен один или несколько рейсов: {message}");
            }

            context.Remove(stored);
            context.SaveChanges();
        }
    }

    public Route GetById(int id)
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            Route r = context.Routes.First(r => r.Id == id);
            List<StationRoute> linking = context.StationRoute.Include(o => o.Station).Where(o => o.RouteId == id).ToList();
            List<Station> stations = new List<Station>();
            linking = linking.OrderBy(o => o.Order).ToList();
            foreach (StationRoute item in linking)
            {
                stations.Add(item.Station);
            }
            r.Stations = stations;
            return r;
        }
    }

    public IEnumerable<Route> GetAll()
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            List<Route> routes = context.Routes.ToList();

            foreach(Route route in routes)
            {
                List<StationRoute> linking = context.StationRoute.Include(o => o.Station).Where(o => o.RouteId == route.Id).ToList();
                List<Station> stations = new List<Station>();
                linking = linking.OrderBy(o => o.Order).ToList();
                foreach (StationRoute item in linking)
                {
                    stations.Add(item.Station);
                }

                route.Stations = stations;
            }

            return routes;
        }
    }

    public IEnumerable<Route> GetByStations(Station from, Station to)
    {
        ArgumentNullException.ThrowIfNull(from);
        ArgumentNullException.ThrowIfNull(to);

        using (ApplicationContext context = new ApplicationContext())
        {
            List<Route> routes = context.Routes.Include(r => r.Stations)
                .Where(r => r.Stations.Contains(from) && r.Stations.Contains(to))
                .ToList();

                
            foreach (Route route in routes)
            {
                route.Stations.Clear();
                List<StationRoute> linking = context.StationRoute.Include(o => o.Station).Where(o => o.RouteId == route.Id).ToList();
                List<Station> stations = new List<Station>();
                linking = linking.OrderBy(o => o.Order).ToList();
                foreach (StationRoute item in linking)
                {
                    stations.Add(item.Station);
                }
                route.Stations = stations;
            }
            return routes;
        }
    }
}