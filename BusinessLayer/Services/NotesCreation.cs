// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesCreation.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------
using BusinessLayer.Interfaces;
using FundooApi;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NotesCreation : INotes
    {
        private readonly IRepositoryNotes repositoryNotes;

        public NotesCreation(IRepositoryNotes repositoryNotes)
        {
            this.repositoryNotes = repositoryNotes;
        }

        /// <summary>
        /// Accesses the notes.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>The List</returns>
        public IList<Notes> AccessNotes(Guid UserId)
        {
            return this.repositoryNotes.ViewNotes(UserId);
        }

        

        /// <summary>
        /// Changes the specified notes model.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task<int> Change(Notes notesModel, int id)
        {
            
            this.repositoryNotes.UpdateNotes(notesModel,id);
            var result = this.repositoryNotes.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Creates the specified notes model.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns></returns>
        public Task<int> Create(Notes notesModel)
        {
            try
            {
            this.repositoryNotes.AddNotes(notesModel);
            var result = this.repositoryNotes.SaveChangesAsync();
            return result;
            }
            catch
            {
                return this.repositoryNotes.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Task<int> Delete(int id)
        {
            return this.repositoryNotes.DeleteNotes(id);
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public string AddImage(IFormFile file, int id)
        {

            return this.repositoryNotes.Image(file, id);
        }

        
       
    }
}