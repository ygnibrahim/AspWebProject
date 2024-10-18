using AspWebProject.DTOs;
using AspWebProject.Models;
using AutoMapper;

namespace AspWebProject.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            // normal mapping işlemi

            //CreateMap<Customer, CustomerDto>().ReverseMap();


            //eğer alanlar farklı isimlerde ise bu sekilde maplama yapılır  bu yüzden aynı isimleri vermeye dikkat etmekte fayda var!!

            CreateMap<CreditCard, CustomerDto>();

            CreateMap<Customer, CustomerDto>().IncludeMembers(x=>x.CreditCard)
                .ForMember(dest => dest.Isim, opt => opt.MapFrom(dest => dest.Name))
                .ForMember(dest => dest.Eposta, opt => opt.MapFrom(dest => dest.Email))
                .ForMember(dest => dest.Yas, opt => opt.MapFrom(dest => dest.Age))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(dest => dest.GetFullName()));
        }
    }
}
