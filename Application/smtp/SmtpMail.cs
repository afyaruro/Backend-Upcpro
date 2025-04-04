using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Application.smtp
{
    public class SmtpMail
    {
        public static async Task<string> enviarMail(string destinatario, string subject, string htmlBody)
        {

            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string smtpUser = "afyarurodeveloment@gmail.com";
            string smtpPassword = "izts nstz nvdp pxqi";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(smtpUser);
                mail.To.Add(destinatario);
                mail.Subject = subject;



                mail.Body = htmlBody;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(smtpServer))
                {
                    smtp.Port = smtpPort;
                    smtp.Credentials = new NetworkCredential(smtpUser, smtpPassword);
                    smtp.EnableSsl = true;

                    try
                    {
                        await smtp.SendMailAsync(mail);
                        return "Correo enviado exitosamente.";
                    }
                    catch (Exception ex)
                    {
                        return "Error al enviar el correo: " + ex.Message;
                    }
                }
            }
        }
    }
}
