using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace Nestodon.UWP.Helpers.Base
{
    public static class TitleBarHelper
    {
        /// <summary>
        /// Permet d'ajsuter la couleur de la TitleBar et les boutons
        /// </summary>
        public static void SetTitleColors()
        {
            var backgroundColor = (Color)App.Current.Resources["TitleBarBackgroundColor"];
            var foregroundColor = (Color)App.Current.Resources["TitleBarForegroundColor"];
            var hoverBackgroundColor = (Color)App.Current.Resources["TitleBarButtonHoverBackgroundColor"];
            var hoverForegroundColor = (Color)App.Current.Resources["TitleBarButtonHoverForegroundColor"];
            var pressedBackgroundColor = (Color)App.Current.Resources["TitleBarButtonPressedBackgroundColor"];
            var pressedForegroundColor = (Color)App.Current.Resources["TitleBarButtonPressedForegroundColor"];
            var inactiveBackgroundColor = (Color)App.Current.Resources["TitleBarButtonInactiveBackgroundColor"];
            var inactiveForegroundColor = (Color)App.Current.Resources["TitleBarButtonInactiveForegroundColor"];

            InternalSetTitleColors(backgroundColor, foregroundColor, hoverBackgroundColor, hoverForegroundColor, pressedBackgroundColor, pressedForegroundColor, inactiveBackgroundColor, inactiveForegroundColor);
        }

        /// <summary>
        /// Permet d'ajsuter la couleur de la TitleBar et les boutons
        /// </summary>
        /// <param name="backgroundColor">Couleur voulue pour le fond de la TitleBar</param>
        /// <param name="foregroundColor">Couleur voulue pour la police de la TitleBar</param>
        /// <param name="hoverBackgroundColor">Couleur voulue pour le fond des boutons au survol</param>
        /// <param name="hoverForegroundColor">Couleur voulue pour la police des boutons au survol</param>
        /// <param name="pressedBackgroundColor">Couleur voulue pour le fond des boutons au click</param>
        /// <param name="pressedForegroundColor">Couleur voulue pour la police des boutons au click</param>
        /// <param name="inactiveBackgroundColor">Couleur voulue pour le fond de la TitleBar et des boutons en statut inactif</param>
        /// <param name="inactiveForegroundColor">Couleur voulue pour la police de la TitleBar et des boutons en statut inactif</param>
        public static void SetTitleColors(Color backgroundColor, Color foregroundColor, Color hoverBackgroundColor, Color hoverForegroundColor, Color pressedBackgroundColor, Color pressedForegroundColor, Color inactiveBackgroundColor, Color inactiveForegroundColor)
        {
            InternalSetTitleColors(backgroundColor, foregroundColor, hoverBackgroundColor, hoverForegroundColor, pressedBackgroundColor, pressedForegroundColor, inactiveBackgroundColor, inactiveForegroundColor);
        }

        #region internal

        internal static void InternalSetTitleColors(Color backgroundColor, Color foregroundColor, Color hoverBackgroundColor, Color hoverForegroundColor, Color pressedBackgroundColor, Color pressedForegroundColor, Color inactiveBackgroundColor, Color inactiveForegroundColor)
        {
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = backgroundColor;
            titleBar.BackgroundColor = backgroundColor;
            titleBar.ButtonForegroundColor = foregroundColor;
            titleBar.ForegroundColor = foregroundColor;
            titleBar.ButtonHoverBackgroundColor = hoverBackgroundColor;
            titleBar.ButtonHoverForegroundColor = hoverForegroundColor;
            titleBar.ButtonPressedBackgroundColor = pressedBackgroundColor;
            titleBar.ButtonPressedForegroundColor = pressedForegroundColor;
            titleBar.ButtonInactiveBackgroundColor = inactiveBackgroundColor;
            titleBar.ButtonInactiveForegroundColor = inactiveForegroundColor;
            titleBar.InactiveBackgroundColor = inactiveBackgroundColor;
            titleBar.InactiveForegroundColor = inactiveForegroundColor;
        }
        #endregion
    }
}
