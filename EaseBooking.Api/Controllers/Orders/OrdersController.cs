using System;
using System.Web.Http;
using EaseBooking.Entities.Orders;
using EaseBooking.Services.Orders;

namespace EaseBooking.Api.Controllers.Orders
{
    /// <summary>
    /// API controller for operations with Order
    /// </summary>
    [RoutePrefix("api/orders")]
    public class OrdersController : BaseApiController<Order, Guid>
    {
        public OrdersController(IOrderService entityService) : base(entityService)
        {
        }
    }
}
