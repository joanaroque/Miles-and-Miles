using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using MilesBackOffice.Web.Data.Entities;
using System.Collections;
using System.Linq;


namespace MilesBackOffice.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {

        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }



        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasOne("MilesBackOffice.Web.Data.Entities.User", "CreatedBy")
                .WithOne()
                .HasForeignKey("User", "CreatedById");

                entity.HasOne("MilesBackOffice.Web.Data.Entities.User", "ModifiedBy")
                .WithOne()
                .HasForeignKey("User", "ModifiedById");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasOne("MilesBackOffice.Web.Data.Entities.User", "CreatedBy")
                .WithOne()
                .HasForeignKey("User", "CreatedById"); ;

                entity.HasOne("MilesBackOffice.Web.Data.Entities.User", "ModifiedBy")
                .WithOne()
                .HasForeignKey("User", "ModifiedById"); ;
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne("MilesBackOffice.Web.Data.Entities.City", "City")
                .WithMany();

                entity.HasOne("MilesBackOffice.Web.Data.Entities.Country", "Country")
                .WithMany();
            });




            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys()
                .Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade));

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);



        }

    }
}
