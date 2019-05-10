// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="ILabel.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

namespace BusinessLayer.Interfaces
{
    using Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface ILabel in Business Layer
    /// </summary>
    public interface ILabel
    {

        /// <summary>
        /// Creates the specified notes model.
        /// </summary>
        /// <param name="LabelModel">The notes model.</param>
        /// <returns></returns>
        Task<int> Create(Label LabelModel);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<int> Delete(int id);

        /// <summary>
        /// Changes the specified notes model.
        /// </summary>
        /// <param name="LabelModel">The notes model.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<int> Change(Label LabelModel, int id);

        /// <summary>
        /// Accesses the notes.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IList<Label> AccessLabel(Guid userId);
    }
}