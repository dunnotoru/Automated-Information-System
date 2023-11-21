using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.UseCases.CasshierUseCases
{
    public class SellTicketUseCase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IRunRepository _runRepository;
        private readonly IPassportRepository _passportRepository;
        private readonly ITicketPriceCalculator _ticketPriceCalculator;

        public SellTicketUseCase(ITicketRepository ticketRepository, IRunRepository runRepository,
            IPassportRepository passportRepository, ITicketPriceCalculator ticketPriceCalculator)
        {
            _ticketRepository = ticketRepository;
            _runRepository = runRepository;
            _passportRepository = passportRepository;
            _ticketPriceCalculator = ticketPriceCalculator;
        }

        public Ticket SellTicket(Passport passport, Run run, TicketType type)
        {
            ArgumentNullException.ThrowIfNull(passport);
            ArgumentNullException.ThrowIfNull(run);

            Passport? storedPassport = _passportRepository.Get(passport.Number, passport.Series);
            if (storedPassport == null)
                _passportRepository.Add(passport);

            Ticket ticket = new Ticket()
            {
                PassengerPassport = passport,
                Run = run,
                BookDate = DateOnly.FromDateTime(DateTime.Now),
                Casshier = "AMONGUS",
                Type = type,
                Price = _ticketPriceCalculator.CalcPrice(run.RunRoute, null, null, type)
            };

            _ticketRepository.Add(ticket);

            try
            {
                _ticketRepository.Save();
                _passportRepository.Save();
                return ticket;
            }
            catch
            {
                return null;
            }
        }
    }
}
