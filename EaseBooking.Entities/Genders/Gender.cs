using System;
using EaseBooking.Entities.Common;

namespace EaseBooking.Entities.Genders
{
    /// <summary>
    /// Entity represents Gender
    /// </summary>
    public class Gender : IIdentifier<Guid>
    {
        /// <summary>
        /// Gender Id
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Gender Name
        /// </summary>
        public string Name { get; set; }
    }
}
