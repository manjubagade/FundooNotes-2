using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICollborators
    {
        Task<int> Create(Collaborators collaboratorsModel);

       
        Task<int> Delete(int id);

        
        Task<int> Change(Collaborators collaboratorsModel, int id);


        IList<CollaboratorMap> AccessCollaborators(Guid userId);

    }
}
