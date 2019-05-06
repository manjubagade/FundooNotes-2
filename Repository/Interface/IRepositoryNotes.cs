// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepositoryNotes.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------

using FundooApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IRepositoryNotes
    {
        void AddNotes(Notes notes);
        Task<int> SaveChangesAsync();
        IList<Notes> ViewNotes(Guid userId);
        Task<int> DeleteNotes(int id);
        void UpdateNotes(Notes notes, int id);
    }
}
