using System;
using System.Collections.Generic;
using System.Linq;
using RestWithASPNET.Model;
using RestWithASPNET.Model.Context;
using RestWithASPNET.Repository.Generic;

namespace RestWithASPNET.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(MySqlContext context)
            : base(context) { }

        public Person Disable(long id)
        {
            var user = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            if (user == null)
                return null;

            user.Enabled = false;

            try
            {
                _context.Entry(user).CurrentValues.SetValues(user);
                _context.SaveChanges();
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Person> FindByName(string firstName, string lastName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return _context
                    .Persons.Where(p =>
                        p.FirstName.Contains(firstName) && p.LastName.Contains(lastName)
                    )
                    .ToList();
            }
            else if (string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(p => p.LastName.Contains(lastName)).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
            {
                return _context.Persons.Where(p => p.FirstName.Contains(firstName)).ToList();
            }

            // if (!string.IsNullOrWhiteSpace(firstName) || !string.IsNullOrWhiteSpace(lastName))
            // {
            //     return _context
            //         .Persons.Where(p =>
            //             p.FirstName.Contains(firstName) || p.LastName.Contains(lastName)
            //         )
            //         .ToList();
            // }

            return null;
        }
    }
}
