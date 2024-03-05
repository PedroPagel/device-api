using Device.Domain.Models;

namespace Device.Domain.Interfaces.Services
{
    public interface IDeviceService
    {
        Task<IEnumerable<Entities.Device>> GetAllDevices();
        Task<Entities.Device> GetDeviceById(Guid id);
        Task<Entities.Device> GetDeviceByBrand(string brand);
        Task<Entities.Device> AddDevice(DeviceRequest deviceRequest);
        Task<Entities.Device> PutDevice(Guid id, DeviceRequest request);
        Task<Entities.Device> PatchDevice(Guid id, DeviceRequest request);
        Task<bool> DeleteDevice(Guid id);
    }
}
