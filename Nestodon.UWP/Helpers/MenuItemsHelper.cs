using System;
using System.Collections.ObjectModel;
using System.Linq;
using Common.Enums;
using Common.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Nestodon.UWP.Class;
using Nestodon.UWP.Helpers.Base;
using Nestodon.UWP.Interfaces;
using Nestodon.UWP.ViewModels.Base;
using GalaSoft.MvvmLight.Views;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Kopigi.NetCore.UWP.Interfaces;

namespace Nestodon.UWP.Helpers
{
    public static class MenuItemsHelper
    {
        private static ObservableCollection<IMenuItem<ViewModelServices>> _menuItems;
        private static ObservableCollection<IMenuItem<ViewModelServices>> _menuItemsSecondary;

        public static ObservableCollection<IMenuItem<ViewModelServices>> MenuItems => SetMenusItems();
        public static ObservableCollection<IMenuItem<ViewModelServices>> MenuItemsSecondary => SetSecondaryMenusItems();

        #region private

        /// <summary>
        /// Permet de vérifier le CanExecute des commandes du menu
        /// </summary>
        public static void RaiseCanExecuteMenuItems()
        {
            if (_menuItems != null && _menuItemsSecondary != null)
            {
                foreach (var menuItem in _menuItems)
                {
                    menuItem.Navigate?.RaiseCanExecuteChanged();
                }

                foreach (var menuItem in _menuItemsSecondary)
                {
                    menuItem.Navigate?.RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// Permet de définir votre menu principal
        /// </summary>
        private static ObservableCollection<IMenuItem<ViewModelServices>> SetMenusItems()
        {
            if (_menuItems == null || !_menuItems.Any())
            {
                //TODO : AJOUTEZ VOS MENUS
                _menuItems = new ObservableCollection<IMenuItem<ViewModelServices>>
                {
                    new MenuItem<AccueilViewModel>("", ServiceLocator.Current.GetInstance<IResources>().GetString("AccueilMenuLabel"), CanExecute),
                };
            }
            return _menuItems;
        }

        /// <summary>
        /// Permet de définir votre menu secondaire
        /// </summary>
        private static ObservableCollection<IMenuItem<ViewModelServices>> SetSecondaryMenusItems()
        {
            //Menus minimums pour la structure de l'application
            return SetMinimalMenus();
        }

        /// <summary>
        /// Menus minimums requis par la structure de l'application
        /// </summary>
        /// <returns></returns>
        private static ObservableCollection<IMenuItem<ViewModelServices>> SetMinimalMenus()
        {
            if (_menuItemsSecondary == null || !_menuItemsSecondary.Any())
            {
                _menuItemsSecondary = new ObservableCollection<IMenuItem<ViewModelServices>>
                {};

                var menuDisconnect = new MenuItem<AuthentificationViewModel>("", ServiceLocator.Current.GetInstance<IResources>().GetString("DisconnectMenuLabel"), CanExecute, null, new object[] { }, "Disconnect");
                menuDisconnect.ConfirmationToNavigate += ConfirmationToNavigateDisconnect;

                _menuItemsSecondary.Add(menuDisconnect);
            }
            return _menuItemsSecondary;
        }
        #endregion

        #region ConfirmationToNavigate

        /// <summary>
        /// Confirmation de déconnexion
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> ConfirmationToNavigateDisconnect()
        {
            var dialog = new MessageDialog(ServiceLocator.Current.GetInstance<IResources>().GetString("QuestionDeconnecter"), ServiceLocator.Current.GetInstance<IResources>().GetString("QuestionDeconnecterTitre"));
            dialog.Commands.Add(new UICommand(ServiceLocator.Current.GetInstance<IResources>().GetString("QuestionResponseOui")) { Id = true });
            dialog.Commands.Add(new UICommand(ServiceLocator.Current.GetInstance<IResources>().GetString("QuestionResponseNon")) { Id = false });
            var result = await dialog.ShowAsync();
            return (bool)result.Id;
        }

        #endregion

        #region CanExecute

        /// <summary>
        /// On verifie de base si un utilisateur est connecté
        /// </summary>
        /// <remarks>
        /// SI VOUS SOUHAITEZ MODIFIER LES CONDITIONS
        /// </remarks>
        /// <returns></returns>
        private static bool CanExecute(object parameter)
        {
            return ServiceLocator.Current.GetInstance<ISettings>().GetValue<bool>(ParamsLocalEnum.IsUserConnected.ToString());
        }

        #endregion
    }
}