using System.Linq.Expressions;

namespace Device.Domain.Interfaces.Repositories
{
    public interface IDeviceRepository : IRepository<Entities.Device>
    {
        Task<Entities.Device> GetDevice(Expression<Func<Entities.Device, bool>> predicate);
    }
}
