// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelHandler.cs" company="Bridgelabz">
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
    /// class for Label Operations
    /// </summary>
    /// <seealso cref="RepositoryLayer.Interface.IRepositoryLabel" />
    public class LabelHandler : IRepositoryLabel
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
        /// Initializes a new instance of the <see cref="LabelHandler"/> class.
        /// </summary>
        /// <param name="registrationControl">The registration control.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        public LabelHandler(RegistrationControl registrationControl, IDistributedCache distributedCache)
        {
            this.registrationControl = registrationControl;
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="LabelModel">The label model.</param>
        /// <returns>Success result</returns>
        /// <exception cref="Exception">The exception</exception>
        public async Task<int> AddLabel(Label labelModel)
        {
            try
            {
                    //// Add Notes
                    var addLabel = new Label()
                    {
                        UserId = labelModel.UserId,
                        Labels = labelModel.Labels
                    };
                    var result = this.registrationControl.Labels.Add(addLabel);
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
        /// Updates the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// updated success result
        /// </returns>
        /// <exception cref="Exception">The Exception</exception>
        public async Task<int> UpdateLabel(Label labelModel, int id)
        {
            try
            { 
            Label label = this.registrationControl.Labels.Where<Label>(c => c.Id == id).FirstOrDefault();
            label.Labels = labelModel.Labels;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return await this.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the Labels.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return LabelModel</returns>
        public IList<Label> ViewLabel(Guid userId)
        {
            var list = new List<Label>();
            var label = from Label in this.registrationControl.Labels where Label.UserId == userId orderby Label.UserId descending select Label;
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
        /// Deletes the label.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result data</returns>
        public async Task<int> DeleteLabel(int id)
        {
            try
            {
                Label label = await this.registrationControl.Labels.FindAsync(id);
                this.registrationControl.Labels.Remove(label);
                var result = this.registrationControl.SaveChanges();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Adds the notes label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>result data</returns>
        /// <exception cref="Exception">The Exception</exception>
        public async Task<int> AddNotesLabel(NotesLabel labelModel)
        {
            try
            {
                var labelData = from t in this.registrationControl.NotesLabels where t.UserId == labelModel.UserId select t;
                foreach (var datas in labelData.ToList())
                {
                    if (datas.NotesId == labelModel.NotesId && datas.LabelId == labelModel.LabelId)
                    {
                        return Convert.ToInt32(false.ToString());
                    }
                }
                //// Adding Notes in database
                var addNotesLabel = new NotesLabel()
                {
                    UserId = labelModel.UserId,
                    NotesId = labelModel.NotesId,
                    LabelId = labelModel.LabelId
                };

                var result = this.registrationControl.NotesLabels.Add(addNotesLabel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
            return await this.SaveChangesAsync();
        }

        /// <summary>
        /// Views the notes
        /// </summary>
        /// <param name="userId">The userId.</param>
        /// <returns>List data</returns>
        public IList<NotesLabel> ViewNotesLabels(string userId)
        {
            var list = new List<NotesLabel>();
            var label = from Label in this.registrationControl.NotesLabels
                        where Label.UserId == userId
                        select Label;

            foreach (var item in label)
            {
                list.Add(item);
            }

            return list.ToArray();
        }

        /// <summary>
        /// Delete Notes Label
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>data result</returns>
        public async Task<int> DeleteNotesLabel(int id)
        {
            try
            {
                NotesLabel label = await this.registrationControl.NotesLabels.FindAsync(id);
                this.registrationControl.NotesLabels.Remove(label);
                var result = this.registrationControl.SaveChanges();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}