using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailAlert.Data;
using EmailAlert.Data.Interfaces;
using EmailAlert.Domain;
using Microsoft.AspNetCore.Mvc;
using EmailAlert.Business;

namespace EmailAlert.App.Controllers
{
    [Route("api/UIcontroller")]
    public class UIcontroller : Controller
    {
        private IEmailDbAccess _email;
        private IRegisterDbAccess _registration;
        private IAdminDbAccess _admin;
        private IStockService _stocks;

        private EmailAPI EmailAPI { get; }

        public UIcontroller(
            IEmailDbAccess Email, 
            IRegisterDbAccess Registration, 
            IAdminDbAccess Administrator,
            IStockService Stocks, EmailAPI emailAPI)
        {
            _email = Email;
            _registration = Registration;
            _admin = Administrator;
            _stocks = Stocks;
            EmailAPI = emailAPI;
        }

        
        [HttpPost("DeleteUser")]
        public List<RegisteredUser> DeleteUser([FromBody] int id)
        {
            _registration.Delete(id);
            _registration.Commit();
            return _registration.ReturnAll();
        }

        [HttpPost("UpdateStock")]
        public async Task<List<Stock>> PostStocks([FromBody] StockUrl urlParts)
        {
            var fullUrl = ParseUrl.Parse(urlParts);
            return await _stocks.CallStockApi(fullUrl, urlParts); 
        }

        [HttpGet("Users")]
        public List<RegisteredUser> GetUsers()
        {
            return _registration.ReturnAll();
        }

        [HttpGet("Emails")]
        public List<Email> UploadEmails()
        {
            return _email.ReturnAll();
        }

        [HttpPost("DeleteEmail")]
        public List<Email> DeleteEmail([FromBody] int id)
        {
            _email.Delete(id);
            _email.Commit();
            return _email.ReturnAll();
        }

        [HttpPost("Authorize")]
        public bool CheckAuth([FromBody] Administrator admin)
        {
            //probably just wanna do these search ops in the service

            var password = admin.Password;
            var email = admin.Email;
            var list = _admin.ReturnAll();

            var administrator1 = list.FirstOrDefault(i => i.Email == email);
            var administrator2 = list.FirstOrDefault(i => i.Password == password);


            if (administrator1 != null && administrator2 != null)
            {
                if (administrator1.Email == administrator2.Email)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        [HttpPost("CreateEmail")]
        public List<Email> PostEmail([FromBody] Email value)
        {
            _email.Add(value);
            _email.Commit();
            return _email.ReturnAll();
        }

        [HttpPost("SendEmail")]
        public async Task<string> SendEmail([FromBody] int id)
        {
            var email = _email.GetById(id);
            email.Admin = true;
            var response = await EmailAPI.Send(email, null);

            if (response == "sent")
            {
                return "Your email has been sent.";
            }
            else
            {
                return "Something went wrong.";
            }
        }

        [HttpPost("RegisterUser")]
        public List<RegisteredUser> PostRegistration([FromBody] RegisteredUser newUser)
        {
            _registration.Add(newUser);
            _registration.Commit();
            return _registration.ReturnAll();
        }
    }
}

