using DataAccess;
using DataAccess.Models;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace API.Services
{
    public class EmailService
    {
        public static void SendRegistrationInformation(User user)
        {
            var credentials = new NetworkCredential("admin", "0000");
            var smtpClient = new SmtpClient()
            {
                Host = "localhost.com",
                Port = 25,
                UseDefaultCredentials = false,
                Credentials = credentials
            };

            InformAdmins(user, smtpClient);
            InformNewUser(user, smtpClient);
        }

        private static void InformAdmins(User user, SmtpClient client)
        {
            var mailAddress = new MailAddress("noreply@gmail.com");
            MailMessage message;
            if (DataManager.Users.Any(c => c.UserRole != DataAccess.Enums.Roles.Administrator && c.Id != user.Id))
            {
                message = new MailMessage()
                {
                    Subject = "New user registered",
                    Body = $"A new user has been registered under the email {user.Email}",
                    From = mailAddress
                };

                foreach (var admin in DataManager.Users.Where(c => c.UserRole == DataAccess.Enums.Roles.Administrator && c.Id != user.Id))
                {
                    message.To.Add(admin.Email);
                }

                client.Send(message);
            }
        }

        private static void InformNewUser(User user, SmtpClient client)
        {
            var mailAddress = new MailAddress("noreply@gmail.com");
            var message = new MailMessage()
            {
                Subject = "New account registration",
                Body = $"Your user has been successfully registered with the email {user.Email}",
                From = mailAddress
            };
            message.To.Add(user.Email);
            client.Send(message);
        }
    }
}