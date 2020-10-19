using Newtonsoft.Json;
using System.Collections.Generic;

namespace Baddy.Models.Apis
{
    public class BookingDeleted
    {
        [JsonProperty("result")]
        public bool Result { get; set; }
        [JsonProperty("data")]
        public List<DeleteData> BookingData { get; set; }
        [JsonProperty("paging")]
        public List<object> Paging { get; set; }
        [JsonProperty("deleted")]
        public List<DeleteMessage> Deleted { get; set; }
    }

    public class DeleteData
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

    public class DeleteMessage
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
