// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="NotesCreation.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------
using BusinessLayer.Interfaces;
using FundooApi;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NotesCreation : INotes
    {
        private readonly IRepositoryNotes repositoryNotes;

        public NotesCreation(IRepositoryNotes repositoryNotes)
        {
            this.repositoryNotes = repositoryNotes;
        }

        /// <summary>
        /// Accesses the notes.
        /// </summary>
        /// <param name="UserId">The user identifier.</param>
        /// <returns>The List</returns>
        public IList<Notes> AccessNotes(Guid UserId)
        {
            return this.repositoryNotes.ViewNotes(UserId);
        }

        public Task<int> Change(Notes notesModel, int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Create(Notes notesModel)
        {
            this.repositoryNotes.AddNotes(notesModel);
            var result = this.repositoryNotes.SaveChangesAsync();
            return result;
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
