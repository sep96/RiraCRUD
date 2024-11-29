using RiraCRUD.Domain.Interface;
using RiraCRUD.Infrastructure.Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiraCRUD.Infrastructure.Persistence.Contexts
{
    public class AppDbContext : DbContext , IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }

        public async Task SaveChangesAsync(CancellationToken cancellation)
        {
            await base.SaveChangesAsync(cancellation);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configuration
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
        }
    }
}
