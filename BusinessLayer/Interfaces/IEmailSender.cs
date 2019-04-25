using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string Email, string subject, string body);
    }
}
