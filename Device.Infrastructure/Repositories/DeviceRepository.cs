using Device.Domain.Interfaces.Repositories;
using Device.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Device.Infrastructure.Repositories
{
    public class DeviceRepository : Repository<Domain.Entities.Device>, IDeviceRepository
    {
        public DeviceRepository(DeviceContext db) : base(db)
        {
        }

        public async Task<Domain.Entities.Device> GetDevice(Expression<Func<Domain.Entities.Device, bool>> predicate)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }
    }
}
