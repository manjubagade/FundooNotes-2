using FundooApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    interface INotes
    {
        Task<int> Create(Notes notesModel);

       
        Task<int> Delete(int id);

       
        Task<int> Change(Notes notesModel, int id);

       
        IList<Notes> AccessNotes(Guid userId);
    }
}
