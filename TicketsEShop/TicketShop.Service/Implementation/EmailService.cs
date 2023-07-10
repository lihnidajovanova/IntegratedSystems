using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TicketShop.Domain;
using TicketShop.Domain.DomainModels;
using TicketShop.Service.Interface;

namespace TicketShop.Service.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(EmailSettings settings)
        {
            _settings = settings;
        }

        public async Task SendEmailAsync(List<EmailMessage> allMails)
        {
            List<MimeMessage> messages = new List<MimeMessage>();
            foreach (var item in allMails)
            {
                var mailMessage = new MimeMessage
                {
                    Sender = new MailboxAddress(_settings.SenderName, _settings.SmtpUserName),
                    Subject = item.Subject
                };
                mailMessage.From.Add(new MailboxAddress(_settings.EmailDisplayName, _settings.SmtpUserName));
                mailMessage.Body = new TextPart(TextFormat.Plain)
                {
                    Text = item.Content
                };
                mailMessage.To.Add(new MailboxAddress(item.MailTo));
                messages.Add(mailMessage);
            }

            try
            {
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    var socketOption = _settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;
                    await smtp.ConnectAsync(_settings.SmtpServer, _settings.SmtpServerPort, socketOption);

                    if(string.IsNullOrEmpty(_settings.SmtpUserName))
                    {
                        await smtp.AuthenticateAsync(_settings.SmtpUserName, _settings.SmtpPassword);
                    }

                    foreach (var item in messages)
                    {
                        await smtp.SendAsync(item);
                    }
                    await smtp.DisconnectAsync(true);
                }
            } catch(SmtpException ex)
            {
                throw ex;
            }
        }

        public Task SendEmailAsync(object p)
        {
            throw new NotImplementedException();
        }
    }
}
