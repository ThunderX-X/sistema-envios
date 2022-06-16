using AutoMapper;
using DeliveryPoints.Service.Dtos;
using DeliveryPoints.Service.Models;

namespace DeliveryPoints.Service.MapperProfiles
{
    public class DeliveryPointProfile : Profile
    {
        public DeliveryPointProfile()
        {
            CreateMap<CreateDeliveryPointDto, DeliveryPoint>()
                .ForMember(
                    dest => dest.CreatedAt,
                    src => src.MapFrom(map => DateTime.Now)
                )
                .ForMember(
                    dest => dest.UpdatedAt,
                    src => src.MapFrom(map => DateTime.Now)
                );
            CreateMap<UpdateDeliveryPointDto, DeliveryPoint>()
                .ForMember(
                    dest => dest.UpdatedAt,
                    src => src.MapFrom(map => DateTime.Now)
                );
        }
    }
}
