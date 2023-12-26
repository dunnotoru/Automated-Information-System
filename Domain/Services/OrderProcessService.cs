using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.Services
{
    public class OrderProcessService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketPrintService _ticketPrintService;
        private readonly IReceiptPrintService _receiptPrintService;
        private readonly ITicketPriceCalculator _ticketPriceCalculator;

        private List<Ticket> _tickets = new List<Ticket>();

        public OrderProcessService(ITicketPrintService ticketPrintService,
                                   IReceiptPrintService receiptPrintService,
                                   ITicketPriceCalculator ticketPriceCalculator,
                                   ITicketRepository ticketRepository)
        {
            ArgumentNullException.ThrowIfNull(ticketPrintService);
            ArgumentNullException.ThrowIfNull(receiptPrintService);
            ArgumentNullException.ThrowIfNull(ticketPriceCalculator);
            ArgumentNullException.ThrowIfNull(ticketRepository);

            _ticketPrintService = ticketPrintService;
            _receiptPrintService = receiptPrintService;
            _ticketPriceCalculator = ticketPriceCalculator;
            _ticketRepository = ticketRepository;
        }

        public void AddTicket(IdentityDocument document, Run run, string cashierName, TicketType ticketType)
        {

            int count = _ticketRepository.GetAll().Where(o => o.RunId == run.Id).Count();
            int capacity = run.Vehicle.VehicleModel.Capacity;
            if (count >= capacity)
                throw new InvalidOperationException($"Количество свободных мест: {capacity - count}");

            int price = _ticketPriceCalculator.CalcPrice(run, ticketType);
            Ticket t = new Ticket()
            {
                Run = run,
                Cashier = cashierName,
                TicketType = ticketType,
                Price = price,
                BookDate = DateTime.Now,
                IdentityDocument = document,
            };
            _tickets.Add(t);
        }

        public List<Ticket> GetTickets()
        {
            List<Ticket> tickets = new List<Ticket>();
            foreach (var ticket in _tickets)
            {
                tickets.Add(ticket);
            }
            return tickets;
        }

        public bool RemoveTicket(Ticket ticket)
        {
            return _tickets.Remove(ticket);
        }

        public void PrintTickets()
        {
            
            foreach (Ticket item in _tickets)
            {
                _ticketRepository.Create(item);
                _ticketPrintService.Print(item);
            }
        }

        public void PrintReceipt(string cashierName)
        {
            List<ReceiptLine> lines = new List<ReceiptLine>();
            foreach (Ticket item in _tickets)
            {
                ReceiptLine line = new ReceiptLine("Билет", item.Price, 1);
                lines.Add(line);
            }
            Receipt receipt = new Receipt(Guid.NewGuid().ToString(), "ООО Возня", "Тута", DateTime.Now, cashierName, lines);
            _receiptPrintService.Print(receipt);
        }

        public int GetFullPrice()
        {
            int price = 0;
            foreach (var item in _tickets)
            {
                price += item.Price;
            }
            return price;
        }

        public void Clear()
        {
            _tickets.Clear();
        }
    }
}
