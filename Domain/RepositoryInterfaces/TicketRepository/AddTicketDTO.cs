using Domain.Models;

namespace Domain.RepositoryInterfaces.TicketRepository
{
    public class AddTicketDTO
    {
        public AddTicketDTO(Run run, Passport passport,
            TicketType ticketType)
        {
            Run = run;
            Passport = passport;
            TicketType = ticketType;
        }

        public Run Run { get; }
        public Passport Passport { get; }
        public TicketType TicketType { get; }
    }
}
