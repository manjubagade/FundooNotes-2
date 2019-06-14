// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateNotes.cs" company="Bridgelabz">
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
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using FundooApi;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using RepositoryLayer.Interface;
   
    /// <summary>
    /// CreateNotes class
    /// </summary>
    public class CreateNotes : IRepositoryNotes
    {
        /// <summary>
        /// RegistrationControl instance
        /// </summary>
        private readonly RegistrationControl registrationControl;

        /// <summary>
        /// IDistributedCache instance
        /// </summary>
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
        /// Additng Notes 
        /// </summary>
        /// <param name="notes">The Notes.</param>
        /// <returns>result data</returns>
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
                        Label = notes.Label,
                        Reminder = notes.Reminder,
                        Pin = notes.Pin
                    };

                    this.registrationControl.Notes.Add(addnotes);
                   
                }
            }
            catch (Exception e)
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
            var result =await this.registrationControl.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Update Notes
        /// </summary>
        /// <param name="model">The Notes.</param>
        /// <param name="id">The id.</param>
        /// <returns>updated result</returns>
        public async Task<int> UpdateNotes(Notes model, int id)
        {
            try
            {
                //// Update notes as per Id
                Notes notes = this.registrationControl.Notes.Where<Notes>(c => c.Id == id).FirstOrDefault();
                notes.Title = model.Title;
                notes.Description = model.Description;
                notes.Image = model.Image;
                notes.Color = model.Color;
                notes.Label = model.Label;
                notes.IsTrash = model.IsTrash;
                notes.IsArchive = model.IsArchive;
                notes.Reminder = model.Reminder;
                notes.Pin = model.Pin;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

           return await this.SaveChangesAsync();
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
           //// (notes.UserId == userId) && (notes.IsArchive == true) && (notes.IsTrash == false) 
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
        /// <returns>Result data</returns>
        public async Task<int> DeleteNotes(int id)
        {
            try
            {
                Notes notes = await this.registrationControl.Notes.FindAsync(id);
                registrationControl.Notes.Remove(notes);
                return registrationControl.SaveChanges();
            }
            catch (Exception e)
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
        public async Task<string> Image(IFormFile file, int id)
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
                result = await this.registrationControl.SaveChangesAsync();
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
        /// <returns>notes data</returns>
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
            var notesData = from notes in this.registrationControl.Notes where (notes.UserId.Equals(userId)) && (!notes.Reminder.Year.Equals(0001)) select notes;
            foreach (var data in notesData)
            {
                list.Add(data);
            }

            return list;
        }

        public IList<Notes> Alarm(string Userid)
        {
            var list = new List<Notes>();
            var Alarm = from notes in this.registrationControl.Notes where notes.UserId == Userid select notes.Reminder;
            //var time = DateTime.Now;
            var time= "0001 - 01 - 01 00:00:00.0000000";
            if (Alarm.Equals(time))
            {
                Console.WriteLine("Alarm Is Successfully Display");
            }
            return null;
        }
    }
}