using Microsoft.EntityFrameworkCore;
using PocChangeTracker.Entities;

namespace PocChangeTracker.Context
{
    public class AplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; } = null!;

        public AplicationContext(DbContextOptions<AplicationContext> options) : base(options)
        {

        }
    }
}
