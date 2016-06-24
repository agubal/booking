using System;
using System.Web.Http;
using EaseBooking.Entities.HealthProblems;
using EaseBooking.Services;

namespace EaseBooking.Api.Controllers.HealthProblems
{
    /// <summary>
    /// API controller for operations with HealthProblem
    /// </summary>
    [RoutePrefix("api/healthproblems")]
    public class HealthProblemsController : BaseApiController<HealthProblem, Guid>
    {
        public HealthProblemsController(IService<HealthProblem, Guid> entityService) : base(entityService)
        {
        }
    }
}
