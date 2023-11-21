using Domain.Models;
using Domain.RepositoryInterfaces;
using Domain.UseCases.CasshierUseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Domain.Tests.TicketSaleTest
{
    [TestClass]
    public class TicketSaleTest
    {
        [TestMethod]
        public void SellTicket_ValidCredentials_Success()
        {
            HashSet<Passport> passports = new HashSet<Passport>();
            HashSet<Ticket> tickets = new HashSet<Ticket>();

            ITicketPriceCalculator calculator = new TicketPriceCalculatorStub();
            IPassportRepository passportRepository = new PassportRepositoryStub(passports);
            ITicketRepository ticketRepository = new TicketRepositoryStub(tickets);
            SellTicketUseCase useCase = new SellTicketUseCase(ticketRepository,
                passportRepository,calculator);

            Passport passport = new Passport(501234, 5017, "John", "Doe", "Ivanovich", DateOnly.Parse("12.10"));
            Run run = new Run();
            TicketType ticketType = new TicketType()
            {
                Id = 1,
                Name = "TestType",
                PriceModifierInPercent = 100,
            };

            Ticket ticket = useCase.SellTicket(passport, run, ticketType);

            Assert.AreEqual(99900, ticket.Price);
        }
    }
}
