using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
     public interface IRepositoryCollaborators
    {
        /// <summary>
        /// Adds the collaborators.
        /// </summary>
        /// <param name="collaboratorsModel">The collaborators model.</param>
        /// <returns></returns>
        Task<int> AddCollaborators(Collaborators collaboratorsModel);

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Views the collaborators.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IList<CollaboratorMap> ViewCollaborators(string userId);

        /// <summary>
        /// Deletes the collaborators.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<int> DeleteCollaborators(int id);

        /// <summary>
        /// Updates the collaborators.
        /// </summary>
        /// <param name="CollaboratorslModel">The collaboratorsl model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<int> UpdateCollaborators(Collaborators CollaboratorslModel, int id);
    }
}
