using Microsoft.EntityFrameworkCore;

namespace Device.Infrastructure.DbContexts
{
    public class DeviceContext : DbContext
    {
        public DbSet<Domain.Entities.Device> Devices { get; set; }

        public DeviceContext(DbContextOptions<DeviceContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeviceContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.State == EntityState.Added))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("CreatedDate").CurrentValue = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
