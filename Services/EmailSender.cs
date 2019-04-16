namespace Services
{
    using Microsoft.Extensions.Configuration;
    using Services.Interfaces;
    using System;
  //  using System.Data.Entity;
    using System.Net.Mail;
    using System.Threading.Tasks;

    public class EmailSender : IEmailSender
    {
        public Task SendEmail(string userId,string code,string Email)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.Host = "smtp.gmail.com";
            client.Port = 587;                 // setup Smtp authentication    
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("privateuser1199@gmail.com", "private252");
            client.Credentials = credentials;
            MailMessage msg = new MailMessage();
            string fromaddress = "FundooApp<Fundoo@gmail.com>";
            msg.From = new MailAddress(fromaddress);
            msg.To.Add(new MailAddress(Email));

            msg.Subject = "Forgot Password";
            msg.IsBodyHtml = true;
            msg.Body = "Please reset your password by clicking here: <a href=\"" + code + "\">link</a>";
            client.EnableSsl = true;
            client.Send(msg);
            return Task.FromResult(0);
        }
    }
}
