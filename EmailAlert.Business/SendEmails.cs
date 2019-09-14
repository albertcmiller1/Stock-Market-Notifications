using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using EmailAlert.Domain;
using EmailAlert.Data;

namespace EmailAlert.Business
{
    public class EmailAPI
    {
        public IRegisterDbAccess _registration;

        public EmailAPI(IRegisterDbAccess Registration)
        {
            _registration = Registration;
        }


        public async Task<string> Send(Email currentEmail, string ticker)
        {
            var AllUsers = _registration.ReturnAll();

            var client = new SendGridClient("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");
            var from = new EmailAddress(currentEmail.From);
            var subject = currentEmail.Subject;
            var plainTextContent = currentEmail.Content;
            var htmlContent = "<p>" + currentEmail.Content + "</p>";

            if (currentEmail.Admin)
            {
                foreach (var recipiant in AllUsers)
                {
                    if (recipiant.Admin)
                    {
                        var to = new EmailAddress(recipiant.Email);
                        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                        var response = await client.SendEmailAsync(msg);
                    }
                }
                return "sent";
            }

            else if (currentEmail.UpFive)
            {
                foreach (var recipiant in AllUsers)
                {
                    if (recipiant.UpFive)
                    {
                        foreach (var Ticker in recipiant.Stocks)
                        {
                            if (Ticker.stock == ticker)
                            {
                                var to = new EmailAddress(recipiant.Email);
                                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                                var response = await client.SendEmailAsync(msg);
                            }
                        }
                    }
                }
                return "sent";
            }

            else if (currentEmail.DownFive)
            {
                foreach (var recipiant in AllUsers)
                {
                    if (recipiant.DownFive)
                    {
                        foreach (var Ticker in recipiant.Stocks)
                        {
                            if (Ticker.stock == ticker)
                            {
                                var to = new EmailAddress(recipiant.Email);
                                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                                var response = await client.SendEmailAsync(msg);
                            }
                        }
                    }
                }
                return "sent";
            }

            else if (currentEmail.MovingAvg)
            {
                foreach (var recipiant in AllUsers)
                {
                    if (recipiant.MovingAvg)
                    {
                        foreach (var Ticker in recipiant.Stocks)
                        {
                            if (Ticker.stock == ticker)
                            {
                                var to = new EmailAddress(recipiant.Email);
                                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                                var response = await client.SendEmailAsync(msg);
                            }
                        }
                    }
                }
                return "sent";
            }
            else
            {
                return "error";
            }
        }
    }
}
////possibly a better way to send
//foreach (var recipiant in currentEmail.Recipiants)
//{
//    if (recipiant.Admin && currentEmail.Admin)
//    {
//        var to = new EmailAddress(recipiant.Email);
//        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
//        var response = await client.SendEmailAsync(msg);
//    }
//}