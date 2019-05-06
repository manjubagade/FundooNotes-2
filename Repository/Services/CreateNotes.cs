// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateNotes.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

namespace RepositoryLayer.Services
{
    using FundooApi;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CreateNotes : IRepositoryNotes
    {
        private readonly RegistrationControl registrationControl;
        private readonly IDistributedCache distributedCache;

        public CreateNotes(RegistrationControl registrationControl, IDistributedCache distributedCache)
        {
            this.registrationControl = registrationControl;
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notes">The notes.</param>
        /// <exception cref="Exception"></exception>
        public void AddNotes(Notes notes)
        {
            try
            { 
                   //// Add Notes
                   var addnotes = new Notes()
                   {
                      UserId = notes.UserId,
                      Title = notes.Title,
                      Description = notes.Description,
                      CreatedDate = notes.CreatedDate,
                      ModifiedDate = notes.ModifiedDate
                   };
                   var result = this.registrationControl.Notes.Add(addnotes);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns>Result int</returns>
        public Task<int> SaveChangesAsync()
        {
            var result = this.registrationControl.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">The identifier.</param>
        public void UpdateNotes(Notes model, int id)
        {
            Notes notes = this.registrationControl.Notes.Where<Notes>(c => c.Id == id).FirstOrDefault();
            notes.Title = model.Title;
            notes.Description = model.Description;
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return NotesModel</returns>
        public IList<Notes> ViewNotes(Guid userId)
        {
            var list = new List<Notes>();
            var note = from notes in this.registrationControl.Notes where notes.UserId == userId orderby notes.UserId descending select notes;
            foreach (var item in note)
            {
                list.Add(item);
            }
            var cacheKey = note.ToString();
            this.distributedCache.GetString(cacheKey);
            this.distributedCache.SetString(cacheKey, note.ToString());
            return note.ToArray();
        }
        

        public async Task<int> DeleteNotes(int id)
        {
            Notes notes = await this.registrationControl.Notes.FindAsync(id);
            registrationControl.Notes.Remove(notes);
            var result = registrationControl.SaveChanges();
            return result;
        }

        public string Image(IFormFile file)
        {
            return null;
        }
    }
}