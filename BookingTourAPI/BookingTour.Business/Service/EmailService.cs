using BookingTour.Business.Service.IService;
using BookingTour.Model;
using MailKit.Net.Smtp;
using MimeKit;

namespace User.Management.Service.Services
{
	public class EmailService : IEmailService
	{
		private readonly EmailConfiguration _emailConfig;

		public EmailService(EmailConfiguration emailConfig)
		{
			_emailConfig = emailConfig;
		}

		public void SendEmail(Message message)
		{
			var emailMessage = CreateEmailMessage(message);
			Send(emailMessage);
		}

		private MimeMessage CreateEmailMessage(Message message)
		{
			var emailMessage = new MimeMessage();
			emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
			emailMessage.To.AddRange(message.To);
			emailMessage.Subject = message.Subject;

			// Sử dụng BodyBuilder để hỗ trợ HTML
			var builder = new BodyBuilder
			{
				HtmlBody = message.Content // Nội dung HTML
			};

			emailMessage.Body = builder.ToMessageBody();
			return emailMessage;
		}

		private void Send(MimeMessage mailMessage)
		{
			using var client = new SmtpClient();
			try
			{
				client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
				client.AuthenticationMechanisms.Remove("XOAUTH2");
				client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
				client.Send(mailMessage);
			}
			catch
			{
				throw;
			}
			finally
			{
				client.Disconnect(true);
				client.Dispose();
			}
		}
	}
}
