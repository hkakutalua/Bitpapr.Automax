using Bitpapr.Automax.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpapr.Automax.Infrastructure
{
    public class AutomaxContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        public AutomaxContext()
            : base("Automax")
        {
            Database.SetInitializer(new AutomaxDbInitializer());
            //Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Invoice>()
                .HasKey(i => i.Number)
                .HasMany(i => i.ServicesToProvide)
                .WithRequired()
                .HasForeignKey(s => s.InvoiceId);

            modelBuilder.Entity<ServiceToProvide>()
                .HasKey(s => new { s.InvoiceId, s.ItemNumber });
        }
    }
}
