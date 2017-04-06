using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.ServiceLocation;
using Navegar.Libs.Interfaces;
using Nestodon.UWP.Helpers.Base;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Nestodon.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AuthentificationPage : Page
    {
        public AuthentificationPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            CommandBarHelper.Reset();
            CommandBarHelper.SetVisibility(false);
        }
    }
}
