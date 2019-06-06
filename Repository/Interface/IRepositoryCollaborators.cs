using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
     public interface IRepositoryCollaborators
    {
       
        Task<int> AddCollaborators(Collaborators collaboratorsModel);

        Task<int> SaveChangesAsync();
        
        IList<CollaboratorMap> ViewCollaborators(Guid userId);
        
        Task<int> DeleteCollaborators(int id);
       
        Task<int> UpdateCollaborators(Collaborators CollaboratorslModel, int id);
    }
}
