// See https://aka.ms/new-console-template for more information
using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Razor;
using FluentEmail.SendGrid;
using Microsoft.Extensions.Configuration;
using SendGridFluentEmailSender;

IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();
string sendGridAPIKey = configuration["SendGridApiKey"];

Email.DefaultRenderer = new RazorRenderer();

IFluentEmail fluentEmail = Email
    .From("<email sender>")
    .To("<email destination>")
    .Subject("Test Email")
    .Tag("test")
    .UsingTemplateFromFile($"TestEmail.cshtml", new TestEmailModel { Name = "Nestor Campos" });
//.Body("This is a test email using Twilio SendGrid and FluentEmail");

SendGridSender sendGridSender = new SendGridSender(sendGridAPIKey);
SendResponse response = sendGridSender.Send(fluentEmail);

if (response.Successful)
{
    Console.WriteLine("The email was sent successfully");
}
else
{
    Console.WriteLine("The email could not be sent. Check the errors: ");
    foreach (string error in response.ErrorMessages)
    {
        Console.WriteLine(error);
    }
}