using System;
using System.Web.Http;
using EaseBooking.Entities.Genders;
using EaseBooking.Services;

namespace EaseBooking.Api.Controllers.Genders
{
    /// <summary>
    /// API controller for operations with Gender
    /// </summary>
    [RoutePrefix("api/genders")]
    public class GendersController : BaseApiController<Gender, Guid>
    {
        public GendersController(IService<Gender, Guid> entityService) : base(entityService)
        {
        }
    }
}
