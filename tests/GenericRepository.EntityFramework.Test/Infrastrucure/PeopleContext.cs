using MultiTenantRepository.EntityFramework;
using System.Data.Entity;

namespace GenericRepository.EntityFramework.Test.Infrastrucure
{
    public class PeopleContext : EntitiesContext, IPeopleContext
    {
        public PeopleContext() { }

        public IDbSet<Person> People { get; set; }
        public IDbSet<Book> Books { get; set; }
    }
}