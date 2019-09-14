using EmailAlert.Data.Interfaces;
using EmailAlert.Domain;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EmailAlert.Data.DbService
{
    public class AdminDbAccess : IAdminDbAccess
    {
        public readonly EmailAlertDbContext db;

        public AdminDbAccess(EmailAlertDbContext db)
        {
            this.db = db;
        }

        public Administrator Add(Administrator newAdmin)
        {
            db.Add(newAdmin);
            return newAdmin;
        }

        //............................................

        public Administrator Delete(int id)
        {
            var newAdmin = GetById(id);
            if (newAdmin != null)
            {
                db.Administrators.Remove(newAdmin);
            }
            return newAdmin;
        }

        public Administrator GetById(int id)
        {
            return db.Administrators.Find(id);
        }

        //............................................

        public IEnumerable<Administrator> GetAdministratorByName(string name)
        {
            var query = from s in db.Administrators
                        where s.Email.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby s.Email
                        select s;
            return query;
        }


        public List<Administrator> ReturnAll()
        {
            return db.Administrators.ToList();
        }


        //??????????????????
        public Administrator Update(Administrator updateAdmin)
        {
            var entity = db.Administrators.Attach(updateAdmin);
            entity.State = EntityState.Modified;
            return updateAdmin;
        }


        public void OtherUpdate()
        {
            var sam = db.Administrators.FirstOrDefault();
            sam.Email += "name";
        }


        public void UpdateMultipleEntities(string toUpdate)
        {
            var sams = db.Administrators.ToList();
            sams.ForEach(s => s.Email += toUpdate);
        }


        //nothing will change in the database until someone calls commit
        public int Commit()
        {
            //the returned int is the number of rows affected in the database
            return db.SaveChanges();
        }
    }
}
