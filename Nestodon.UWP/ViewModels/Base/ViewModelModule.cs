using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nestodon.UWP.ViewModels.Base
{
    public class ViewModelModule : ViewModelServices
    {
        //Défini le titre de ce ViewModel pour le contenu
        private string _titleContent;
        public string TitleContent
        {
            get { return _titleContent; }
            set
            {
                _titleContent = value;
                RaisePropertyChanged(() => TitleContent);
            }
        }
    }
}
