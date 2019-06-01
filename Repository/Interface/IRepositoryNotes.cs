// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepositoryNotes.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

using FundooApi;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    /// <summary>
    /// Interface IRepositoryNotes for Handling Notes Operations
    /// </summary>
    public interface IRepositoryNotes
    {
        /// <summary>
        /// Adds the notes.
        /// </summary>
        /// <param name="notes">The notes.</param>
        Task<int> AddNotes(Notes notes);

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Views the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IList<Notes> ViewNotes(Guid userId);

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>int result</returns>
        Task<int> DeleteNotes(int id);

        /// <summary>
        /// Updates the notes.
        /// </summary>
        /// <param name="notes">The notes.</param>
        /// <param name="id">The identifier.</param>
        Task<int> UpdateNotes(Notes notes, int id);

        /// <summary>
        /// Images the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>Uri string</returns>
        string Image(IFormFile file, int id);

        /// <summary>
        /// Archives the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IList<Notes> Archive(string userId);

        /// <summary>
        /// Trashes the note.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IList<Notes> TrashNote(string userId);

        /// <summary>
        /// Reminders the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IList<Notes> Reminder(string userId);

    }
}
