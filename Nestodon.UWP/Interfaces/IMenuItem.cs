using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;

namespace Nestodon.UWP.Interfaces
{
    public interface IMenuItem<out T>
    {
        /// <summary>
        /// Icône du menu
        /// </summary>
        string Icon { get; set; }

        /// <summary>
        /// Titre du menu
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Fonction permettant d'autoriser ou non la navigation
        /// </summary>
        Func<object, bool> CanNavigate { get; set; }

        /// <summary>
        /// ICommand déclenchant la navigation
        /// </summary>
        RelayCommand<object> Navigate { get; }

        /// <summary>
        /// Paramètre pour le RelayCommand
        /// </summary>
        object CommandParameter { get; set; }
    }
}
