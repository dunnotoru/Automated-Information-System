using System;
using UI.ViewModel.Sales;

namespace UI.Stores;

internal class OrderStore
{
    public event Action<OrderViewModel> OrderCreated;
    public void CreateOrder(OrderViewModel order)
    {
        OrderCreated?.Invoke(order);
    }
}