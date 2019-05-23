using System;
using System.Linq;
using AutoMapper;
using DatingApp.API.Models;
using DatingApp.API.ModelsDTO;
using DatingApp.API.Extensions;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //USER >> USERDTO
            #region USER >> USERDTO
            CreateMap<User, UserDTO>()
                .ForMember(x => x.PhotoUrl, options =>
                {
                    options.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain == true).Url);
                })
                .ForMember(x => x.FullAge, options =>
                {
                    options.MapFrom(src => src.DateOfBirth.CalculateFullAge());
                })
                .ForMember(x => x.Age, options =>
                {
                    options.MapFrom(src => src.DateOfBirth.CalculateAge());
                })
                .ForMember(x => x.CityName, options =>
                {
                    options.MapFrom(src => src.City.Name);
                })
                .ForMember(x => x.CountryName, options =>
                {
                    options.MapFrom(src => src.Country.Name);
                })
                .ForMember(x => x.GenderName, options =>
                {
                    options.MapFrom(src => src.Gender.Name);
                });
            #endregion

        }
    }
}
