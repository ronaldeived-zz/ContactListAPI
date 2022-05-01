#nullable disable
using ContactAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactAPI.Data
{
    public class ContactContext : DbContext
    {
        public ContactContext (DbContextOptions<ContactContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }

        public DbSet<Contact> Contact { get; set; }
    }
}
