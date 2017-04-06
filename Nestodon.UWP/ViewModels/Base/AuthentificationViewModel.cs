using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using Nestodon.UWP.Class;
using Nestodon.UWP.Helpers;
using Nestodon.UWP.Services;

namespace Nestodon.UWP.ViewModels.Base
{
    public class AuthentificationViewModel : ViewModelModule
    {
        #region properties

        /// <summary>
        /// Permet de contrôler la propriété User
        /// </summary>
        private string _user;
        public string User
        {
            get { return _user; }
            set
            {
                _user = value;
                RaisePropertyChanged(() => User);
            }
        }

        /// <summary>
        /// Permet de contrôler la propriété Password
        /// </summary>
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        /// <summary>
        /// Permet de contrôler la propriété MessageErreur
        /// </summary>
        private string _messageErreur;
        public string MessageErreur
        {
            get { return _messageErreur; }
            set
            {
                _messageErreur = value;
                RaisePropertyChanged(() => MessageErreur);
            }
        }

        public RelayCommand CheckAuthentificationCommand { get; set; }

        #endregion

        #region cstor

        public AuthentificationViewModel()
        {
            TitleContent = string.Empty;
            CheckAuthentificationCommand = new RelayCommand(CheckAuthentification);
            User = SettingsService.GetValue<string>(ParamsLocalEnum.LastUserConnected.ToString());
        }

        #endregion

        #region RelayCommand functions

        /// <summary>
        /// Permet de vérifier si l'authentification est correcte
        /// </summary>
        private void CheckAuthentification()
        {
            if (!string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Password))
            {
                AccessControlErrorsEnum accessError;
                if (AccessLayerService.IsAuth(User.ToLower(), Password, out accessError))
                {
                    SettingsService.Save(ParamsLocalEnum.IsUserConnected.ToString(), true);
                    SettingsService.Save(ParamsLocalEnum.LastUserConnected.ToString(), "");

                    //On vérifie les CanExecute des commandes du menus
                    MenuItemsHelper.RaiseCanExecuteMenuItems();

                    //On navigue vers la page d'accueil de l'application
                    NavigationService.NavigateTo<AccueilViewModel>(true);
                }
                else
                {
                    MessageErreur = ResourcesService.GetString("ErrorMessageAuthentification");
                }
            }
        }

        #endregion

        #region public

        public void Disconnect()
        {
            //On déconnecte l'utilisateur de l'application
            AccessLayerService.Disconnect();
            SettingsService.Save(ParamsLocalEnum.IsUserConnected.ToString(), false);

            //On vérifie les CanExecute des commandes du menus
            MenuItemsHelper.RaiseCanExecuteMenuItems();
        }

        #endregion
    }
}
