using EmailAlert.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EmailAlert.Data
{
    public class RegisterDbAccess : IRegisterDbAccess
    {
        public readonly EmailAlertDbContext db;

        public RegisterDbAccess(EmailAlertDbContext db)
        {
            this.db = db;
        }

        public RegisteredUser Add(RegisteredUser newUser)
        {
            db.Add(newUser);
            //this might be wrong.. need to update the tickers. 
            return newUser;
        }

        //............................................

        public RegisteredUser Delete(int id)
        {
            var newUser = GetById(id);
            if (newUser != null)
            {
                //this is called cascade deleting..
                //it deletes the child of the parent.. aka the List<Ticker> 
                db.Entry(newUser).Collection(u => u.Stocks).Load();
                db.RegisteredUsers.Remove(newUser);
            }
            return newUser;
        }
        

        public RegisteredUser Load(int id)
        {
            var user = GetById(id);
            if (user != null)
            {
                //this is called cascade deleting..
                //it deletes the child of the parent.. aka the List<Ticker> 
                db.Entry(user).Collection(u => u.Stocks).Load();
            }
            return user;
        }


        public RegisteredUser GetById(int id)
        {
            return db.RegisteredUsers.Find(id);
        }

        //............................................

        public IEnumerable<RegisteredUser> GetUserByName(string name)
        {
            var query = from s in db.RegisteredUsers
                        where s.FirstName.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby s.FirstName
                        select s;
            return query;
        }

        public List<RegisteredUser> ReturnAll()
        {
            var everyone = db.RegisteredUsers.ToList();
            foreach(var usr in everyone)
            {
                db.Entry(usr).Collection(u => u.Stocks).Load();
            }
            return everyone;
        }

        public RegisteredUser Update(RegisteredUser updateUser)
        {
            var entity = db.RegisteredUsers.Attach(updateUser);
            entity.State = EntityState.Modified;
            return updateUser;
        }

        public void OtherUpdate()
        {
            var mail = db.RegisteredUsers.FirstOrDefault();
            mail.FirstName += "name";
        }

        public void UpdateMultipleEntities(string toUpdate)
        {
            var mails = db.RegisteredUsers.ToList();
            mails.ForEach(s => s.FirstName += toUpdate);
        }


        public int Commit()
        {
            return db.SaveChanges();
        }
    }
}

