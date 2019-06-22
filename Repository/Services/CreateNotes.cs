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
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Policy;
    using System.Text;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Common.Models;
    using FundooApi;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;
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

        private Uri FireBasePushNotificationsURL = new Uri("https://fcm.googleapis.com/fcm/send");
        private string ServerKey = "AAAAb2HcBc0:APA91bE4Xvd-kSF0p-NjHhriao0zqN6Nl4-ms93s5V15qa1RB0oc-US0O_YnVxqPwIveDbxwQJnvB5kYK54NDSfh5kh3E-04mu3rGzWV9Cjfmm3TeOBuJWh2Wja-FSaEP1e3tlu-GGY1";

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
        /// Adding Notes 
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
            var result = await this.registrationControl.SaveChangesAsync();
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
        public (IList<Notes>, IList<CollaboratorMap>) ViewNotes(string userId)
        {
            var list = new List<Notes>();
            var list1 = new List<CollaboratorMap>();
            var note = from notes in this.registrationControl.Notes where (notes.UserId == userId && notes.IsTrash == false && notes.IsArchive == false) orderby notes.UserId descending select notes;

            foreach (var item in note)
            {
                list.Add(item);
            }
            var user = from users in registrationControl.Application where users.Id == userId select users;
            foreach (var users in user)
            {
                var ReceiveCollaboratorNotes = from e in this.registrationControl.Notes
                                join d in this.registrationControl.Collaborators on e.UserId equals d.UserId
                                where e.Id.ToString() == d.Id && d.ReceiverEmail == users.Email

                                select new Notes
                                {
                                    Id = e.Id,
                                    UserId = e.UserId,
                                    Title = e.Title,
                                    Description = e.Description,
                                    Label = e.Label,
                                    Image = e.Image,
                                };

                var CollaboratesData = from e in this.registrationControl.Notes
                                join d in this.registrationControl.Collaborators on e.UserId equals d.UserId
                                where e.Id.ToString() == d.Id

                                       select new CollaboratorMap
                                {
                                    NotesId = e.Id,
                                   SenderEmail = d.SenderEmail,
                                   ReceiverEmail = d.ReceiverEmail,
                                };
                foreach (var notes in ReceiveCollaboratorNotes)
                {
                    list.Add(notes);
                }
                foreach (var collaboratordetails in CollaboratesData)
                {
                    list1.Add(collaboratordetails);
                }
            }
            return (list.ToArray(), list1.ToArray());
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
            var notesData = from notes in this.registrationControl.Notes where notes.UserId.Equals(userId) && (!notes.Reminder.Year.Equals(0001)) select notes;
            foreach (var data in notesData)
            {
                list.Add(data);
            }

            return list;
        }

        /// <summary>
        /// Alarms the specified userid.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns>
        /// Notes data
        /// </returns>
        public async Task<IList<Notes>> Alarm(string userid)
        {
            var time = from times in registrationControl.Notes.Where(c => c.UserId == userid) select times;
            foreach (var alarm in time)
            {
                string alarmtime = alarm.Reminder.ToString("yyyy-M-dd hh:mm");
                string currenttime = DateTime.Now.ToString("yyyy-M-dd hh:mm");
                if (alarmtime == currenttime)
                {
                    await this.PushAlarmNotification(alarm);
                }
            }
            return null;
        }

        /// <summary>
        /// Pushes the alarm notification.
        /// </summary>
        /// <returns>result data</returns>
        public async Task<int> PushAlarmNotification(Notes notes)
        {
            try
            {
                // Get the server key from FCM console
                var serverKey = string.Format("key={0}", "AAAAaCKwEdQ:APA91bEr4NSV6brLrqstvZfegKNimcVNN1esDfIVQ5whKo-YQ32RIr9zM2p9cCuFV5RbJe8ZJ6MCc_oSO96Zlp6eju_uRxlO5G-aDdEaWLnW1UOK35bMOvL9bEgq2o4HiT85EPnRBizG");

                // Get the sender id from FCM console
                var senderId = string.Format("id={0}", "447258563028");

                var data = new
                {
                    to = "dSgnCdE3y5U:APA91bGrAp1IJfO3wJlyRH02HuL6q9wLpDzH9VZhsaippsstPaVbweLuqYhvyciy4rhMH0qnaNcPbE7gnweQnBw3ziYPjm2Zo27iTOQ4uS27C1PE3GiQMBbymog32dLuqLfl_TqLo1K9", // Recipient device token
                    notification = new
                    {
                        title = "Hello Xyz",
                        body = "This is Message from Aniket"
                    }
                };

                // Using Newtonsoft.Json
                var jsonBody = JsonConvert.SerializeObject(data);

                using (var httpRequest = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                    httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);

                        if (result.IsSuccessStatusCode)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}