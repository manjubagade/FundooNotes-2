// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="ICollborators.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

namespace BusinessLayer.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Models;

    /// <summary>
    /// ICollborators interface
    /// </summary>
    public interface ICollborators
    {
        /// <summary>
        /// Creates the specified collaborators model.
        /// </summary>
        /// <param name="collaboratorsModel">The collaborators model.</param>
        /// <returns>result data</returns>
        Task<int> Create(Collaborators collaboratorsModel);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result data</returns>
        Task<int> Delete(int id);

        /// <summary>
        /// Changes the specified collaborators model.
        /// </summary>
        /// <param name="collaboratorsModel">The collaborators model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>result data</returns>
        Task<int> Change(Collaborators collaboratorsModel, int id);

        /// <summary>
        /// Accesses the collaborators.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>result of data</returns>
        IList<CollaboratorMap> AccessCollaborators(string userId);
    }
}
