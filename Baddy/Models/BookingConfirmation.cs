using Newtonsoft.Json;
using System.Collections.Generic;

namespace Baddy.Models
{
    public class BookingConfirmation
    {
        [JsonProperty("details")]
        public List<Detail> Details { get; set; }
        [JsonProperty("club")]
        public string Club { get; set; }
        [JsonProperty("player")]
        public string Player { get; set; }
        [JsonProperty("sport")]
        public string Sport { get; set; }
        [JsonProperty("balance_pre")]
        public string BalancePre { get; set; }
        [JsonProperty("future_cost")]
        public string FutureCost { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("status_msg")]
        public string StatusMessage { get; set; }
        [JsonProperty("total_cost")]
        public string TotalCost { get; set; }
        [JsonProperty("card_check")]
        public int CardCheck { get; set; }
        [JsonProperty("main_email")]
        public string Email { get; set; }
        [JsonProperty("alt_email")]
        public string AltEmail { get; set; }
        [JsonProperty("charge_type")]
        public string ChargeType { get; set; }
        [JsonProperty("bookings")]
        public List<ConfirmationBooking> Bookings { get; set; }
        [JsonProperty("tnc_accept")]
        public int TncAccept { get; set; }
        [JsonProperty("club_policy")]
        public List<object> ClubPolicy { get; set; }
    }

    public class Detail
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class ConfirmationBooking
    {
        [JsonProperty("time")]
        public string Time { get; set; }
        [JsonProperty("court")]
        public string Court { get; set; }
        [JsonProperty("duration")]
        public string Duration { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("time_text")]
        public string TimeText { get; set; }
        [JsonProperty("date_text")]
        public string DateText { get; set; }
        [JsonProperty("court_name")]
        public string CourtName { get; set; }
        [JsonProperty("ct_duration")]
        public string CourtDuration { get; set; }
        [JsonProperty("cost")]
        public int Cost { get; set; }
        [JsonProperty("cost_text")]
        public string CostText { get; set; }
    }
}
