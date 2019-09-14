using EmailAlert.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailAlert.Data
{
    public interface IEmailDbAccess
    {
        Email Add(Email newEmail);

        Email Delete(int id);

        Email GetById(int id);

        IEnumerable<Email> GetEmailByName(string name);

        List<Email> ReturnAll();

        Email Update(Email updateEmail);

        void UpdateMultipleEntities(string toUpdate);

        int Commit();
    }
}
