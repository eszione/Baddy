using Baddy.Interfaces;
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

        public string ReadKey(string key)
            => ContainsKey(key) ? Application.Current.Properties[key] as string : string.Empty;

        public async Task SaveKey(string key, string value)
        {
            try
            {
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
