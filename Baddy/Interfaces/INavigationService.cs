using System.Threading.Tasks;

namespace Baddy.Interfaces
{
    public interface INavigationService
    {
        Task CloseMenu();
        Task NavigateTo<T>(params object[] parameters);
        Task NavigateToHome();
        Task RefreshMenu();
    }
}
