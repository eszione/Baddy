using Newtonsoft.Json;

namespace Baddy.Helpers
{
    public class JsonSerializerHelper
    {
        public static string GetString(string input)
        {
            try
            {
                return JsonConvert.DeserializeObject<string>(input);
            }
            catch
            {
                return input;
            }
        }

        public static T GetObject<T>(string input)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(input);
            }
            catch
            {
                return default;
            }
        }
    }
}
