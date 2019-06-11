﻿// -------------------------------------------------------------------------------------------------------------------------
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
    /// <summary>
    /// class for Business Layer
    /// </summary>
    /// <seealso cref="BusinessLayer.Interfaces.ILabel" />
    public class BusinessLabel : ILabel
    {
        private readonly IRepositoryLabel repositoryLabel;

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessLabel"/> class.
        /// </summary>
        /// <param name="repositoryLabel">The repository label.</param>
        public BusinessLabel(IRepositoryLabel repositoryLabel)
        {
            this.repositoryLabel = repositoryLabel;
        }

        /// <summary>
        /// Accesses the notes.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>The List</returns>
        public IList<Label> AccessLabel(Guid UserId)
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

          return await this.repositoryLabel.UpdateLabel(LabelModel, id);
           
             
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
               return await this.repositoryLabel.AddLabel(LabelModel);
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