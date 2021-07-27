using AutoMapper;
using ClaroPrueba.Dtos;
using ClaroPrueba.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaroPrueba.Configuration
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>();

            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
