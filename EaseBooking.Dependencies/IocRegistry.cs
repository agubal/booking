using EaseBooking.DataAccess;
using EaseBooking.Services;
using EaseBooking.Services.Orders;
using StructureMap;

namespace EaseBooking.Dependencies
{
    /// <summary>
    /// Inversion of Control container
    /// </summary>
    public class IocRegistry : Registry
    {
        public IocRegistry()
        {
            Register();
        }

        /// <summary>
        /// Registers project dependencies and puts them to IoC Container
        /// </summary>
        private void Register()
        {
            //Generics:
            For(typeof(IRepository<,>)).Use(typeof(GenericRepository<,>));
            For(typeof(IService<,>)).Use(typeof(GenericService<,>));

            //Services:
            For<IOrderService>().Use<OrderService>();
        }
    }
}
