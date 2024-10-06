using System;
using InformationSystem.ViewModel.Sales;

namespace InformationSystem.Stores;

internal class OrderStore
{
    public event Action<OrderViewModel> OrderCreated;
    public void CreateOrder(OrderViewModel order)
    {
        OrderCreated?.Invoke(order);
    }
}