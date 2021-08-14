using System.IO;
using System.Net.Mail;
using System.Web;

using UserAPI.Interfaces;

namespace UserAPI.Services
{
    public class SendVerificationMail : ISendVerificationMail
    {
        
        public void SmtpServer(string email, string userName)
        {
            StreamReader reader = new StreamReader(new FileStream("Templates/ConfirmEmail.html", FileMode.Open));
            
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            var content = HttpUtility.HtmlEncode(reader.ReadToEnd());
            var myWriter = new StringWriter();
            HttpUtility.HtmlDecode(content, myWriter);
            var htmlDecoded = myWriter.ToString();
            smtpClient.Credentials = new System.Net.NetworkCredential("", "");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("lanegoworld@gmail.com", "E-mail Verification LaneGo! World");
            mail.To.Add(new MailAddress(email));
            mail.Subject = "Please Verify your Email";
            mail.IsBodyHtml = true;
            mail.Body = htmlDecoded;
            smtpClient.Send(mail);
        }
    }
}
