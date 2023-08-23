using AutoMapper;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Customer, CustomerEntity>();
        }
    }
}