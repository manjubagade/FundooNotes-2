// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesCreation.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------
using BusinessLayer.Interfaces;
using FundooApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    /// <summary>
    /// Class Notes For Notes Operations
    /// </summary>
    /// <seealso cref="BusinessLayer.Interfaces.INotes" />
    public class NotesCreation : INotes
    {
        private readonly IRepositoryNotes repositoryNotes;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesCreation"/> class.
        /// </summary>
        /// <param name="repositoryNotes">The repository notes.</param>
        public NotesCreation(IRepositoryNotes repositoryNotes)
        {
            this.repositoryNotes = repositoryNotes;
        }

        /// <summary>
        /// Accesses the notes.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>The List</returns>
        public IList<Notes> AccessNotes(string UserId)
        {
            return this.repositoryNotes.ViewNotes(UserId);
        }

        

        /// <summary>
        /// Changes the specified notes model.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<int> Change(Notes notesModel, int id)
        {
          return await this.repositoryNotes.UpdateNotes(notesModel,id);
        }

        /// <summary>
        /// Creates the specified notes model.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns></returns>
        public async Task<int> Create(Notes notesModel)
        {
            try
            {
                return await this.repositoryNotes.AddNotes(notesModel);
            }
            catch(Exception e)
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
            return await this.repositoryNotes.DeleteNotes(id);
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<string> AddImage(IFormFile file, int id)
        {
            return await this.repositoryNotes.Image(file, id);
        }

        /// <summary>
        /// Archives the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return list</returns>
        public IList<Notes> Archive(string userId)
        {
            return this.repositoryNotes.Archive(userId);
        }

        /// <summary>
        /// Archives the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return list</returns>
        public IList<Notes> Trash(string userId)
        {
            return this.repositoryNotes.TrashNote(userId);
        }

        /// <summary>
        /// Reminders the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// returns list
        /// </returns>
        public IList<Notes> Reminder(string userId)
        {
            return this.repositoryNotes.Reminder(userId);
        }
        
    }
}