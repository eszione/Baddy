using System;

namespace Baddy.Models
{
    public class CreateBookingInfo
    {
        public int Court { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
    }
}
