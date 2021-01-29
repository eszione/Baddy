using System.Threading.Tasks;

namespace Baddy.Interfaces
{
    public interface IStorageService
    {
        Task DeleteKey(string key);

        T ReadKey<T>(string key);

        Task SaveKey<T>(string key, T value);   
    }
}
