using EmailAlert.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailAlert.Data.Interfaces
{
    public interface IAdminDbAccess
    {
        Administrator Add(Administrator newAdmin);

        Administrator Delete(int id);

        Administrator GetById(int id);

        IEnumerable<Administrator> GetAdministratorByName(string name);

        List<Administrator> ReturnAll();

        Administrator Update(Administrator updateAdmin);

        void UpdateMultipleEntities(string toUpdate);

        int Commit();
    }
}
