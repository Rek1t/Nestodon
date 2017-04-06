using Common.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using Kopigi.NetCore.UWP.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Navegar.Libs.Interfaces;
using Nestodon.UWP.Interfaces;

namespace Nestodon.UWP.ViewModels.Base
{
    public class ViewModelServices : ViewModelBase
    {
        public INavigation NavigationService => ServiceLocator.Current.GetInstance<INavigation>();
        public ISettings SettingsService => ServiceLocator.Current.GetInstance<ISettings>();
        public IResources ResourcesService => ServiceLocator.Current.GetInstance<IResources>();
        public IDialogService DialogService => ServiceLocator.Current.GetInstance<IDialogService>();
        public IAccessLayer AccessLayerService => ServiceLocator.Current.GetInstance<IAccessLayer>();
    }
}
