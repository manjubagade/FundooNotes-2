using Common.Models;
using FundooApi;
using Microsoft.Extensions.Caching.Distributed;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    /// <summary>
    /// Class Collaborator for Colaborator operations
    /// </summary>
    /// <seealso cref="RepositoryLayer.Interface.IRepositoryCollaborators" />
    public class CollaboratorsHandler : IRepositoryCollaborators
    {
        private readonly RegistrationControl registrationControl;
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNotes"/> class.
        /// </summary>
        /// <param name="registrationControl">The registration control.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public CollaboratorsHandler(RegistrationControl registrationControl, IDistributedCache distributedCache)
        {
            this.registrationControl = registrationControl;
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// Adds the collaborators.
        /// </summary>
        /// <param name="collaboratorsModel">The collaborators model.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> AddCollaborators(Collaborators collaboratorsModel)
        {
            try
            {
                if (collaboratorsModel.ReceiverEmail != null)
                {

                    //// Adding Notes in database
                    var addCollaborators = new Collaborators()
                    {
                        SenderEmail = collaboratorsModel.SenderEmail,
                        ReceiverEmail = collaboratorsModel.ReceiverEmail,
                        Id = collaboratorsModel.Id,
                        UserId = collaboratorsModel.UserId
                    };
                    this.registrationControl.Collaborators.Add(addCollaborators);

                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
             return await SaveChangesAsync();
           

        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns>Result int</returns>
        public async Task<int> SaveChangesAsync()
        {
            var result = await this.registrationControl.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">The identifier.</param>
        public async Task<int> UpdateCollaborators(Collaborators collaboratorsmodel, int id)
        {
            try
            {
                Collaborators collaborator = this.registrationControl.Collaborators.Where<Collaborators>(c => c.CollaboratorsId == id).FirstOrDefault();
                collaborator.SenderEmail = collaboratorsmodel.SenderEmail;
                collaborator.ReceiverEmail = collaboratorsmodel.ReceiverEmail;
                collaborator.Id = collaboratorsmodel.Id;
                collaborator.UserId = collaboratorsmodel.UserId;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return await SaveChangesAsync();
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return NotesModel</returns>
        public IList<CollaboratorMap> ViewCollaborators(string userId)
        {
            try
            {
                var list = new List<CollaboratorMap>();
                var innerJoin = from e in registrationControl.Notes
                                join d in registrationControl.Collaborators on e.UserId equals d.UserId where e.Id.ToString()==d.Id
                                
                                select new CollaboratorMap
                                {
                                    UserId=e.UserId,
                                    Title = e.Title,
                                    Description = e.Description,
                                    Label = e.Label,
                                    SenderEmail = d.SenderEmail,
                                    ReceiverEmail = d.ReceiverEmail,
                                   Image=e.Image,

                               };
               

                return innerJoin.ToList();
            }

            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
            /// <summary>
            /// Deletes the notes.
            /// </summary>
            /// <param name="id">The identifier.</param>
            /// <returns>Result in Int</returns>
            public async Task<int> DeleteCollaborators(int id)
        {
            try
            {
                Collaborators collaboratorsModel = await this.registrationControl.Collaborators.FindAsync(id);
                registrationControl.Collaborators.Remove(collaboratorsModel);
                return registrationControl.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

    }
}