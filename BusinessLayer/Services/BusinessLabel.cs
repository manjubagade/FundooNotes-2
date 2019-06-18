// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="BusinessLabel.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

namespace BusinessLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessLayer.Interfaces;
    using Common.Models;
    using Microsoft.AspNetCore.Mvc;
    using RepositoryLayer.Interface;

    /// <summary>
    /// class for Business Layer
    /// </summary>
    /// <seealso cref="BusinessLayer.Interfaces.ILabel" />
    public class BusinessLabel : ILabel
    {
        /// <summary>
        /// The repository label
        /// </summary>
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
        public IList<Label> AccessLabel(Guid userId)
        {
            return this.repositoryLabel.ViewLabel(userId);
        }

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="notesLabel">The notes label.</param>
        /// <returns>
        /// success result
        /// </returns>
        /// <exception cref="Exception">The Exception</exception>
        public async Task<int> AddLabel(NotesLabel notesLabel)
        {
            try
            {
                return await this.repositoryLabel.AddNotesLabel(notesLabel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Changes the specified notes model.
        /// </summary>
        /// <param name="labelModel">The notes model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>updated data</returns>
        public async Task<int> Change(Label labelModel, int id)
        {
          return await this.repositoryLabel.UpdateLabel(labelModel, id);
        }

        /// <summary>
        /// Creates the specified Label.
        /// </summary>
        /// <param name="labelModel">The Label.</param>
        /// <returns>addition success data</returns>
        public async Task<int> Create(Label labelModel)
        {
            try
            {
               return await this.repositoryLabel.AddLabel(labelModel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result data</returns>
        public async Task<int> Delete(int id)
        {
            return await this.repositoryLabel.DeleteLabel(id);
        }

        /// <summary>
        /// Views the notes label.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns>NotesLabel Data</returns>
        public IList<NotesLabel> ViewNotesLabel(string userid)
        {
            return this.repositoryLabel.ViewNotesLabels(userid);
        }

        /// <summary>
        /// Deletes the notes label.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Success result</returns>
        public async Task<int> DeleteNotesLabel(int id)
        {
            return await this.repositoryLabel.DeleteNotesLabel(id);
        }
    }
}
