using System;
using Common.Interfaces;
using Kopigi.NetCore.UWP.Object;
using Common.Enums;

namespace Nestodon.UWP.Services
{
    public class AccessLayerService : NotifyPropertyChanged, IAccessLayer
    {

        /// <summary>
        /// 
        /// </summary>
        public AccessLayerService()
        {
        }

        /// <summary>
        /// Permet de savoir si le couple utilisateur/mot de passe est correct sur l'application
        /// </summary>
        /// <param name="user">Utilisateur</param>
        /// <param name="password">Mot de passe</param>
        /// <param name="errorsEnum">Erreur possible lors de l'authentification</param>
        /// <returns></returns>
        public bool IsAuth(string user, string password, out AccessControlErrorsEnum error)
        {
            error = AccessControlErrorsEnum.None;
            return true;
        }

        /// <summary>
        /// Permet de déconnecte run utilisateur et de re-initialiser les informations actuelles sur les droits
        /// </summary>
        public bool Disconnect()
        {
            try
            {
               
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
        }
    }
}
