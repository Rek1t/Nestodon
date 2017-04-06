using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using Navegar.Libs.Interfaces;
using Nestodon.UWP.Class;
using Nestodon.UWP.ViewModels.Base;
using Kopigi.NetCore.UWP.Interfaces;

namespace Nestodon.UWP.Helpers.Base
{
    public static class SearchHelper
    {
        private static readonly INavigation NavigationService = ServiceLocator.Current.GetInstance<INavigation>();
        private static readonly IResources ResourcesService = ServiceLocator.Current.GetInstance<IResources>();

        /// <summary>
        /// Permet d'effectuer une recherche globale sur l'application
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static async void RunSearch(string pattern)
        {
            var list = new List<ResultSearch>();

            //TODO : AJOUTEZ VOS FONCTIONS DE RECHERCHE
            //...

            //2. On navigue vers la vue des résultats
            NavigationService.NavigateTo<ResultSearchViewModel>(ServiceLocator.Current.GetInstance<INavigation>().GetViewModelCurrent(), new object[] { list }, true);
        }

        #region search functions

        #endregion

        #region private

        /// <summary>
        /// Permet de transformer une liste de résultat en ResultSearch pour le renvoi vers la liste
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        private static ResultSearch ToResultSearch<T>(string moduleName, List<object> results)
            where T : ViewModelServices
        {
            return new ResultSearch()
            {
                ModuleName = moduleName,
                Results = results,
                OpenAction = new Action<object>((o) =>
                {
                    ServiceLocator.Current.GetInstance<INavigation>()
                        .NavigateTo<T>(ServiceLocator.Current.GetInstance<INavigation>().GetViewModelCurrent(),
                            new object[] {o}, true);
                })
            };
        }

        #endregion

    }
}
