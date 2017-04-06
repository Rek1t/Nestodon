using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nestodon.UWP.Interfaces
{
    /// <summary>
    /// Interface définissant qu'une CommandBar doit être générée pour la page de ce ViewModel
    /// </summary>
    public interface IPageCommandBar
    {
        /// <summary>
        /// Permet la création et l'affichage d'une CommandBar
        /// </summary>
        void CreateCommandBar();
    }
}
