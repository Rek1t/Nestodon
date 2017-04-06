using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    /// <summary>
    /// Enumération des erreurs possibles lors de l'authentification
    /// </summary>
    public enum AccessControlErrorsEnum
    {
        /// <summary>
        /// Aucune erreur
        /// </summary>
        None,

        /// <summary>
        /// L'utilisateur est inconnu
        /// </summary>
        UtilisateurInconnu,

        /// <summary>
        /// Le mot de passe est incorrect
        /// </summary>
        MDPIncorrect,

        /// <summary>
        /// L'utilisateur est bloqué
        /// </summary>
        UtilisateurBloque
    }
}
