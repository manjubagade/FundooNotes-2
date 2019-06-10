using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICollborators
    {
        /// <summary>
        /// Creates the specified collaborators model.
        /// </summary>
        /// <param name="collaboratorsModel">The collaborators model.</param>
        /// <returns></returns>
        Task<int> Create(Collaborators collaboratorsModel);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<int> Delete(int id);

        /// <summary>
        /// Changes the specified collaborators model.
        /// </summary>
        /// <param name="collaboratorsModel">The collaborators model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<int> Change(Collaborators collaboratorsModel, int id);

        /// <summary>
        /// Accesses the collaborators.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IList<CollaboratorMap> AccessCollaborators(string userId);

    }
}
