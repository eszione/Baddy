using Baddy.Interfaces;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Baddy.Services
{
    public class StorageService : IStorageService
    {
        public async Task DeleteKey(string key)
        {
            try
            {
                if (ContainsKey(key))
                    Application.Current.Properties[key] = null;

                await Application.Current.SavePropertiesAsync();
            }
            catch
            {
            }
        }

        public T ReadKey<T>(string key)
        {
            if (!ContainsKey(key))
                return default;

            try
            {
                var stringValue = Application.Current.Properties[key].ToString();

                return typeof(T).IsEnum
                    ? (T)Enum.Parse(typeof(T), stringValue)
                    : (T)Convert.ChangeType(stringValue, typeof(T));
            } 
            catch
            {
                return default;
            }
        }

        public async Task SaveKey<T>(string key, T value)
        {
            try
            {
                if (typeof(T) != typeof(int) || typeof(T) != typeof(string))
                    Application.Current.Properties[key] = value.ToString();
                else
                    Application.Current.Properties[key] = value;

                await Application.Current.SavePropertiesAsync();
            }
            catch
            {
            }
        }

        private bool ContainsKey(string key)
            => Application.Current.Properties.ContainsKey(key) && !string.IsNullOrWhiteSpace(Application.Current.Properties[key] as string);
    }
}
