namespace Domain.Models
{
    public class ReceiptLine
    {
        public string Header { get; set; }
        public int Price {  get; set; }
        public int Count { get; set; }
        public int FullPrice { get; set; }

        public ReceiptLine(string header, int price, int count, int fullPrice)
        {
            Header = header;
            Price = price;
            Count = count;
            FullPrice = fullPrice;
        }
    }
}
