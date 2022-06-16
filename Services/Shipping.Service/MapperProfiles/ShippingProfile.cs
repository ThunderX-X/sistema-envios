using AutoMapper;
using Shipments.Service.Dtos;
using Shipments.Service.Models;
using Shipments.Service.Rules;
using Shipments.Service.Utils;

namespace Shipments.Service.MapperProfiles
{
    public class ShippingProfile : Profile
    {
        private readonly int GUIDE_NUMBER_LENGTH = 10;
        public ShippingProfile()
        {
            CreateMap<CreateShippingDto, Shipping>()
                .ForMember(dest => dest.CreatedAt, opt =>
                {
                    opt.MapFrom(src => DateTime.Now);
                })
                .ForMember(dest => dest.UpdatedAt, opt =>
                {
                    opt.MapFrom(src => DateTime.Now);
                })
                .ForMember(dest => dest.RegistredAt, opt =>
                {
                    opt.MapFrom(src => DateTime.Now);
                })
                .ForMember(dest => dest.DiscountPercent, opt =>
                {
                    opt.MapFrom(src =>
                        DiscountRule.GetDiscount(src.ShipmentType, src.Quantity)
                    );
                })
                .ForMember(dest => dest.PriceWithDiscount, opt =>
                {
                    opt.MapFrom(src =>
                        src.Price - src.Price * (DiscountRule.GetDiscount(src.ShipmentType, 
                            src.Quantity) / 100)
                    );
                })
                .ForMember(dest => dest.GuideNumber, opt =>
                {
                    opt.MapFrom(src => new RandomUtils().RandomString(GUIDE_NUMBER_LENGTH)
                    );
                });
            CreateMap<UpdateShippingDto, Shipping>()
                .ForMember(dest => dest.UpdatedAt, opt =>
                {
                    opt.MapFrom(src => DateTime.Now);
                });
        }

    }
}
