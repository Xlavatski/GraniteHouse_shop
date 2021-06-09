using AutoMapper;
using GraniteHouse.Models;
using GraniteHouse.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraniteHouse.AutoMapper
{
    public class QueryProfile : Profile
    {
        public QueryProfile()
        {
            CreateMap<Products, ProductVMEntitet>()
                .ForMember(dest =>
                dest.ProductType,
                opt => opt.MapFrom(src => src.ProductTypes.Name))
                .ForMember(dest =>
                dest.SpecialTag,
                opt => opt.MapFrom(src => src.SpecialTags.SpecialName));
        }
    }
}
