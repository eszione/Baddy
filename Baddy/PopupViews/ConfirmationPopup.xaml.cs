using Baddy.Interfaces;
using System.ComponentModel;
using Unity.Resolution;
using Unity;
using Rg.Plugins.Popup.Pages;
using Baddy.PopupModels;

namespace Baddy.Views
{
    [DesignTimeVisible(false)]
    public partial class ConfirmationPopup : PopupPage
    {
        public ConfirmationPopup(IAppContext appContext, string message)
        {
            InitializeComponent();

            BindingContext = appContext.Container.Resolve<ConfirmationPopupModel>(
                new ResolverOverride[] {
                    new ParameterOverride("message", message)
                }
            );
        }
    }
}