using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static BookingTour.Business.Service.EmailSender;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;

namespace BookingTour.Business.Service
{
	
		public class EmailSender : IEmailSender
		{
			private readonly string _smtpServer;
			private readonly int _smtpPort;
			private readonly string _smtpUser;
			private readonly string _smtpPass;
			private readonly int _timeout;

			public EmailSender(IConfiguration configuration)
			{
				_smtpServer = configuration["SmtpSettings:Server"];
				_smtpPort = int.Parse(configuration["SmtpSettings:Port"]);
				_smtpUser = configuration["SmtpSettings:User"];
				_smtpPass = configuration["SmtpSettings:Password"];
				_timeout = int.Parse(configuration["SmtpSettings:Timeout"]);
			}

			public async Task SendEmailAsync(string email, string subject, string htmlMessage)
			{
				using (var client = new SmtpClient(_smtpServer, _smtpPort)
				{
					Credentials = new NetworkCredential(_smtpUser, _smtpPass),
					EnableSsl = true,
					Timeout = _timeout
				})
				{
					var mailMessage = new MailMessage
					{
						From = new MailAddress(_smtpUser),
						Subject = subject,
						Body = htmlMessage,
						IsBodyHtml = true,
					};
					mailMessage.To.Add(email);

					await client.SendMailAsync(mailMessage);
				}
			}
		}
	}

