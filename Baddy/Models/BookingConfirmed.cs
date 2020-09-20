using Newtonsoft.Json;
using System.Collections.Generic;

namespace Baddy.Models
{
    public class BookingConfirmed
    {
        [JsonProperty("result")]
        public string Result { get; set; }
        [JsonProperty("bookings_array")]
        public List<ConfirmedBooking> Bookings { get; set; }
    }

    public class ConfirmedBooking
    {
        [JsonProperty("time")]
        public string Time { get; set; }
        [JsonProperty("court")]
        public string Court { get; set; }
        [JsonProperty("duration")]
        public string Duration { get; set; }
        [JsonProperty("cost")]
        public string Cost { get; set; }
        [JsonProperty("book_date")]
        public string Date { get; set; }
        [JsonProperty("book_status")]
        public int Status { get; set; }
        [JsonProperty("insert_result")]
        public bool? Result { get; set; }
    }
}
