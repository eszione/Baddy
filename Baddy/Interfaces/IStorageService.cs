using System.Threading.Tasks;

namespace Baddy.Interfaces
{
    public interface IStorageService
    {
        Task DeleteKey(string key);

        string ReadKey(string key);

        Task SaveKey(string key, string value);   
    }
}
