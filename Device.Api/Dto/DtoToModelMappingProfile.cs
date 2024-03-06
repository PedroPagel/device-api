using AutoMapper;

namespace Device.WebApi.Dto
{
    public class DtoToModelMappingProfile : Profile
    {
        public DtoToModelMappingProfile()
        {
            CreateMap<Domain.Entities.Device, DeviceDto>();
            CreateMap<DeviceDto, Domain.Entities.Device>();
        }
    }
}
