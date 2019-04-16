using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IEmailSender
    {
       Task SendEmail(string userId,string code,string Email);
    }
}
