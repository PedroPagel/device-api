using Device.Domain.Interfaces;
using Device.Domain.Interfaces.Repositories;
using Device.Domain.Interfaces.Services;
using Device.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Device.Domain.Services
{
    public class DeviceService : BaseService, IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly ILogger<DeviceService> _logger;

        public DeviceService(INotificator notificator, IDeviceRepository deviceRepository, ILogger<DeviceService> logger) : base(notificator) 
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

            if (device == null)
            {
                NotifyError($"Device not found with the id: {id}");
                return null;
            }

            return device;
        }

        public async Task<Entities.Device> GetDeviceByBrand(string brand)
        {
            _logger.LogInformation("Getting device by brand: {brand}", brand);
            var device = await _deviceRepository.GetDevice(d => d.Brand.StartsWith(brand));

            if (device == null)
            {
                NotifyError($"Device not found with the brand: {brand}");
                return null;
            }

            return device;
        }

        public async Task<bool> DeleteDevice(Guid id)
        {
            _logger.LogInformation("Removing device: {id}", id);
            var device = await GetDeviceById(id);

            if (device == null)
            {
                NotifyError($"Device not found with the id: {id}");
                return false;
            }

            var removed = await _deviceRepository.Delete(d => d.Id == id);

            return removed == 1;
        }

        public async Task<Entities.Device> PatchDevice(Guid id, DeviceRequest request)
        {
            _logger.LogInformation("Patch device: {deviceRequest.Name}, {deviceRequest.Brand}", request.Name, request.Brand);
            var device = await GetDeviceById(id);

            if (device == null)
            {
                NotifyError($"Device not found with the id: {id}");
                return null;
            }

            return await UpdateDevice(device, request);
        }

        public async Task<Entities.Device> PutDevice(Guid id, DeviceRequest request)
        {
            _logger.LogInformation("Updating device: {deviceRequest.Name}, {deviceRequest.Brand}", request.Name, request.Brand);
            var device = await GetDeviceById(id);
            device ??= new();

            return await UpdateDevice(device, request);
        }

        public async Task<Entities.Device> AddDevice(DeviceRequest request)
        {
            _logger.LogInformation("Adding device: {deviceRequest.Name}, {deviceRequest.Brand}", request.Name, request.Brand);
            var device = await _deviceRepository.Add<Entities.Device>(new Entities.Device()
            {
                Brand = request.Brand,
                Name = request.Name
            });

            return device;
        }

        private async Task<Entities.Device> UpdateDevice(Entities.Device device, DeviceRequest request)
        {
            device.Brand = request.Brand;
            device.Name = request.Name;

            return await _deviceRepository.Update<Entities.Device>(device);
        }
    }
}
