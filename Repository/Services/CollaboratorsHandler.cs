// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="CollaboratorsHandler.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

namespace RepositoryLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Models;
    using FundooApi;
    using Microsoft.Extensions.Caching.Distributed;
    using RepositoryLayer.Interface;

    /// <summary>
    /// Class Collaborator for Collaborator operations
    /// </summary>
    /// <seealso cref="RepositoryLayer.Interface.IRepositoryCollaborators" />
    public class CollaboratorsHandler : IRepositoryCollaborators
    {
        /// <summary>
        /// The registration control
        /// </summary>
        private readonly RegistrationControl registrationControl;

        /// <summary>
        /// The distributed cache
        /// </summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorsHandler"/> class.
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
        /// <returns>data data</returns>
        /// <exception cref="Exception">the Exception</exception>
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
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            return await this.SaveChangesAsync();
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns>Result data</returns>
        public async Task<int> SaveChangesAsync()
        {
            var result = await this.registrationControl.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Updates the collaborators.
        /// </summary>
        /// <param name="collaboratorsmodel">The Collaborators.</param>
        /// <param name="id">The id.</param>
        /// <returns>updated result</returns>
        /// <exception cref="Exception">The Exception</exception>
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

            return await this.SaveChangesAsync();
        }

        public IList<Notes> CollaboratorNote(string receiverEmail)
        {
            try
            {
                var sharednotes = new List<Notes>();
                var data = from coll in this.registrationControl.Collaborators
                           where coll.ReceiverEmail == receiverEmail
                           select new
                           {
                               coll.SenderEmail,
                               coll.Id
                           };
                foreach (var result in data)
                {
                    var collnotes = from notes in this.registrationControl.Notes
                                    where notes.Id.Equals(result.Id)
                                    select new Notes
                                    {
                                        Id = notes.Id,
                                        Title = notes.Title,
                                        Description = notes.Description,
                                    };
                    foreach (var collaborator in collnotes)
                    {
                        sharednotes.Add(collaborator);
                    }
                }

                return sharednotes;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
                var innerJoin = from e in this.registrationControl.Notes
                                join d in this.registrationControl.Collaborators on e.UserId equals d.UserId where e.Id.ToString() == d.Id
                                
                                select new CollaboratorMap
                                {
                                    UserId = e.UserId,
                                    Title = e.Title,
                                    Description = e.Description,
                                    Label = e.Label,
                                    SenderEmail = d.SenderEmail,
                                    ReceiverEmail = d.ReceiverEmail,
                                   Image = e.Image,
                               };

                return innerJoin.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
            /// <summary>
            /// Deletes the notes.
            /// </summary>
            /// <param name="id">The identifier.</param>
            /// <returns>Result data</returns>
            public async Task<int> DeleteCollaborators(int id)
        {
            try
            {
                Collaborators collaboratorsModel = await this.registrationControl.Collaborators.FindAsync(id);
                this.registrationControl.Collaborators.Remove(collaboratorsModel);
                return this.registrationControl.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}