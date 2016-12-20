using MultiTenantRepository;
using System;

namespace GenericRepository.EntityFramework.Test.Infrastrucure
{
    /// <summary>
    /// The Book Entity
    /// </summary>
    /// <seealso cref="MultiTenantRepository.IMultiTenantEntity" />
    public class Book : IMultiTenantEntity
    {
        public int Id { get; set; }
        public Guid TenantId { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public DateTime PublishedOn { get; set; }
        public Person Person { get; set; }
    }
}