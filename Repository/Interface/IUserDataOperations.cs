using FundooApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserDataOperations
    {
        Task RegisterData(UserRegistration userRegistration);
    }
}
