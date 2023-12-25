using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace SemanticKernel.Plugins;

public class EmailPlugin
{
    [KernelFunction]
    [Description("Sends an email to a recipient.")]
    public async Task SendEmailAsync(Kernel kernel,
        [Description("Semicolon delimitated list of emails of the recipients")] string recipientEmails,
        string subject,
        string body
    )
    {
        Console.WriteLine("Email sent!");
    }
}