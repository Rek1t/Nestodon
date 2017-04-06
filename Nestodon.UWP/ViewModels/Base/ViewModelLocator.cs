using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Kopigi.NetCore.UWP.Interfaces;
using Kopigi.NetCore.UWP.Resources;
using Kopigi.NetCore.UWP.Services;
using Kopigi.NetCore.UWP.Settings;
using Microsoft.Practices.ServiceLocation;
using Navegar.Libs.Enums;
using Navegar.Libs.Interfaces;
using Navegar.Plateformes.NetCore.UWP.Win10;
using Nestodon.UWP.Views;

namespace Nestodon.UWP.ViewModels.Base
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<ShellViewModel>();

            //Enregistrement des Services dans l'IOC
            RegisterServices();

            //Association des ViewModels et des Views associées pour la navigation
            AssociateViewModelsToViews();
        }

        #region instances des viewmodels

        public ShellViewModel ShellViewModelInstance => ServiceLocator.Current.GetInstance<ShellViewModel>();

        public AuthentificationViewModel AuthentificationViewModelInstance
            => ServiceLocator.Current.GetInstance<INavigation>().GetViewModelInstance<AuthentificationViewModel>();

        public AccueilViewModel AccueilViewModelInstance
            => ServiceLocator.Current.GetInstance<INavigation>().GetViewModelInstance<AccueilViewModel>();

        public ResultSearchViewModel ResultSearchViewModelInstance
            => ServiceLocator.Current.GetInstance<INavigation>().GetViewModelInstance<ResultSearchViewModel>();

        //TODO : AJOUTEZ LES INSTANCES DE VOS VIEWMODELS POUR LES VIEWS


        #endregion

        #region protected

        /// <summary>
        /// Fais l'assocaition des ViewModels avec les Views associées pour Navegar
        /// </summary>
        protected void AssociateViewModelsToViews()
        {
            ServiceLocator.Current.GetInstance<INavigation>().RegisterView<ShellViewModel, Views.Shell>();
            ServiceLocator.Current.GetInstance<INavigation>().RegisterView<AuthentificationViewModel, AuthentificationPage>(BackButtonViewEnum.None);
            ServiceLocator.Current.GetInstance<INavigation>().RegisterView<AccueilViewModel, AccueilPage>(BackButtonViewEnum.Auto);
            ServiceLocator.Current.GetInstance<INavigation>().RegisterView<ResultSearchViewModel, ResultSearchPage>(BackButtonViewEnum.Auto);

            //TODO : AJOUTEZ VOS VIEWMODELS ET VIEWS POUR NAVEGAR
        }

        /// <summary>
        /// Enregistre les services dans l'IOC
        /// </summary>
        protected void RegisterServices()
        {
            //Gestion de la navigation
            if (!SimpleIoc.Default.IsRegistered<INavigation>())
            {
                SimpleIoc.Default.Register<INavigation, Navigation>();
            }

            //Gestion des paramétres fichiers
            if (!SimpleIoc.Default.IsRegistered<ISettings>())
            {
                SimpleIoc.Default.Register<ISettings, Settings>();
            }

            //Gestion des ressources
            if (!SimpleIoc.Default.IsRegistered<IResources>())
            {
                SimpleIoc.Default.Register<IResources, Resources>();
            }

            //Gestion du réseau
            if (!SimpleIoc.Default.IsRegistered<INetworkService>())
            {
                SimpleIoc.Default.Register<INetworkService, NetworkService>();

            }

            //Cryptographie
            if (!SimpleIoc.Default.IsRegistered<ICryptographyService>())
            {
                SimpleIoc.Default.Register<ICryptographyService, CryptographyService>();
            }

            //Gestion des mesages
            if (!SimpleIoc.Default.IsRegistered<IDialogService>())
            {
                SimpleIoc.Default.Register<IDialogService, DialogService>();
            }
        }

        #endregion
    }
}
