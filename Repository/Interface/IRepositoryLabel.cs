// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepositoryLabel.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

namespace RepositoryLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Models;

    /// <summary>
    /// IRepositoryLabel for Handling Label Operations
    /// </summary>
    public interface IRepositoryLabel
    {
        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <returns>add result</returns>
        Task<int> AddLabel(Label labelModel);

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns>data result</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Views the label.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Label Data</returns>
        IList<Label> ViewLabel(Guid userId);

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>data value</returns>
        Task<int> DeleteLabel(int id);

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="labelModel">The label model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>updated success result</returns>
        Task<int> UpdateLabel(Label labelModel, int id);

        Task<int> AddNotesLabel(NotesLabel notesLabel);
        IList<NotesLabel> ViewNotesLabels(string userid);
        Task<int> DeleteNotesLabel(int id);
    }
}