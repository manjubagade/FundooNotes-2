// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelNotesHandler.cs" company="Bridgelabz">
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

    /// <summary>
    /// LabelNotesHandler class
    /// </summary>
    public class LabelNotesHandler
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
        /// Initializes a new instance of the <see cref="LabelNotesHandler"/> class.
        /// </summary>
        /// <param name="registrationControl">The registration control.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public LabelNotesHandler(RegistrationControl registrationControl, IDistributedCache distributedCache)
        {
            this.registrationControl = registrationControl;
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="LabelModel">The label model.</param>
        /// <returns>Success result</returns>
        /// <exception cref="Exception">The Exception</exception>
        public async Task<int> AddLabel(NotesLabel LabelModel)
        {
            try
            {
                //// Adding Notes in database
                var addNotesLabel = new NotesLabel()
                {
                    UserId = LabelModel.UserId,
                    NotesId = LabelModel.NotesId,
                    LabelId = LabelModel.LabelId

                };

                var result = this.registrationControl.NotesLabels.Add(addNotesLabel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return await this.SaveChangesAsync();
        }

        public IList<NotesLabel> ViewLabel(Guid userId)
        {
            var list = new List<NotesLabel>();
            var label = from Label in this.registrationControl.NotesLabels where Label.UserId.Equals(userId) orderby Label.UserId descending select Label;
            foreach (var item in label)
            {
                list.Add(item);
            }

            var cacheKey = label.ToString();
            this.distributedCache.GetString(cacheKey);
            this.distributedCache.SetString(cacheKey, label.ToString());
            return label.ToArray();
          
        }


        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns>Result data</returns>
        public async Task<int> SaveChangesAsync()
        {
            //// for changes the Database Entries
            return await this.registrationControl.SaveChangesAsync();
             
        }
       
        ///// <summary>
        ///// Gets the Labels.
        ///// </summary>
        ///// <param name="userId">The user identifier.</param>
        ///// <returns>return LabelModel</returns>
        //public IList<Label> ViewLabel(Guid userId)
        //{
        //    var list = new List<Label>();
        //    var label = from Label in this.registrationControl.Labels where Label.UserId == userId orderby Label.UserId descending select Label;
        //    foreach (var item in label)
        //    {
        //        list.Add(item);
        //    }
        //    var cacheKey = label.ToString();
        //    this.distributedCache.GetString(cacheKey);
        //    this.distributedCache.SetString(cacheKey, label.ToString());
        //    return label.ToArray();
        //}
        
        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result data</returns>
        public async Task<int> DeleteLabel(int id)
        {
            try
            {
                NotesLabel label = await this.registrationControl.NotesLabels.FindAsync(id);
                registrationControl.NotesLabels.Remove(label);
                return registrationControl.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
           
        }
    }
}