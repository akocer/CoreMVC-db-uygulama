using AutoMapper;
using uy01.Models;
using uy01.ViewModels;

namespace uy01.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
        }
    }
}
