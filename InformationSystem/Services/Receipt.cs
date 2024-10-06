using System;
using System.Collections.Generic;

namespace InformationSystem.Services;

public class Receipt
{
    public List<ReceiptLine> ReceiptLines { get; set; }
    public string Number { get; set; }
    public string CompanyName { get; set; }
    public string Address { get; set; }
    public DateTime OperationDateTime { get; set; }
    public string CashierName { get; set; }
    public int FullPrice
    {
        get
        {
            int result = 0;
            foreach (var line in ReceiptLines)
                result += line.FullPrice;
            return result;
        }
    }

    public Receipt(string number, string companyName, string address,
        DateTime operationDateTime, string cashierName, List<ReceiptLine> lines)
    {

        ReceiptLines = new List<ReceiptLine>();
        Number = number;
        CompanyName = companyName;
        Address = address;
        OperationDateTime = operationDateTime;
        CashierName = cashierName;
        ReceiptLines = lines;
    }
}