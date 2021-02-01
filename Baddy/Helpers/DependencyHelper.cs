using Baddy.Data;
using Baddy.Interfaces;
using Baddy.Services;
using Unity;
using Unity.Lifetime;

namespace Baddy.Helpers
{
    public class DependencyHelper
    {
        public static void RegisterDependencies(IUnityContainer container)
        {
            container.RegisterType<IAppContext, AppContext>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAuthService, AuthService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmailService, EmailService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IBookingService, BookingService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IHttpService, HttpService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMenuService, MenuService>(new ContainerControlledLifetimeManager());
            container.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IProfileService, ProfileService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IStorageService, StorageService>(new ContainerControlledLifetimeManager());
        }
    }
}
