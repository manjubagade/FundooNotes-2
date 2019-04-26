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
    }
}
