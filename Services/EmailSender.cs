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
        public Task SendEmail(string code)
        {
            /*  SmtpClient smtpClient = new SmtpClient();
             smtpClient.Host = "smtp.gmail.com";
             smtpClient.Port = 587;
             smtpClient.Credentials =new System.Net.NetworkCredential("Fundoo@gmail.com","Abcdef123");
             smtpClient.EnableSsl = true;
             MailMessage mailMessage = new MailMessage();
             mailMessage.Subject = "Forgot Password";
             mailMessage.Body = "Click" + code + "here for Forgot Password";
             string toaddress = "Kambleaniket17@gmail.com";
             mailMessage.To.Add(toaddress);
             string fromaddress = "FundooApp<Fundoo@gmail.com>";
             mailMessage.From=new MailAddress(fromaddress);
             try
             {
                 smtpClient.Send(mailMessage);
             }
             catch
             {
                 throw;
             }
             return null;*/

            /*var fromAddress = new MailAddress("asd@asd.com", "Fundoo");
            var toAddress = new MailAddress("kambleaniket17@gmail.com", "User");

            const string subject = "Forgot PassWord";
            const string body = "Body";
            
            var smtp = new SmtpClient
            {
                
                Host = "smtp.gmail.com",
                Port = 587,
               
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
            return null;*/
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.Host = "smtp.gmail.com";
            client.Port = 587;                 // setup Smtp authentication    
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("kambleaniket17@gmail.com", "anupun252");
            client.Credentials = credentials;
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("Fundoo@gmail.com");

            msg.To.Add(new MailAddress("kambleaniket17@gmail.com"));

            msg.Subject = "Forgot Password";
            msg.IsBodyHtml = true;
            msg.Body = "click <a href=\"" + code + "\">here for forgot Password</a>";
            client.EnableSsl = true;
            client.Send(msg);
            return null;
        }
    }
}
