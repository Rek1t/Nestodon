using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using Navegar.Libs.Interfaces;
using Nestodon.UWP.Interfaces;
using Nestodon.UWP.ViewModels.Base;
using Kopigi.NetCore.UWP.Object;

namespace Nestodon.UWP.Class
{
    public delegate Task<bool> ConfirmationNavigateTo();

    public class MenuItem<T> : NotifyPropertyChanged, IMenuItem<T> where T : ViewModelBase
    {
        #region fields

        private object[] _parametersViewModel = new object[] { };
        private string _functionToLoad;
        private object[] _parametersFunction = new object[] { };
        #endregion

        public ConfirmationNavigateTo ConfirmationToNavigate { get; set; }

        /// <summary>
        /// Icône du menu
        /// </summary>
        private string _icon;
        public string Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Titre du menu
        /// </summary>
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Fonction permettant d'autoriser ou non la navigation
        /// </summary>
        public Func<object, bool> CanNavigate { get; set; }

        /// <summary>
        /// ICommand déclenchant la navigation
        /// </summary>
        public RelayCommand<object> Navigate { get; set; }

        /// <summary>
        /// Paramètre pour le RelayCommand
        /// </summary>
        public object CommandParameter { get; set; }

        #region cstor

        public MenuItem(string icon, string title, Func<object, bool> canNavigate, object commandParameter = null)
        {
            Icon = icon;
            Title = title;
            CanNavigate = canNavigate;
            CommandParameter = commandParameter;
            Navigate = new RelayCommand<object>(NavigateTo, CanNavigate);
        }

        public MenuItem(string icon, string title, Func<object, bool> canNavigate, object commandParameter = null, params object[] parametersViewModel) : this(icon, title, canNavigate, commandParameter)
        {
            _parametersViewModel = parametersViewModel;
        }

        public MenuItem(string icon, string title, Func<object, bool> canNavigate, object commandParameter, object[] parametersViewModel,
            string functionToLoad, params object[] parametersFunction)
            : this(icon, title, canNavigate, commandParameter, parametersViewModel)
        {
            _functionToLoad = functionToLoad;
            _parametersFunction = parametersFunction;
        }

        #endregion

        #region relaycommand

        /// <summary>
        /// Navigation vers le ViewModel choisi
        /// </summary>
        private async void NavigateTo(object obj)
        {
            var currentViewModel = ServiceLocator.Current.GetInstance<INavigation>().GetViewModelCurrent();
            if (currentViewModel != null)
            {
                if (currentViewModel.GetType() != typeof(T))
                {
                    if (ConfirmationToNavigate != null)
                    {
                        if (await ConfirmationToNavigate())
                        {
                            ServiceLocator.Current.GetInstance<INavigation>().NavigateTo<T>(currentViewModel, _parametersViewModel, _functionToLoad, _parametersFunction);
                        }
                    }
                    else
                    {
                        ServiceLocator.Current.GetInstance<INavigation>().NavigateTo<T>(currentViewModel, _parametersViewModel, _functionToLoad, _parametersFunction);
                    }
                }
            }
            else
            {
                ServiceLocator.Current.GetInstance<INavigation>().NavigateTo<T>(true, new object[] {Title});
            }
            ServiceLocator.Current.GetInstance<ShellViewModel>().IsSplitViewPaneOpen = false;
        }

        #endregion
    }
}
