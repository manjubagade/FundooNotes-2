// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="INotes.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

namespace BusinessLayer.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Models;
    using FundooApi;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// INotes interface
    /// </summary>
    public interface INotes
    {
        /// <summary>
        /// Creates the specified notes model.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <returns>result of data</returns>
        Task<int> Create(Notes notesModel);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result data</returns>
        Task<int> Delete(int id);

        /// <summary>
        /// Changes the specified notes model.
        /// </summary>
        /// <param name="notesModel">The notes model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>result data</returns>
        Task<int> Change(Notes notesModel, int id);

        /// <summary>
        /// Accesses the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>result of notes data</returns>
        (IList<Notes>, IList<CollaboratorMap>) AccessNotes(string userId);

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>string url</returns>
        Task<string> AddImage(IFormFile file, int id);

        /// <summary>
        /// Trashes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return list</returns>
        IList<Notes> Trash(string userId);

        /// <summary>
        /// Archives the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>return list</returns>
        IList<Notes> Archive(string userId);

        /// <summary>
        /// Reminders the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>notes data</returns>
        IList<Notes> Reminder(string userId);

        /// <summary>
        /// Alarms the specified userid.
        /// </summary>
        /// <param name="Userid">The userid.</param>
        /// <returns>list of data</returns>
        Task<IList<Notes>> Alarm(string userid);
    }
}
