using RestWithASPNET.Model;

namespace RestWithASPNET.Repository.Generic
{
    public interface IPersonRepository : IRepository<Person>
    {
        Person Disable(long id);
    }
}

