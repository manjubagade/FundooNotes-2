// -------------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailSender.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator name="Aniket Kamble"/>
// ---------------------------------------------------------------------------------------------------------------------------
namespace BusinessLayer.Services
{
    using Experimental.System.Messaging;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Net;
    using System.Net.Mail;

    using System.Threading.Tasks;

    public class EmailSenders : IEmailSender
    {
        public Task SendEmailAsync(string Email, string subject, string body)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.Host = "smtp.gmail.com";
            client.Port = 587;                 //// setup Smtp authentication    
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("privateuser1199@gmail.com", "private252");
            client.Credentials = credentials;
            MailMessage msg = new MailMessage();
            string fromaddress = "FundooApp<Fundoo@gmail.com>";
            msg.From = new MailAddress(fromaddress);
            msg.To.Add(new MailAddress(Email));

            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = body;
            client.EnableSsl = true;
            client.Send(msg);
            return Task.FromResult(0);
        }




        ///// <summary>
        ///// Sends the email asynchronous.
        ///// </summary>
        ///// <param name="email">The email.</param>
        ///// <param name="subject">The subject.</param>
        ///// <param name="message">The message.</param>
        ///// <returns></returns>
        //public Task SendEmailAsync(string email, string subject, string message)
        //{
        //    try
        //    {
        //        const string queueName = @".\private$\msmq";

        //        MessageQueue msMq = null;
        //        if (!MessageQueue.Exists(queueName))
        //        {
        //            msMq = MessageQueue.Create(queueName);
        //        }
        //        else
        //        {
        //            msMq = new MessageQueue(queueName);
        //        }


        //            var credentials = new NetworkCredential("privateuser1199@gmail.com", "private252");
        //            var mail = new MailMessage()
        //            {
        //                From = new MailAddress("privateuser1199@gmail.com"),
        //                Subject = subject,
        //                Body = message,
        //                IsBodyHtml = true
        //            };
        //            mail.To.Add(new MailAddress(email));

        //            var client = new SmtpClient()
        //            {
        //                Port = 587,
        //                DeliveryMethod = SmtpDeliveryMethod.Network,
        //                UseDefaultCredentials = false,
        //                Host = "smtp.gmail.com",
        //                EnableSsl = true,
        //                Credentials = credentials
        //            };
        //            client.Send(mail);
        //        }

        //        catch (Exception e)
        //        {
        //            Console.Write(e.ToString());
        //        }

        //        return Task.CompletedTask;
        //    }
    }
    }
