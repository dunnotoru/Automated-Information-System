namespace InformationSystem.Services;

public class ReceiptLine
{
    public string Header { get; set; }
    public int Price { get; set; }
    public int Count { get; set; }
    public int FullPrice => Price * Count;

    public ReceiptLine(string header, int price, int count)
    {
        Header = header;
        Price = price;
        Count = count;
    }
}