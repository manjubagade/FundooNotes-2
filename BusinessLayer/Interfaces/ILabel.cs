// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="ILabel.cs" company="Bridgelabz">
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

    /// <summary>
    /// Interface ILabel in Business Layer
    /// </summary>
    public interface ILabel
    {
        /// <summary>
        /// Creates the specified notes model.
        /// </summary>
        /// <param name="LabelModel">The notes model.</param>
        /// <returns>result data</returns>
        Task<int> Create(Label labelModel);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>result data</returns>
        Task<int> Delete(int id);

        /// <summary>
        /// Changes the specified notes model.
        /// </summary>
        /// <param name="LabelModel">The notes model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>result data</returns>
        Task<int> Change(Label labelModel, int id);

        /// <summary>
        /// Accesses the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>result of label data</returns>
        IList<Label> AccessLabel(Guid userId);

        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="notesLabel">The notes label.</param>
        /// <returns>success result</returns>
        Task<int> AddLabel(NotesLabel notesLabel);

        /// <summary>
        /// Views the notes label.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns>List of NotesLabel data</returns>
        IList<NotesLabel> ViewNotesLabel(string userid);

        /// <summary>
        /// Deletes the notes label.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>delete success data</returns>
        Task<int> DeleteNotesLabel(int id);
    }
}