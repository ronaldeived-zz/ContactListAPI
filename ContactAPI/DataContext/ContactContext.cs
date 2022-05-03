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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry =>
            entry.Entity.GetType().GetProperty("DateCreated") != null ||
            entry.Entity.GetType().GetProperty("DateLastUpdate") != null
            ))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DateCreated").CurrentValue = DateTime.Now;
                    entry.Property("DateLastUpdate").CurrentValue = DateTime.Now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DateLastUpdate").CurrentValue = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync();
        }

        public DbSet<Person> Person { get; set; }

        public DbSet<Contact> Contact { get; set; }
    }
}
