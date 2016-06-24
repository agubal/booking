using System;
using EaseBooking.Entities.Common;

namespace EaseBooking.Entities.TimeSlots
{
    public class TimeSlot : IIdentifier<Guid>
    {
        public Guid Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool IsAvailable { get; set; }
        public Guid ProductId { get; set; }
    }
}
