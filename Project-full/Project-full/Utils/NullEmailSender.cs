using Microsoft.AspNetCore.Identity.UI.Services;

namespace Project_full.Utils
{
	public class NullEmailSender : IEmailSender
	{
		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			Console.WriteLine($"Email to: {email}");
			Console.WriteLine($"Subject: {subject}");
			Console.WriteLine($"Message: {htmlMessage}");
			return Task.CompletedTask;
		}
	}
}
