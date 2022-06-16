using AutoMapper;
using Clients.Service.DTOs;
using Clients.Service.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Clients.Service.MapperProfiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<CreateClientDTO, Client>()
                .ForMember(dest => dest.CreatedAt, opt =>
                {
                    opt.MapFrom(src => DateTime.Now);
                })
                .ForMember(dest => dest.UpdatedAt, opt =>
                 {
                     opt.MapFrom(src => DateTime.Now);
                 });
            CreateMap<UpdateClientDTO, Client>()
                .ForMember(dest => dest.UpdatedAt, opt =>
                {
                    opt.MapFrom(src => DateTime.Now);
                });
        }
    }
}
