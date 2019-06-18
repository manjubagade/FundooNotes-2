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
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Policy;
    using System.Text;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
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
        public IList<Notes> ViewNotes(string userId)
        {
            var list = new List<Notes>();
            var note = from notes in this.registrationControl.Notes where (notes.UserId == userId && notes.IsTrash == false && notes.IsArchive == false) orderby notes.UserId descending select notes;
           
            foreach (var item in note)
            {
                list.Add(item);
            }
            var user = from users in registrationControl.Application where users.Id == userId select users;
            foreach(var users in user) { 
             var innerJoin = from e in this.registrationControl.Notes
                                join d in this.registrationControl.Collaborators on e.UserId equals d.UserId where e.Id.ToString() == d.Id && d.ReceiverEmail==users.Email
                                
                                select new Notes
                                {
                                    Id=e.Id,
                                    UserId = e.UserId,
                                    Title = e.Title,
                                    Description = e.Description,
                                    Label = e.Label,
                                   Image = e.Image,
                               };
            foreach(var collaborator in innerJoin)
            {
                list.Add(collaborator);
            }
            }

            var cacheKey = note.ToString();
            this.distributedCache.GetString(cacheKey);
            this.distributedCache.SetString(cacheKey, note.ToString());
            return list.ToArray();
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
        public IList<Notes> Alarm(string userid)
        {
            ////var list = new List<Notes>();
            ////var Alarm = from notes in this.registrationControl.Notes where notes.UserId == Userid select notes.Reminder;
            ////var time = DateTime.Now;
            ////var time= "0001 - 01 - 01 00:00:00.0000000";
            ////if (Alarm.Equals(time))
            ////{
            ////    Console.WriteLine("Alarm Is Successfully Display");
            ////}

            var a = 10;
            var b = 20 - a;
            if (a == b)
            {
                this.PushAlarmNotification();
            }

            return null;
        }

        /// <summary>
        /// Pushes the alarm notification.
        /// </summary>
        /// <returns>result data</returns>
        public int PushAlarmNotification()
        {
            ////           var client = new HttpClient();
            ////            var data = @"
            //// notification: {
            ////  'title': 'Hello World', 
            ////  'body': 'This is Message from Aniket'
            //// },
            //// 'to' : 'cViOWmXN0FU:APA91bGoixBJQSdzn4i2xM0sq0Srb4GfcIrQh6Yvntzu4ltP7uOJaopf5QHhAtLYwDiL77czIrhjbeDt893aKjaRusTpjEwoL1y5XtjGisLwCEg9OMp7Iq_V3Mmh-WDS40aJhGHGRTcx'
            ////}";
            ////            var msg = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "https://fcm.googleapis.com/fcm/send");
            ////            HttpRequestMessage()
            ////            // msg.Headers.Add("Content-Type", "application / json");
            ////            //msg.Headers.Add("Authorization", "'key=AAAAaCKwEdQ:APA91bEr4NSV6brLrqstvZfegKNimcVNN1esDfIVQ5whKo-YQ32RIr9zM2p9cCuFV5RbJe8ZJ6MCc_oSO96Zlp6eju_uRxlO5G-aDdEaWLnW1UOK35bMOvL9bEgq2o4HiT85EPnRBizG'");
            ////           client.DefaultRequestHeaders.Add("Authorization", "key AAAAaCKwEdQ:APA91bEr4NSV6brLrqstvZfegKNimcVNN1esDfIVQ5whKo-YQ32RIr9zM2p9cCuFV5RbJe8ZJ6MCc_oSO96Zlp6eju_uRxlO5G-aDdEaWLnW1UOK35bMOvL9bEgq2o4HiT85EPnRBizG");

            ////                msg.Content = new StringContent(
            ////                    data,
            ////                    UnicodeEncoding.UTF8,
            ////                    "application/json");
            ////                client.SendAsync(msg);
            ////            // return new FirebaseRequest(HttpMethod.POST, https://fcm.googleapis.com/fcm/send , jsonData).Execute();
            ////            return 1;
            ////        }
            ////var request="";
            var data = @"
   headers: 
    { 
       'Authorization': 'key=AAAAaCKwEdQ:APA91bEr4NSV6brLrqstvZfegKNimcVNN1esDfIVQ5whKo-YQ32RIr9zM2p9cCuFV5RbJe8ZJ6MCc_oSO96Zlp6eju_uRxlO5G-aDdEaWLnW1UOK35bMOvL9bEgq2o4HiT85EPnRBizG',
     },
             notification: {
              'title': 'Hello World', 
              'body': 'This is Message from Aniket'
             },
             'to' : 'cViOWmXN0FU:APA91bGoixBJQSdzn4i2xM0sq0Srb4GfcIrQh6Yvntzu4ltP7uOJaopf5QHhAtLYwDiL77czIrhjbeDt893aKjaRusTpjEwoL1y5XtjGisLwCEg9OMp7Iq_V3Mmh-WDS40aJhGHGRTcx'
            }";
            var client = new HttpClient();
            var msg = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "https://fcm.googleapis.com/fcm/send");
            msg.Headers.Add("Authorization", "key = AAAAaCKwEdQ:APA91bEr4NSV6brLrqstvZfegKNimcVNN1esDfIVQ5whKo - YQ32RIr9zM2p9cCuFV5RbJe8ZJ6MCc_oSO96Zlp6eju_uRxlO5G - aDdEaWLnW1UOK35bMOvL9bEgq2o4HiT85EPnRBizG");
            msg.Content = new StringContent(
                data,
                UnicodeEncoding.UTF8,
                "application/json");
            return 1;
           
        }
        //public void SendPushNotification(string deviceTokens, string title, string body, object data)
        //{


        //    bool sent = false;

        //    if (deviceTokens != null)
        //    {
              

        //        var notification = new notification()
        //        {


        //            title = title,
        //            text = body,


        //            data = data,
        //            registration_ids = deviceTokens
        //        };

                
        //        string jsonMessage = JsonConvert.SerializeObject(notification);

        //        /*
        //         ------ JSON STRUCTURE ------
        //         {
        //            notification: {
        //                            title: "",
        //                            text: ""
        //                            },
        //            data: {
        //                    action: "Play",
        //                    playerId: 5
        //                    },
        //            registration_ids = ["id1", "id2"]
        //         }
        //         ------ JSON STRUCTURE ------
        //         */

        //        //Create request to Firebase API
        //        var request = new HttpRequestMessage(HttpMethod.Post, FireBasePushNotificationsURL);

        //        request.Headers.TryAddWithoutValidation("Authorization", "key=" + ServerKey);
        //        request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

        //        HttpResponseMessage result;
        //        using (var client = new HttpClient())
        //        {
        //            result = await client.SendAsync(request);
        //            sent = sent && result.IsSuccessStatusCode;
        //        }
        //    }
        //}
        //        return sent;
        //    }

}
}