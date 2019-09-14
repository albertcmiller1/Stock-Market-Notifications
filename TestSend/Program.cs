using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;


namespace TestSend
{
    internal class Example
    {
        private static void Main()
        {
            Execute().Wait();
            Console.WriteLine("sent");
        }

        static async Task Execute()
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("albertcmiller1@gmail.com", "al miller");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("albertcmiller1@gmail.com", "albert miller");
            var plainTextContent = "im some plain text content";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(msg.Serialize());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers);
            Console.WriteLine("\n\nPress <Enter> to continue.");
            Console.ReadLine();
        }
    }
}


//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
//email api key