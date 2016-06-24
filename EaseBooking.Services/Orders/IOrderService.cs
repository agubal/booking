using System;
using EaseBooking.Entities.Orders;

namespace EaseBooking.Services.Orders
{
    public interface IOrderService : IService<Order, Guid>
    {
    }
}
