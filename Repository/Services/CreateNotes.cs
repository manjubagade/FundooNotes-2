// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateNotes.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

namespace RepositoryLayer.Services
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNotes"/> class.
        /// </summary>
        /// <param name="registrationControl">The registration control.</param>
        /// <param name="distributedCache">The distributed cache.</param>
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
        public async Task<int> AddNotes(Notes notes)
        {
            try
            {
                if (notes.Title != null || notes.Description != null)
                {

                    //// Adding Notes in database
                    var addnotes = new Notes()
                    {
                         UserId = notes.UserId,
                        Title = notes.Title,
                        Description = notes.Description,
                        CreatedDate = notes.CreatedDate,
                        ModifiedDate = notes.ModifiedDate,
                        Label = notes.Label
                    };
                    this.registrationControl.Notes.Add(addnotes);
                   
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
            var result =await this.registrationControl.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">The identifier.</param>
        public async Task<int> UpdateNotes(Notes model, int id)
        {
            try
            {
                Notes notes = this.registrationControl.Notes.Where<Notes>(c => c.Id == id).FirstOrDefault();
                notes.Title = model.Title;
                notes.Description = model.Description;
                notes.Image = model.Image;
                notes.Color = model.Color;
                notes.Label = model.Label;
                notes.IsTrash = model.IsTrash;
                notes.IsArchive = model.IsArchive;
            }
            catch(Exception e)
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
        public IList<Notes> ViewNotes(string userId)
        {
            var list = new List<Notes>();
            var note = from notes in this.registrationControl.Notes where (notes.UserId == userId && notes.IsTrash == false && notes.IsArchive == false) orderby notes.UserId descending select notes;
           // (notes.UserId == userId) && (notes.IsArchive == true) && (notes.IsTrash == false) 
            foreach (var item in note)
            {
                list.Add(item);
            }
            var cacheKey = note.ToString();
            this.distributedCache.GetString(cacheKey);
            this.distributedCache.SetString(cacheKey, note.ToString());
            return note.ToArray();
        }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Result in Int</returns>
        public async Task<int> DeleteNotes(int id)
        {
            try
            {
                Notes notes = await this.registrationControl.Notes.FindAsync(id);
                registrationControl.Notes.Remove(notes);
                return registrationControl.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
             
        }

        /// <summary>
        /// Images the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>Result in string</returns>
        public string Image(IFormFile file, int id)
        {
            var stream = file.OpenReadStream();
            var name = file.FileName;
            Account account = new Account("dc1kbrrhk", "383789512449669", "fqD5389o6BAzQiFaUk56zQzsYyM");
            Cloudinary cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(name, stream)
            };
            var uploadResult = cloudinary.Upload(uploadParams);
            var data = this.registrationControl.Notes.Where(t => t.Id == id).FirstOrDefault();
            data.Image = uploadResult.Uri.ToString();
            int result = 0;
            try
            {
                result = this.registrationControl.SaveChanges();
                return data.Image;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Archives the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IList<Notes> Archive(string userId)
        {
            var list = new List<Notes>();
            var note = from notes in this.registrationControl.Notes where notes.IsArchive == true orderby notes.UserId descending select notes;
            foreach (var data in note)
            {
                list.Add(data);
            }

            return list;
        }

        /// <summary>
        /// Archives the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns list</returns>
        public IList<Notes> TrashNote(string userId)
        {
            var list = new List<Notes>();
            var note = from notes in this.registrationControl.Notes where notes.IsTrash == true select notes;
            foreach (var data in note)
            {
                list.Add(data);
            }

            return list;
        }

        /// <summary>
        /// Reminders the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns list</returns>
        public IList<Notes> Reminder(string userId)
        {
            var list = new List<Notes>();
            var notesData = from notes in this.registrationControl.Notes where (notes.UserId.Equals(userId)) && (notes.Reminder != null) select notes;
            foreach (var data in notesData)
            {
                list.Add(data);
            }

            return list;
        }


    }
}