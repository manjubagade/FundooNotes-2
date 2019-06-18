// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepositoryCollaborators.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

namespace RepositoryLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Models;

    /// <summary>
    /// IRepositoryCollaborators interface
    /// </summary>
    public interface IRepositoryCollaborators
    {
        /// <summary>
        /// Adds the collaborators.
        /// </summary>
        /// <param name="collaboratorsModel">The collaborators model.</param>
        /// <returns>data Value</returns>
        Task<int> AddCollaborators(Collaborators collaboratorsModel);

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns>data Result</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Views the collaborators.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>result data</returns>
        IList<CollaboratorMap> ViewCollaborators(string userId);

        /// <summary>
        /// Deletes the collaborators.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>data result</returns>
        Task<int> DeleteCollaborators(int id);

        /// <summary>
        /// Updates the collaborators.
        /// </summary>
        /// <param name="collaboratorslModel">The Collaborators.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>result data</returns>
        Task<int> UpdateCollaborators(Collaborators collaboratorslModel, int id);
    }
}
