namespace Domain.Models
{
    public class Receipt
    {
        public List<ReceiptLine> ReceiptLines { get; set; }
        public int Number { get; set; }
        public string CompanyName {  get; set; }
        public string Address { get; set; }
        public DateTime OperationDateTime { get; set; }
        public string CashierName {  get; set; }
        public int FullPrice {  get; set; }

        public Receipt(int number, string companyName, string address,
            DateTime operationDateTime, string cashierName, int fullPrice)
        {
            ReceiptLines = new List<ReceiptLine>();
            Number = number;
            CompanyName = companyName;
            Address = address;
            OperationDateTime = operationDateTime;
            CashierName = cashierName;
            FullPrice = fullPrice;
        }
    }
}
