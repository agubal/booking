using System;
using EaseBooking.Entities.Common;

namespace EaseBooking.Entities.HealthProblems
{
    /// <summary>
    /// Entitiy represents Health Problem
    /// </summary>
    public class HealthProblem : IIdentifier<Guid>
    {
        /// <summary>
        /// HealthProblem Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Health problem name
        /// </summary>
        public string Name { get; set; }
    }
}
