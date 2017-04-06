using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;

namespace Common.Interfaces
{
    /// <summary>
    /// Interface de gestion des droits d'accés
    /// </summary>
    public interface IAccessLayer : INotifyPropertyChanged
    {

        /// <summary>
        /// Permet d'authentifier un utilisateur
        /// </summary>
        /// <param name="user">Nom d'utilisateur</param>
        /// <param name="password">Mot de passe</param>
        /// <returns>true dans le cas d'une authentification correct, sinon false</returns>
        bool IsAuth(string user, string password, out AccessControlErrorsEnum error);

        /// <summary>
        /// Déconnecte l'utilisateur courant
        /// </summary>
        /// <returns>Indique si l'opération s'est correctement déroulée</returns>
        bool Disconnect();
    }
}
