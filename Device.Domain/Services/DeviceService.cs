using Device.Domain.Interfaces.Repositories;
using Device.Domain.Interfaces.Services;
using Device.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Device.Domain.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly ILogger<DeviceService> _logger;

        public DeviceService(IDeviceRepository deviceRepository, ILogger<DeviceService> logger)
        {
            _deviceRepository = deviceRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Entities.Device>> GetAllDevices()
        {
            _logger.LogInformation("Getting all devices");
            var devices = await _deviceRepository.GetAll();

            return devices;
        }

        public async Task<Entities.Device> GetDeviceById(Guid id)
        {
            _logger.LogInformation("Getting device by id: {id}", id);
            var device = await _deviceRepository.GetDevice(d => d.Id == id);

            return device;
        }

        public async Task<Entities.Device> GetDeviceByBrand(string brand)
        {
            _logger.LogInformation("Getting device by brand: {brand}", brand);
            var device = await _deviceRepository.GetDevice(d => d.Brand.StartsWith(brand));

            return device;
        }

        public async Task<bool> DeleteDevice(Guid id)
        {
            _logger.LogInformation("Removing device: {id}", id);
            var removed = await _deviceRepository.Delete(d => d.Id == id);

            return removed == 1;
        }

        public async Task<Entities.Device> PatchDevice(Guid id, DeviceRequest request)
        {
            var device = await GetDeviceById(id);

            if (device == null)
            {
                return null;
            }

            device.Brand = request.Brand;
            device.Name = request.Name;

            return await _deviceRepository.Update<Entities.Device>(device);
        }

        public async Task<Entities.Device> UpdateDevice(Guid id, DeviceRequest request)
        {
            var device = await GetDeviceById(id);

            device ??= new();
            device.Brand = request.Brand;
            device.Name = request.Name;

            return await _deviceRepository.Update<Entities.Device>(device);
        }

        public async Task<Entities.Device> AddDevice(DeviceRequest deviceRequest)
        {
            _logger.LogInformation("Adding device: {deviceRequest.Name}, {deviceRequest.Brand}", deviceRequest.Name, deviceRequest.Brand);
            var device = await _deviceRepository.Add<Entities.Device>(new Entities.Device()
            {
                Brand = deviceRequest.Brand,
                Name = deviceRequest.Name
            });

            return device;
        }
    }
}
