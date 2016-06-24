using System;
using System.Collections.Generic;
using EaseBooking.Entities.Common;
using EaseBooking.Entities.TimeSlots;

namespace EaseBooking.Entities.Products
{
    /// <summary>
    /// Entity represents Product
    /// </summary>
    public class Product : IIdentifier<Guid>
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Link
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Available Time slots
        /// </summary>
        public List<TimeSlot> TimeSlots { get; set; }
    }
}
