using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IEmailSender
    {
       Task SendEmail(string userId, string Email, string subject, string body);
    }
}
