using System;
using System.Collections.Generic;
using System.Text;
using EmailAlert.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EmailAlert.Data
{
    public class EmailDbAccess : IEmailDbAccess
    {
        public readonly EmailAlertDbContext db;

        public EmailDbAccess(EmailAlertDbContext db)
        {
            this.db = db;
        }

        public Email Add(Email newEmail)
        {
            db.Add(newEmail);
            return newEmail;
        }

        //............................................

        public Email Delete(int id)
        {
            var newEmail = GetById(id);
            if (newEmail != null)
            {
                db.Emails.Remove(newEmail);
            }
            return newEmail;
        }

        public Email GetById(int id)
        {
            return db.Emails.Find(id);
        }

        //............................................

        public IEnumerable<Email> GetEmailByName(string name)
        {
            var query = from s in db.Emails
                        where s.Subject.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby s.Subject
                        select s;
            return query;
        }


        public List<Email> ReturnAll()
        {
            return db.Emails.ToList();
        }


        public Email Update(Email updateEmail)
        {
            var entity = db.Emails.Attach(updateEmail);
            entity.State = EntityState.Modified;
            return updateEmail;
        }


        public void OtherUpdate()
        {
            var mail = db.Emails.FirstOrDefault();
            mail.Subject += "name";
        }


        public void UpdateMultipleEntities(string toUpdate)
        {
            var mails = db.Emails.ToList();
            mails.ForEach(s => s.Subject += toUpdate);
        }


        public int Commit()
        {
            return db.SaveChanges();
        }
    }
}
