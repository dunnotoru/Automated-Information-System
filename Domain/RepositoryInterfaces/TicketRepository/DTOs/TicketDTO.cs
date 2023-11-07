using Domain.Entities;

namespace Domain.RepositoryInterfaces.TicketRepository.DTOs
{
    public class TicketDTO
    {
        public TicketDTO(Run run, Passport passport, 
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
