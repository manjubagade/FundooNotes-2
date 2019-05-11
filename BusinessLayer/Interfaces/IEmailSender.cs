using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IEmailSender
    {
        /// <summary>
        /// Sends the email asynchronous.
        /// </summary>
        /// <param name="Email">The email.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <returns>result</returns>
        Task SendEmailAsync(string Email, string subject, string body);
    }
}