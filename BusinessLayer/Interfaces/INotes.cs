using FundooApi;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface INotes
    {
        Task<int> Create(Notes notesModel);

       
        Task<int> Delete(int id);

       
        Task<int> Change(Notes notesModel, int id);
       
        IList<Notes> AccessNotes(Guid userId);

        string AddImage(IFormFile file, int id);
    }
}
