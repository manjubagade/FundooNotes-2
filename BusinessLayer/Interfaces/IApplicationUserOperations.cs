using FundooApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IApplicationUserOperations
    {
        Task RegisterData(UserRegistration userRegistration);
    }
}
