using BusinessLayer.Interfaces;
using Common.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CollaboratorsBusinessLayes:ICollborators
    {
        private readonly IRepositoryCollaborators repositoryCollaborator;
        public CollaboratorsBusinessLayes(IRepositoryCollaborators repositoryCollaborator)
        {
            this.repositoryCollaborator = repositoryCollaborator;
        }

        /// <summary>
        /// Accesses the notes.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>The List</returns>
        public IList<CollaboratorMap> AccessNotes(string UserId)
        {
            return this.repositoryCollaborator.ViewCollaborators(UserId);
        }



        /// <summary>
        /// Changes the specified notes model.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<int> Change(Collaborators collaboratorsModel, int id)
        {
            return await this.repositoryCollaborator.UpdateCollaborators(collaboratorsModel, id);
        }

        /// <summary>
        /// Creates the specified notes model.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns></returns>
        public async Task<int> Create(Collaborators collaboratorsModel)
        {
            try
            {
                return await this.repositoryCollaborator.AddCollaborators(collaboratorsModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<int> Delete(int id)
        {
            return await this.repositoryCollaborator.DeleteCollaborators(id);
        }

        public IList<CollaboratorMap> AccessCollaborators(string UserId)
        {
            return this.repositoryCollaborator.ViewCollaborators(UserId);
        }
        
    }
}
