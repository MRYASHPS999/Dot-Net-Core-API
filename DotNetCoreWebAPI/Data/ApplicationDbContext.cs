using DotNetCoreWebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public DbSet<Employee> employee { get; set; }

        public DbSet<Manager> manager { get; set; }

        public DbSet<UserDetails> users { get; set; }


        //Fluent API Setup
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Prevent multiple cascade paths

            //modelBuilder.Entity<Employee>()
            //    .HasOne(q => q.RFQ)
            //    .WithMany(r => r.RFQQuotations)
            //    .HasForeignKey(q => q.RFQId)
            //    .OnDelete(DeleteBehavior.Restrict); //no cascade delete

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(ent =>
                {
                    ent.HasOne(x => x.Manager)
                    .WithMany(x => x.emps)
                    .HasForeignKey(x => x.Mid)
                    .OnDelete(DeleteBehavior.Restrict);
                });

        }


        }

}
