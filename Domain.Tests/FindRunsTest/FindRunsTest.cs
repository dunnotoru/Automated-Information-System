using Domain.Models;
using Domain.RepositoryInterfaces;
using Domain.UseCases.CashierUseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Tests.FindRunsTest
{
    [TestClass]
    public class FindRunsTest
    {
        [TestMethod]
        public void FindRuns()
        {
            Station moscow = new Station() { Id = 1, Name = "Moscow", Address = "Moss" };
            Station omsk = new Station() { Id = 2, Name = "Omsk", Address = "omsk-city" };
            Station newYork = new Station() { Id = 3, Name = "new york", Address = "usa" };
            Station tomYorke = new Station() { Id = 4, Name = "radiohead", Address = "uk" };
            Station astana = new Station() { Id = 5, Name = "astana", Address = "kazakstan" };
            Station nursultan = new Station() { Id = 6, Name = "nursultan", Address = "kazakstan" };

            HashSet<Route> routes = new HashSet<Route>()
            {
                new Route(1, "MOSKVA-OMSK", new List<Station>() { moscow, omsk }),
                new Route(2, "NEW YORK-TOM YORKE", new List<Station>() { newYork, tomYorke }),
                new Route(3, "ASTANA-NURSULTAN", new List<Station>() { astana, nursultan }),
                new Route(4, "MOSKVA-ASTANA", new List<Station>() { moscow, newYork, omsk, astana })
            };

            HashSet<Run> runs = new HashSet<Run>()
            {
                new Run() { Number = 1, Route = routes.First(x=>x.Id == 1), Departure = DateTime.MinValue },
                new Run() { Number = 2, Route = routes.First(x=>x.Id == 2), Departure = DateTime.MinValue },
                new Run() { Number = 3, Route = routes.First(x=>x.Id == 3), Departure = DateTime.MinValue },
                new Run() { Number = 4, Route = routes.First(x=>x.Id == 4), Departure = DateTime.MinValue },
            };

            IRunRepository runRepository = new RunRepositoryStub(runs);
            IRouteRepository routeRepository = new RouteRepositoryStub(routes);
            FindRunsUseCase useCase = new FindRunsUseCase(routeRepository,runRepository);

            List<Run> foundRuns = useCase.FindRuns(moscow, omsk, DateTime.MinValue).ToList();

            Assert.AreEqual(2, foundRuns.Count);
        }
    }
}
