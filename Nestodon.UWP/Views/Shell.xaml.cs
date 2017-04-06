using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Nestodon.UWP.Helpers.Base;

namespace Nestodon.UWP.Views
{
    public sealed partial class Shell
    {
        public Frame RootFrame => this.ContentFrame;

        public Shell()
        {
            InitializeComponent();

            //Initilisation de la barre de menu de l'application
            CommandBarHelper.InitializeCommandBar(this.ActionsCommand);
#if DEBUG
            Application.Current.DebugSettings.EnableFrameRateCounter = false;
#endif
        }

        /// <summary>
        /// Lancement de la recherche globale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void OnQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            SplitView.Focus(FocusState.Programmatic);
            SearchHelper.RunSearch(args.QueryText);
        }

        /// <summary>
        /// Pour afficher la recherche en version mobile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBtOnClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button) sender;
            btn.Visibility = Visibility.Collapsed;
            if (AutoSuggestBox != null)
            {
                AutoSuggestBox.Visibility = Visibility.Visible;
                
                //Pour laisser le temps au contrôle de s'afficher
                Task.Factory.StartNew(
                    () => Dispatcher.RunAsync(CoreDispatcherPriority.Low,
                    () => AutoSuggestBox.Focus(FocusState.Programmatic)));
            }
        }

        /// <summary>
        /// Pour masquer la recherche en version mobile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoSuggestBoxOnLostFocus(object sender, RoutedEventArgs e)
        {
            var agb = (AutoSuggestBox) sender;
            agb.Visibility=Visibility.Collapsed;
            if (SearchBt != null)
            {
                SearchBt.Visibility = Visibility.Visible;
            }
        }
    }
}
