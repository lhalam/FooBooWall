using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace DataAccess.Entities
{
    public class VerificationLetter
    {
        public int Id { get; set; }
        public string Email{get; set;}
        public string Text { get; set; }
        public int Code { get; set; }
        public string Login { get; set; }
        public bool Verified { get; set; }
        public VerificationLetter()
        {
            Random r = new Random();
            int cd = r.Next(10000000, 99999999);
            Code = cd;
        }
        public void Send()
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.EnableSsl = true;
            client.Timeout = 100000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(
              "franko.pmi1@gmail.com", "pmi12345");
            MailMessage msg = new MailMessage();
            msg.To.Add(this.Email);
            msg.From = new MailAddress("franko.pmi1@gmail.com");
            msg.Subject = "Registration Verification";
            msg.Body = Text + " " + Code;
            client.Send(msg);
        }
    }
}
