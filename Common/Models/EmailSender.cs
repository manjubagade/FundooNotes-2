using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class EmailSender
    {
       public string FromAddress { get; set; }
       public string SmtpClient { get; set; }
       public string UserId { get; set; }
       public string Password { get; set; }
       public string SMTPPort { get; set; }
       public bool EnableSSL { get; set; }
    }
}
