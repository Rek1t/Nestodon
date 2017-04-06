using System.Reflection;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Practices.ServiceLocation;
using Navegar.Libs.Interfaces;
using Nestodon.UWP.Interfaces;
using Nestodon.UWP.Class;
using Windows.UI.Xaml.Controls.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Nestodon.UWP.Helpers.Base
{
    public static class CommandBarHelper
    {
        #region fields

        private static CommandBar _commandBar;

        #endregion

        #region public

        /// <summary>
        /// Ajoute un bouton à la CommandBar
        /// </summary>
        /// <param name="emplacement">Type d'emplacment du bouton sur la CommandBar, PrimaryCommands ou SecondaryCommands</param>
        /// <param name="icon">Icon du bouton</param>
        /// <param name="label">Label du bouton</param>
        /// <param name="command">ICommand du bouton</param>
        /// <param name="commandParameter">CommandParameter de la ICommand du bouton</param>
        public static CommandBarElement AddButton(CommandBarEmplacement emplacement, IconElement icon, string label,
            ICommand command, object commandParameter = null)
        {
            return AddObjectToCommandBar(CommandBarButtonType.Button, emplacement, icon, label, command, commandParameter, null);
        }

        /// <summary>
        /// Ajoute un Toggle bouton à la CommandBar
        /// </summary>
        /// <param name="emplacement">Type d'emplacment du bouton sur la CommandBar, PrimaryCommands ou SecondaryCommands</param>
        /// <param name="icon">Icon du bouton</param>
        /// <param name="label">Label du bouton</param>
        /// <param name="command">ICommand du bouton</param>
        /// <param name="commandParameter">CommandParameter de la ICommand du bouton</param>
        public static CommandBarElement AddToggleButton(CommandBarEmplacement emplacement, IconElement icon, string label,
            ICommand command, object commandParameter = null)
        {
            return AddObjectToCommandBar(CommandBarButtonType.ToggleButton, emplacement, icon, label, command, commandParameter, null);
        }

        /// <summary>
        /// Ajoute un bouton avec un flyout associé à la CommandBar
        /// </summary>
        /// <param name="emplacement">Type d'emplacment du bouton sur la CommandBar, PrimaryCommands ou SecondaryCommands</param>
        /// <param name="icon">Icon du bouton</param>
        /// <param name="label">Label du bouton</param>
        /// <param name="flyout">Flyout associé au bouton</param>
        public static CommandBarElement AddFlyoutButton(CommandBarEmplacement emplacement, IconElement icon, string label, FlyoutBase flyout)
        {
            return AddObjectToCommandBar(CommandBarButtonType.FlyoutButton, emplacement, icon, label, null, null, flyout);
        }

        /// <summary>
        /// Permet d'ajouter un séparateur à la CommandBar
        /// </summary>
        /// <param name="emplacement">Type d'emplacment du bouton sur la CommandBar, PrimaryCommands ou SecondaryCommands</param>
        public static CommandBarElement AddSeparator(CommandBarEmplacement emplacement)
        {
            return AddObjectToCommandBar(CommandBarButtonType.Separator, emplacement, null, null, null, null, null);
        }

        /// <summary>
        /// Permet la fermeture de la Page courante
        /// </summary>
        public static bool CloseAction(string loadFunction = "", params object[] loadFunctionParams)
        {
            var dictFunctions = new Dictionary<string, object[]>();

            if (!string.IsNullOrEmpty(loadFunction))
            {
                dictFunctions.Add(loadFunction, loadFunctionParams);
            }

            //On regarde comment on doit gérer la CommandBar de l'application
            if (typeof(IPageCommandBar).IsAssignableFrom(ServiceLocator.Current.GetInstance<INavigation>().GetTypeViewModelToBack()))
            {
                dictFunctions.Add("CreateCommandBar", new object[] { });
            }

            if (ServiceLocator.Current.GetInstance<INavigation>().CanGoBack())
            {
                if (dictFunctions.Any())
                {
                    ServiceLocator.Current.GetInstance<INavigation>().GoBack(dictFunctions);
                }
                else
                {
                    CommandBarHelper.Reset();
                    CommandBarHelper.SetVisibility(false);
                    ServiceLocator.Current.GetInstance<INavigation>().GoBack();
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Permet de savoir si un element existe déja dans la CommandBar
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool Exist(CommandBarElement element)
        {
            if (element != null)
            {
                if (element.Emplacement == CommandBarEmplacement.Primary)
                {
                    return _commandBar.PrimaryCommands.Any(e => e == element.Element);
                }
                return _commandBar.SecondaryCommands.Any(e => e == element.Element);
            }
            return false;
        }

        /// <summary>
        /// initialise le controle qui servira de CommandBar pour l'application
        /// </summary>
        /// <param name="command"></param>
        public static void InitializeCommandBar(CommandBar command)
        {
            _commandBar = command;
        }

        /// <summary>
        /// Permet de rendre visible ou invisible le controle
        /// </summary>
        /// <param name="visible"></param>
        public static void SetVisibility(bool visible)
        {
            _commandBar.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Permet de supprimer un élément de la CommandBar
        /// </summary>
        /// <param name="element"></param>
        public static void RemoveElement(CommandBarElement element)
        {
            if (element != null)
            {
                if (element.Emplacement == CommandBarEmplacement.Primary)
                {
                    _commandBar.PrimaryCommands.Remove(element.Element);
                }
                else
                {
                    _commandBar.SecondaryCommands.Remove(element.Element);
                }
            }
        }

        /// <summary>
        /// Permet de vider la CommandBar, donc le PrimaryCommands et le SecondaryCommands
        /// </summary>
        public static void Reset()
        {
            _commandBar.PrimaryCommands.Clear();
            _commandBar.SecondaryCommands.Clear();
        }
        #endregion

        #region private

        /// <summary>
        /// Ajoute un bouton à la barre
        /// </summary>
        /// <param name="objectType">type d'objet à créer</param>
        /// <param name="emplacement">Type d'emplacment du bouton sur la CommandBar, PrimaryCommands ou SecondaryCommands</param>
        /// <param name="icon">Icon du bouton</param>
        /// <param name="label">Label du bouton</param>
        /// <param name="command">ICommand du bouton</param>
        /// <param name="commandParameter">CommandParameter de la ICommand du bouton</param>
        private static CommandBarElement AddObjectToCommandBar(CommandBarButtonType objectType, CommandBarEmplacement emplacement, IconElement icon, string label, ICommand command, object commandParameter, FlyoutBase flyout)
        {
            ICommandBarElement element = null;

            switch (objectType)
            {
                case CommandBarButtonType.Button:
                    element = new AppBarButton
                    {
                        Icon = icon,
                        Label = label,
                        Command = command,
                        CommandParameter = commandParameter
                    };
                    break;
                case CommandBarButtonType.ToggleButton:
                    element = new AppBarToggleButton()
                    {
                        Icon = icon,
                        Label = label,
                        Command = command,
                        CommandParameter = commandParameter
                    };
                    break;
                case CommandBarButtonType.FlyoutButton:
                    element = new AppBarButton()
                    {
                        Icon = icon,
                        Label = label,
                        Flyout = flyout
                    };
                    break;
                case CommandBarButtonType.Separator:
                    element = new AppBarSeparator();
                    break;
            }

            if (element != null)
            {
                if (emplacement == CommandBarEmplacement.Primary)
                {
                    _commandBar.PrimaryCommands.Add(element);
                }
                else
                {
                    _commandBar.SecondaryCommands.Add(element);
                }

                if (_commandBar.Visibility == Visibility.Collapsed)
                {
                    _commandBar.Visibility = Visibility.Visible;
                }
            }
            return new CommandBarElement() { Element = element, Emplacement = emplacement };
        }
        #endregion
    }
}
