using System;
using System.Collections.Generic;
using System.Linq;
using EaseBooking.DataAccess;
using EaseBooking.Entities.Common;
using EaseBooking.Entities.Orders;
using EaseBooking.Entities.Products;

namespace EaseBooking.Services.Orders
{
    /// <summary>
    /// Business logic for Order entity
    /// </summary>
    public class OrderService : GenericService<Order, Guid>, IOrderService
    {
        private readonly IService<Product, Guid> _productService;

        public OrderService(IRepository<Order, Guid> entityRepository, IService<Product, Guid> productService) : base(entityRepository)
        {
            _productService = productService;
        }

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ServiceResult<Order> Create(Order entity)
        {
            //Validate input:
            ServiceResult<Order> validationResult = ValidateInput(entity);
            if (!validationResult.Succeeded) return validationResult;

            ServiceResult<Product> productSearchResult = _productService.FindByKey(entity.ProductId);
            if(!productSearchResult.Succeeded) return new ServiceResult<Order>("Product Id is invalid");

            //Create order
            ServiceResult<Order> createResult =  base.Create(entity);
            if (!createResult.Succeeded) return createResult;

            //Update Available slots in product:
            Product product = productSearchResult.Result;
            foreach (var slot in product.TimeSlots)
            {
                slot.IsAvailable = slot.Id != entity.TimeSlot.Id;
            }
            _productService.Update(product);
            return createResult;
        }

        /// <summary>
        /// Validate entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private ServiceResult<Order> ValidateInput(Order entity)
        {
            if (entity == null) return new ServiceResult<Order>("Order was not provided");
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(entity.ClientName)) errors.Add("Client Name was not provided");
            if (entity.Age == default(int)) errors.Add("Age is invalid or was not provided");
            if (entity.Age < 0) errors.Add("Age should have positive value");
            if (entity.Weight == default(int)) errors.Add("Weight is invalid or was not provided");
            if (entity.Weight < 0) errors.Add("Weight should have positive value");

            if (entity.GenderId == Guid.Empty) errors.Add("Gender is invalid or was not provided");
            if (entity.ProductId == Guid.Empty) errors.Add("Product is invalid or was not provided");
            if (entity.TimeSlot == null) errors.Add("TimeSlot is invalid or was not provided");
            return errors.Any() ? new ServiceResult<Order>(errors) : new ServiceResult<Order>(entity);
        }
    }
}
