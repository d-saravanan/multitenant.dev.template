using MultiTenantRepository.EntityFramework;
using System;
using System.Data.Entity;

namespace GenericRepository.EntityFramework.Test.Infrastrucure
{
    public interface IPeopleContext : IDisposable, IEntitiesContext
    {
        IDbSet<Person> People { get; set; }
        IDbSet<Book> Books { get; set; }
    }
}