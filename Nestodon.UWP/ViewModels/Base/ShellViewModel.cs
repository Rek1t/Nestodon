using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Common.Enums;
using Common.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Microsoft.Practices.ServiceLocation;
using Navegar.Libs.Interfaces;
using Nestodon.UWP.Class;
using Nestodon.UWP.Helpers;
using Nestodon.UWP.Helpers.Base;
using Nestodon.UWP.Interfaces;
using Windows.UI.ViewManagement;
using Windows.UI;
using Kopigi.NetCore.UWP.Interfaces;

namespace Nestodon.UWP.ViewModels.Base
{
    public class ShellViewModel : ViewModelModule
    {
        #region properties

        private bool _isSplitViewPaneOpen;
        public bool IsSplitViewPaneOpen
        {
            get { return _isSplitViewPaneOpen; }
            set
            {
                _isSplitViewPaneOpen = value;
                RaisePropertyChanged(() => IsSplitViewPaneOpen);   
            }
        }

        private bool _isSearchVisible;
        public bool IsSearchVisible
        {
            get { return _isSearchVisible; }
            set
            {
                _isSearchVisible = value;
                RaisePropertyChanged(() => IsSearchVisible);
            }
        }

        public ObservableCollection<IMenuItem<ViewModelServices>> MenuItems => MenuItemsHelper.MenuItems;
        public ObservableCollection<IMenuItem<ViewModelServices>> SecondaryMenuItems => MenuItemsHelper.MenuItemsSecondary;

        public RelayCommand ToggleSplitViewPaneCommand { get; set; }

        #endregion

        public ShellViewModel()
        {
            TitleContent = String.Empty;
            this.ToggleSplitViewPaneCommand = new RelayCommand(ToggleSplitView);

            //Paramétre d'exécution
            ServiceLocator.Current.GetInstance<ISettings>().InitializeSettings(true);
            ServiceLocator.Current.GetInstance<ISettings>().Save(ParamsLocalEnum.IsUserConnected.ToString(), false);
            MenuItemsHelper.RaiseCanExecuteMenuItems();

            //Permet de réagir si l'utilisateur se connecte ou se deconnecte de l'application
            //ServiceLocator.Current.GetInstance<IAccessLayer>().GetPropertyValues(x => x.CurrentUser).Subscribe(v => IsSearchVisible = v != null);

            //Couleur de la TitleBar
            TitleBarHelper.SetTitleColors();
        }

        #region relaycommand

        private void ToggleSplitView()
        {
            this.IsSplitViewPaneOpen = !this.IsSplitViewPaneOpen;
        }
        #endregion
    }
}