using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace CinelAirMilesMySQL.DataBase
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ticket> Tickets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
