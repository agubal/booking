using System;
using System.Web.Http;
using EaseBooking.Entities.Products;
using EaseBooking.Services;

namespace EaseBooking.Api.Controllers.Products
{
    /// <summary>
    /// API controller for operations with Product
    /// </summary>
    [RoutePrefix("api/products")]
    public class ProductsController : BaseApiController<Product, Guid>
    {
        public ProductsController(IService<Product, Guid> entityService) : base(entityService)
        {
        }
    }
}
