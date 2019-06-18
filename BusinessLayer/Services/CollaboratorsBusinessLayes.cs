// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorsBusinessLayes.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

namespace BusinessLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLayer.Interfaces;
    using Common.Models;
    using RepositoryLayer.Interface;

    /// <summary>
    /// Collaborator Business Layer
    /// </summary>
    /// <seealso cref="BusinessLayer.Interfaces.ICollborators" />
    public class CollaboratorsBusinessLayes : ICollborators
    {
        /// <summary>
        /// The repository collaborator
        /// </summary>
        private readonly IRepositoryCollaborators repositoryCollaborator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorsBusinessLayes"/> class.
        /// </summary>
        /// <param name="repositoryCollaborator">The repository collaborator.</param>
        public CollaboratorsBusinessLayes(IRepositoryCollaborators repositoryCollaborator)
        {
            this.repositoryCollaborator = repositoryCollaborator;
        }

        /// <summary>
        /// Accesses the notes.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>The List</returns>
        public IList<CollaboratorMap> AccessNotes(string userId)
        {
            return this.repositoryCollaborator.ViewCollaborators(userId);
        }

        /// <summary>
        /// Changes the specified Collaborators.
        /// </summary>
        /// <param name="collaboratorsModel">The Collaborators.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>updated data</returns>
        public async Task<int> Change(Collaborators collaboratorsModel, int id)
        {
            return await this.repositoryCollaborator.UpdateCollaborators(collaboratorsModel, id);
        }

        /// <summary>
        /// Creates the specified Collaborators.
        /// </summary>
        /// <param name="collaboratorsModel">The Collaborators.</param>
        /// <returns>created data</returns>
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
        /// <returns>delete success data</returns>
        public async Task<int> Delete(int id)
        {
            return await this.repositoryCollaborator.DeleteCollaborators(id);
        }

        /// <summary>
        /// Accesses the collaborators.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>List of Data</returns>
        public IList<CollaboratorMap> AccessCollaborators(string userId)
        {
            return this.repositoryCollaborator.ViewCollaborators(userId);
        }
    }
}
