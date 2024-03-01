using AutoMapper;
using Device.Domain.Interfaces.Services;
using Device.Domain.Models;
using Device.WebApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Device.WebApi.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/v1/device")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private readonly IMapper _mapper;

        public DeviceController(IMapper mapper, IDeviceService deviceService)
        {
            _mapper = mapper;
            _deviceService = deviceService;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetAllDevices()
        {
            var result = _mapper.Map<IEnumerable<DeviceDto>>(await _deviceService.GetAllDevices());

            return await Task.FromResult(new OkObjectResult(new
            {
                success = true,
                data = result,
            }));
        }

        [HttpGet("get-by-id/{id:guid}")]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetDeviceById(Guid id)
        {
            var result = _mapper.Map<DeviceDto>(await _deviceService.GetDeviceById(id));

            return await Task.FromResult(new OkObjectResult(new
            {
                success = true,
                data = result,
            }));
        }

        [HttpGet("get-by-brand/{brand}")]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetDeviceByBrand(string brand)
        {
            var result = _mapper.Map<DeviceDto>(await _deviceService.GetDeviceByBrand(brand));

            return await Task.FromResult(new OkObjectResult(new
            {
                success = true,
                data = result,
            }));
        }


        [HttpDelete("delete-by-id/{id:guid}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var result = await _deviceService.DeleteDevice(id);

            return await Task.FromResult(new OkObjectResult(new
            {
                success = true,
                data = result,
            }));
        }

        [HttpPost]
        public async Task<ActionResult<DeviceDto>> AddDevice(DeviceRequest request)
        {
            var result = _mapper.Map<DeviceDto>(await _deviceService.AddDevice(request));

            return await Task.FromResult(new OkObjectResult(new
            {
                success = true,
                data = result,
            }));
        }

        [HttpPatch("id")]
        public async Task<ActionResult<DeviceDto>> PatchDevice(Guid id, [FromBody] DeviceRequest request)
        {
            var result = _mapper.Map<DeviceDto>(await _deviceService.PatchDevice(id, request));

            return await Task.FromResult(new OkObjectResult(new
            {
                success = true,
                data = result,
            }));
        }


        [HttpPut("id")]
        public async Task<ActionResult<DeviceDto>> PutDevice(Guid id, [FromBody] DeviceRequest request)
        {
            var result = _mapper.Map<DeviceDto>(await _deviceService.UpdateDevice(id, request));

            return await Task.FromResult(new OkObjectResult(new
            {
                success = true,
                data = result,
            }));
        }
    }
}
