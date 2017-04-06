using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Nestodon.UWP.Class;

namespace Nestodon.UWP.ViewModels.Base
{
    public class ResultSearchViewModel : ViewModelModule
    {
        #region properties
        
        public ObservableCollection<ResultDraw> Results { get; set; }

        private ResultDraw _selectedResult;
        public ResultDraw SelectedResult
        {
            get { return _selectedResult; }
            set
            {
                _selectedResult = value;
                if (_selectedResult != null)
                {
                    _selectedResult.OpenAction(_selectedResult.Result);
                }
                RaisePropertyChanged(() => SelectedResult);
            }
        }
        
        #endregion

        public ResultSearchViewModel(List<ResultSearch> results)
        {
            TitleContent = ResourcesService.GetString("ResultViewLabel");
            Results = new ObservableCollection<ResultDraw>();
            foreach (var result in results)
            {
                foreach (var o in result.Results)
                {
                    Results.Add(new ResultDraw() {Result = o, OpenAction = result.OpenAction});
                }
            }
        }
    }

    public class ResultDraw
    {
        public object Result { get; set; }
        public Action<object> OpenAction { get; set; }
    }
}
