using System;
using System.Collections.Generic;
using EaseBooking.Entities.Common;
using EaseBooking.Entities.HealthProblems;
using EaseBooking.Entities.TimeSlots;

namespace EaseBooking.Entities.Orders
{
    /// <summary>
    /// Entity represents Order
    /// </summary>
    public class Order : IIdentifier<Guid>
    {
        /// <summary>
        /// Order Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Client name
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gender Id. Reference to Gender entity
        /// </summary>
        public Guid GenderId { get; set; }

        /// <summary>
        /// Weight
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Product Id. Reference to Product object
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Selected Time slot
        /// </summary>
        public TimeSlot TimeSlot { get; set; }

        /// <summary>
        /// Collection of Client health problems
        /// </summary>
        public List<HealthProblem> HealthProblems { get; set; }
    }
}
