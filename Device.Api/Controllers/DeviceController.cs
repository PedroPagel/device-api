using AutoMapper;
using Device.Domain.Interfaces;
using Device.Domain.Interfaces.Services;
using Device.Domain.Models;
using Device.WebApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Device.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [AllowAnonymous]
    [Route("api/v1/device")]
    public class DeviceController : BaseController
    {
        private readonly IDeviceService _deviceService;
        private readonly IMapper _mapper;

        public DeviceController(INotificator notificator, IMapper mapper, IDeviceService deviceService) : base(notificator) 
        {
            _mapper = mapper;
            _deviceService = deviceService;
        }


        /// <summary>
        /// Get all devices
        /// </summary>
        /// <returns>Devices</returns>
        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetAllDevices()
        {
            var result = _mapper.Map<IEnumerable<DeviceDto>>(await _deviceService.GetAllDevices());

            return CustomResponse(result); 
        }

        /// <summary>
        /// Get a device by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Device</returns>
        [HttpGet("get-by-id/{id:guid}")]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetDeviceById(Guid id)
        {
            var result = _mapper.Map<DeviceDto>(await _deviceService.GetDeviceById(id));

            return CustomResponse(result);
        }

        /// <summary>
        /// Get a device by brand
        /// </summary>
        /// <param name="brand">Brand</param>
        /// <returns>Device</returns>
        [HttpGet("get-by-brand/{brand}")]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetDeviceByBrand(string brand)
        {
            var result = _mapper.Map<DeviceDto>(await _deviceService.GetDeviceByBrand(brand));

            return CustomResponse(result);
        }

        /// <summary>
        /// Delete a device
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>True if deleted</returns>
        [HttpDelete("delete-by-id/{id:guid}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var result = await _deviceService.DeleteDevice(id);

            return CustomResponse(result);
        }

        /// <summary>
        /// Post a device
        /// </summary>
        /// <param name="request">Model</param>
        /// <returns>Device added</returns>
        [HttpPost]
        public async Task<ActionResult<DeviceDto>> AddDevice(DeviceRequest request)
        {
            var result = _mapper.Map<DeviceDto>(await _deviceService.AddDevice(request));

            return CustomResponse(result);
        }

        /// <summary>
        /// Patch device
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="request">Model</param>
        /// <returns>Device</returns>
        [HttpPatch("id")]
        public async Task<ActionResult<DeviceDto>> PatchDevice(Guid id, [FromBody] DeviceRequest request)
        {
            var result = _mapper.Map<DeviceDto>(await _deviceService.PatchDevice(id, request));

            return CustomResponse(result);
        }

        /// <summary>
        /// Put device
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="request">Model</param>
        /// <returns>Device</returns>
        [HttpPut("id")]
        public async Task<ActionResult<DeviceDto>> PutDevice(Guid id, [FromBody] DeviceRequest request)
        {
            var result = _mapper.Map<DeviceDto>(await _deviceService.PutDevice(id, request));

            return CustomResponse(result);
        }
    }
}
