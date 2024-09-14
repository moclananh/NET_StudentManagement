using AutoMapper;
using StudentManagement.Data.Models;
using StudentManagement.Data.ViewModels.CategoryDTO;
using StudentManagement.Data.ViewModels.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.AutoMapper
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            CreateMap<CreateProductRequest, Product>().ReverseMap();
         
            CreateMap<Product, ProductVm>()
           .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ProductInCategories.Select(pic => pic.Category))).ReverseMap();

            CreateMap<Category, CategoryVm>();
        }
    }
}
