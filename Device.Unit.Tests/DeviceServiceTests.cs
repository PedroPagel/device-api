using Device.Domain.Interfaces;
using Device.Domain.Interfaces.Repositories;
using Device.Domain.Models;
using Device.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Linq.Expressions;

namespace Device.Unit.Tests
{
    [TestFixture]
    public class DeviceServiceTests
    {
        private DeviceService _deviceService;
        private Mock<IDeviceRepository> _deviceRepositoryMock;
        private Mock<ILogger<DeviceService>> _loggerMock;
        private Mock<INotificator> _notificatorMock;

        [SetUp]
        public void SetUp()
        {
            _deviceRepositoryMock = new Mock<IDeviceRepository>();
            _loggerMock = new Mock<ILogger<DeviceService>>();
            _notificatorMock = new Mock<INotificator>();
            _deviceService = new DeviceService(_notificatorMock.Object, _deviceRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task GetAllDevices_ReturnsAllDevices()
        {
            // Arrange
            var devices = new List<Domain.Entities.Device>
        {
            new Domain.Entities.Device { Id = Guid.NewGuid(), Name = "Device1", Brand = "Brand1" },
            new Domain.Entities.Device { Id = Guid.NewGuid(), Name = "Device2", Brand = "Brand2" }
        };

            _deviceRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(devices);

            // Act
            var result = await _deviceService.GetAllDevices();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(devices));
        }

        [Test]
        public async Task GetDeviceById_ReturnsDevice_IfExists()
        {
            // Arrange
            var deviceId = Guid.NewGuid();
            var device = new Domain.Entities.Device { Id = deviceId, Name = "TestDevice", Brand = "TestBrand" };

            _deviceRepositoryMock.Setup(repo => repo.GetDevice(It.IsAny<Expression<Func<Domain.Entities.Device, bool>>>()))
                                 .ReturnsAsync(device);

            // Act
            var result = await _deviceService.GetDeviceById(deviceId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(device));
        }

        [Test]
        public async Task GetDeviceById_ReturnsNull_IfNotExists()
        {
            // Arrange
            var deviceId = Guid.NewGuid();
            _deviceRepositoryMock.Setup(repo => repo.GetDevice(It.IsAny<Expression<Func<Domain.Entities.Device, bool>>>()))
                                 .ReturnsAsync((Device.Domain.Entities.Device)null);

            // Act
            var result = await _deviceService.GetDeviceById(deviceId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetDeviceByBrand_ReturnsDevice_IfExists()
        {
            // Arrange
            var brand = "TestBrand";
            var device = new Domain.Entities.Device { Id = Guid.NewGuid(), Name = "TestDevice", Brand = brand };
            _deviceRepositoryMock.Setup(repo => repo.GetDevice(It.IsAny<Expression<Func<Domain.Entities.Device, bool>>>()))
                                 .ReturnsAsync(device);

            // Act
            var result = await _deviceService.GetDeviceByBrand(brand);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(device));
        }

        [Test]
        public async Task GetDeviceByBrand_ReturnsNull_IfNotExists()
        {
            // Arrange
            var brand = "NonExistentBrand";
            _deviceRepositoryMock.Setup(repo => repo.GetDevice(It.IsAny<Expression<Func<Domain.Entities.Device, bool>>>()))
                                 .ReturnsAsync((Domain.Entities.Device)null);

            // Act
            var result = await _deviceService.GetDeviceByBrand(brand);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task DeleteDevice_ReturnsTrue_IfDeviceExistsAndDeleted()
        {
            // Arrange
            var device = new Domain.Entities.Device { Id = Guid.NewGuid(), Name = "OldName", Brand = "OldBrand" };

            _deviceRepositoryMock.Setup(repo => repo.GetDevice(It.IsAny<Expression<Func<Domain.Entities.Device, bool>>>()))
                                 .ReturnsAsync(device);

            _deviceRepositoryMock.Setup(repo => repo.Delete(It.IsAny<Expression<Func<Domain.Entities.Device, bool>>>()))
                                 .ReturnsAsync(1);

            // Act
            var result = await _deviceService.DeleteDevice(device.Id);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task DeleteDevice_ReturnsFalse_IfDeviceNotExists()
        {
            // Arrange
            var deviceId = Guid.NewGuid();
            _deviceRepositoryMock.Setup(repo => repo.Delete(It.IsAny<Expression<Func<Domain.Entities.Device, bool>>>()))
                                 .ReturnsAsync(0);

            // Act
            var result = await _deviceService.DeleteDevice(deviceId);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task PatchDevice_ReturnsUpdatedDevice_IfDeviceExists()
        {
            // Arrange
            var deviceId = Guid.NewGuid();
            var deviceRequest = new DeviceRequest { Name = "UpdatedName", Brand = "UpdatedBrand" };
            var device = new Domain.Entities.Device { Id = deviceId, Name = "OldName", Brand = "OldBrand" };

            _deviceRepositoryMock.Setup(repo => repo.GetDevice(It.IsAny<Expression<Func<Domain.Entities.Device, bool>>>()))
                                 .ReturnsAsync(device);
            _deviceRepositoryMock.Setup(repo => repo.Update<Domain.Entities.Device>(device))
                                 .ReturnsAsync(device);

            // Act
            var result = await _deviceService.PatchDevice(deviceId, deviceRequest);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(deviceRequest.Name));
            Assert.That(result.Brand, Is.EqualTo(deviceRequest.Brand));
        }

        [Test]
        public async Task PatchDevice_ReturnsNull_IfDeviceNotExists()
        {
            // Arrange
            var deviceId = Guid.NewGuid();
            var deviceRequest = new DeviceRequest { Name = "UpdatedName", Brand = "UpdatedBrand" };
            _deviceRepositoryMock.Setup(repo => repo.GetDevice(It.IsAny<Expression<Func<Domain.Entities.Device, bool>>>()))
                                 .ReturnsAsync((Domain.Entities.Device)null);

            // Act
            var result = await _deviceService.PatchDevice(deviceId, deviceRequest);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task PutDevice_ReturnsUpdatedDevice_IfDeviceExists()
        {
            // Arrange
            var deviceId = Guid.NewGuid();
            var deviceRequest = new DeviceRequest { Name = "UpdatedName", Brand = "UpdatedBrand" };
            var device = new Domain.Entities.Device { Id = deviceId, Name = "OldName", Brand = "OldBrand" };

            _deviceRepositoryMock.Setup(repo => repo.GetDevice(It.IsAny<Expression<Func<Domain.Entities.Device, bool>>>()))
                                 .ReturnsAsync(device);

            _deviceRepositoryMock.Setup(repo => repo.Update<Domain.Entities.Device>(device))
                                 .ReturnsAsync(device);

            // Act
            var result = await _deviceService.PutDevice(deviceId, deviceRequest);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(deviceRequest.Name));
            Assert.That(result.Brand, Is.EqualTo(deviceRequest.Brand));
        }

        [Test]
        public async Task PutDevice_ReturnsNewDevice_IfDeviceNotExists()
        {
            // Arrange
            var deviceId = Guid.NewGuid();
            var deviceRequest = new DeviceRequest { Name = "NewDevice", Brand = "NewBrand" };

            _deviceRepositoryMock.Setup(repo => repo.GetDevice(It.IsAny<Expression<Func<Domain.Entities.Device, bool>>>()))
                                 .ReturnsAsync((Domain.Entities.Device)null);
            
            _deviceRepositoryMock.Setup(repo => repo.Update<Domain.Entities.Device>(It.IsAny<Domain.Entities.Device>()))
                                 .ReturnsAsync(new Domain.Entities.Device { Id = deviceId, Name = deviceRequest.Name, Brand = deviceRequest.Brand });

            // Act
            var result = await _deviceService.PutDevice(deviceId, deviceRequest);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(deviceRequest.Name));
            Assert.That(result.Brand, Is.EqualTo(deviceRequest.Brand));
        }

        [Test]
        public async Task AddDevice_ReturnsNewDevice()
        {
            // Arrange
            var deviceRequest = new DeviceRequest { Name = "NewDevice", Brand = "NewBrand" };

            _deviceRepositoryMock.Setup(repo => repo.Add<Domain.Entities.Device>(It.IsAny<Domain.Entities.Device>()))
                                 .ReturnsAsync(new Domain.Entities.Device { Id = Guid.NewGuid(), Name = deviceRequest.Name, Brand = deviceRequest.Brand });

            // Act
            var result = await _deviceService.AddDevice(deviceRequest);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(deviceRequest.Name));
            Assert.That(result.Brand, Is.EqualTo(deviceRequest.Brand));
        }
    }
}