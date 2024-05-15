using System;
using System.Linq;
using RestWithASPNET.Model;
using RestWithASPNET.Model.Context;

namespace RestWithASPNET.Repository.Generic
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySqlContext context) : base(context)
        {}

        public Person Disable(long id)
        {
            var user = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            if(user == null) return null;

            user.Enabled = false;

            try {
                _context.Entry(user).CurrentValues.SetValues(user);
                _context.SaveChanges();
                return user;
            } catch (Exception) {
                throw;
            }
        }

    }
}

