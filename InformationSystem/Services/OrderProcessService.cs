﻿using System;
using System.Collections.Generic;
using System.Linq;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using InformationSystem.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.Services;

public class OrderProcessService
{
    private readonly IDbContextFactory<DomainContext> _contextFactory;
    private readonly ITicketPrintService _ticketPrintService;
    private readonly IReceiptPrintService _receiptPrintService;
    private readonly ITicketPriceCalculator _ticketPriceCalculator;

    private List<Ticket> _tickets = new List<Ticket>();

    public OrderProcessService(ITicketPrintService ticketPrintService,
        IReceiptPrintService receiptPrintService,
        ITicketPriceCalculator ticketPriceCalculator,
        IDbContextFactory<DomainContext> contextFactory)
    {
        _ticketPrintService = ticketPrintService;
        _receiptPrintService = receiptPrintService;
        _ticketPriceCalculator = ticketPriceCalculator;
        _contextFactory = contextFactory;
    }

    public void AddTicket(IdentityDocument document, Run run, string cashierName, TicketType ticketType)
    {
        int count = 0;
        using (DomainContext context = _contextFactory.CreateDbContext())
        {
            count = context.Tickets.Count(o => o.RunId == run.Id);
        }
        
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
        using DomainContext context = _contextFactory.CreateDbContext();
        
        foreach (Ticket item in _tickets)
        {
            context.Tickets.Add(item);
            context.SaveChanges();
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