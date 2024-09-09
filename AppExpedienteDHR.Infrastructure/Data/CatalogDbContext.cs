using AppExpedienteDHR.Core.Domain.Entities.General;
using Microsoft.EntityFrameworkCore;


namespace AppExpedienteDHR.Infrastructure.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        public DbSet<Padron> Padron { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuraciones adicionales si es necesario
        }


    }
}
