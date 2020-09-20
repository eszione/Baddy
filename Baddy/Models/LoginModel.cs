using Newtonsoft.Json;

namespace Baddy.Models
{
    public class LoginModel
    {
        [JsonProperty("function")]
        public string Function { get; set; }
        [JsonProperty("user")]
        public string User { get; set; }
        [JsonProperty("pass")]
        public string Pass { get; set; }
    }
}
