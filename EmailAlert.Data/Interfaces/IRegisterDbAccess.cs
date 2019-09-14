using EmailAlert.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailAlert.Data
{
    public interface IRegisterDbAccess
    {
        RegisteredUser Add(RegisteredUser newUser);

        RegisteredUser Delete(int id);

        RegisteredUser GetById(int id);

        IEnumerable<RegisteredUser> GetUserByName(string name);

        List<RegisteredUser> ReturnAll();

        RegisteredUser Update(RegisteredUser updateUser);

        void UpdateMultipleEntities(string toUpdate);

        int Commit();
    }
}

