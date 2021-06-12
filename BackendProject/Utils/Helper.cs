using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BackendProject.Utils
{
    public class Helper
    {
        public static async Task SendMessage(string messageSubject, string messageBody, string mailTo)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("nazimnh@code.edu.az", "221edd1sd");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage message = new MailMessage("nazimnh@code.edu.az", mailTo);
            message.Subject = messageSubject;
            message.Body = messageBody;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            try
            {
                await client.SendMailAsync(message);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
