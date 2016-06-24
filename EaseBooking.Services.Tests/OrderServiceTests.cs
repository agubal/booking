using System;
using System.Collections.Generic;
using System.Linq;
using EaseBooking.DataAccess;
using EaseBooking.Entities.Common;
using EaseBooking.Entities.Orders;
using EaseBooking.Entities.Products;
using EaseBooking.Entities.TimeSlots;
using EaseBooking.Services.Orders;
using Moq;
using NUnit.Framework;

namespace EaseBooking.Services.Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private Mock<IRepository<Order, Guid>> _orderRepositoryMock;
        private Mock<IService<Product, Guid>> _productServiceMock;
        private IOrderService _orderService;

        [SetUp]
        public void Initialize()
        {
            _orderRepositoryMock = new Mock<IRepository<Order, Guid>>();
            _productServiceMock = new Mock<IService<Product, Guid>>();
            _orderService = new OrderService(_orderRepositoryMock.Object, _productServiceMock.Object);
        }

        public IEnumerable<TestCaseData> InvalidInputdata
        {
            get
            {
                Initialize();
                yield return new TestCaseData("", 33, 88, Guid.NewGuid(), Guid.NewGuid(), new TimeSlot(), "Client Name was not provided");
                yield return new TestCaseData("Andrey", 0, 88, Guid.NewGuid(), Guid.NewGuid(), new TimeSlot(), "Age is invalid or was not provided");
                yield return new TestCaseData("Andrey", -1, 88, Guid.NewGuid(), Guid.NewGuid(), new TimeSlot(), "Age should have positive value");
                yield return new TestCaseData("Andrey", 33, 0, Guid.NewGuid(), Guid.NewGuid(), new TimeSlot(), "Weight is invalid or was not provided");
                yield return new TestCaseData("Andrey", 33, -1, Guid.NewGuid(), Guid.NewGuid(), new TimeSlot(), "Weight should have positive value");
                yield return new TestCaseData("Andrey", 33, 88, Guid.Empty, Guid.NewGuid(), new TimeSlot(), "Gender is invalid or was not provided");
                yield return new TestCaseData("Andrey", 33, 88, Guid.NewGuid(), Guid.Empty, new TimeSlot(), "Product is invalid or was not provided");
                yield return new TestCaseData("Andrey", 33, 88, Guid.NewGuid(), Guid.NewGuid(), null, "TimeSlot is invalid or was not provided");
            }
        }

        [Test]
        [Description("Test logic when input data is null")]
        public void Create_WhenEntityIsNull()
        {
            //Arrange
            Order order = null;
            const string expectedError = "Order was not provided";

            //Act
            // ReSharper disable once ExpressionIsAlwaysNull
            ServiceResult<Order> result = _orderService.Create(order);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded);
            Assert.IsNull(result.Result);
            Assert.IsNotNull(result.Errors);
            Assert.AreEqual(result.Errors.First(), expectedError);
            _orderRepositoryMock.Verify(g => g.Create(It.IsAny<Order[]>()), Times.Never);
        }

        
        [Description("Test behavior for various validation errors")]
        [TestCase]
        [TestCaseSource("InvalidInputdata")]
        public void Create_WhenEntityIsInvalid(string clientName, int age, int weight, Guid genderId, Guid productId, TimeSlot timeSlot, string expectedError)
        {
            //Arrange
            var order = new Order
            {
                ClientName = clientName,
                Age = age,
                Weight = weight,
                GenderId = genderId,
                ProductId = productId,
                TimeSlot = timeSlot
            };

            //Act
            ServiceResult<Order> result = _orderService.Create(order);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded);
            Assert.IsNull(result.Result);
            Assert.IsNotNull(result.Errors);
            Assert.AreEqual(result.Errors.First(), expectedError);
            _orderRepositoryMock.Verify(g => g.Create(It.IsAny<Order[]>()), Times.Never);
        }
    }
}
