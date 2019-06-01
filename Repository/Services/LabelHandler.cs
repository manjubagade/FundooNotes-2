// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelHandler.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------
namespace RepositoryLayer.Services
{
    using Common.Models;
    using FundooApi;
    using Microsoft.Extensions.Caching.Distributed;
    using RepositoryLayer.Interface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// class for Label Operations
    /// </summary>
    /// <seealso cref="RepositoryLayer.Interface.IRepositoryLabel" />
    public class LabelHandler:IRepositoryLabel
    {
        private readonly RegistrationControl registrationControl;
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
        /// <exception cref="Exception"></exception>
        public async Task<int> AddLabel(Label LabelModel)
        {
            try
            {
                    //// Add Notes
                    var addLabel = new Label()
                    {
                        UserId = LabelModel.UserId,
                        Labels = LabelModel.Labels
                    };
                    var result = this.registrationControl.Labels.Add(addLabel);
               
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
        /// <returns>Result Int int</returns>
        public async Task<int> SaveChangesAsync()
        {
            var result =await this.registrationControl.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <param name="id">The identifier.</param>
        public async Task<int> UpdateLabel(Label labelModel, int id)
        {
            try
            { 
            Label label = this.registrationControl.Labels.Where<Label>(c => c.Id == id).FirstOrDefault();
            label.Labels = labelModel.Labels;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return await SaveChangesAsync();
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
        /// <returns>result in int</returns>
        public async Task<int> DeleteLabel(int id)
        {
            try
            {
                Label label = await this.registrationControl.Labels.FindAsync(id);
                registrationControl.Labels.Remove(label);
                var result = registrationControl.SaveChanges();
                return result;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }
    }
}