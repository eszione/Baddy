using Newtonsoft.Json;
using System.Collections.Generic;

namespace Baddy.Models.Apis
{
    public class Booking
    {
        [JsonProperty("result")]
        public string Result { get; set; }
        [JsonProperty("data")]
        public IList<BookingData> Data { get; set; }
    }

    public class BookingData
    {
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("time")]
        public string Time { get; set; }
        [JsonProperty("duration")]
        public string Duration { get; set; }
        [JsonProperty("court")]
        public string Court { get; set; }
        [JsonProperty("sport")]
        public string Sport { get; set; }
        [JsonProperty("amount")]
        public string Amount { get; set; }
        [JsonProperty("booking_no")]
        public string BookingNo { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }
        [JsonProperty("cost_refund")]
        public int CostRefund { get; set; }
        [JsonProperty("refund_hours")]
        public string RefundHours { get; set; }
        [JsonProperty("percent_refund")]
        public int PercentRefund { get; set; }
    }
}
