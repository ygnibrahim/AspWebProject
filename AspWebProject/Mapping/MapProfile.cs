using AspWebProject.DTOs;
using AspWebProject.Models;
using AutoMapper;

namespace AspWebProject.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
