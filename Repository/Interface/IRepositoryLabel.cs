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
    using FundooApi;

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

        /// <summary>
        /// Adds the notesLabel.
        /// </summary>
        /// <param name="notesLabel">The NotesLabel.</param>
        /// <returns>success data</returns>
        Task<int> AddNotesLabel(NotesLabel notesLabel);

        /// <summary>
        /// Views the notes labels.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns>List of data</returns>
        IList<NotesLabel> ViewNotesLabels(string userid);

        /// <summary>
        /// Deletes the notes label.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result data</returns>
        Task<int> DeleteNotesLabel(int id);

        IList<Notes> ViewLabelNotes(NotesLabel notesLabelmodel);
    }
}