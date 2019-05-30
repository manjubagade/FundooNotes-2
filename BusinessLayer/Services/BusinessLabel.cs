// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="BusinessLabel.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

using BusinessLayer.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class BusinessLabel : ILabel
    {
        private readonly IRepositoryLabel repositoryLabel;

        public BusinessLabel(IRepositoryLabel repositoryLabel)
        {
            this.repositoryLabel = repositoryLabel;
        }

        /// <summary>
        /// Accesses the notes.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>The List</returns>
        public  IList<Label> AccessLabel(Guid UserId)
        {
            return this.repositoryLabel.ViewLabel(UserId);
             
        }

        /// <summary>
        /// Changes the specified notes model.
        /// </summary>
        /// <param name="LabelModel">The notes model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<int> Change(Label LabelModel, int id)
        {

            this.repositoryLabel.UpdateLabel(LabelModel, id);
            var result = await this.repositoryLabel.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Creates the specified notes model.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns></returns>
        public async Task<int> Create(Label LabelModel)
        {
            try
            {
                 this.repositoryLabel.AddLabel(LabelModel);
                
                var result =await this.repositoryLabel.SaveChangesAsync();
                return result;
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
            return await this.repositoryLabel.DeleteLabel(id);
        }
     
    }
}
