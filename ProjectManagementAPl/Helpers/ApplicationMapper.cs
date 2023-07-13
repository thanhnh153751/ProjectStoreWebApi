using AutoMapper;
using BusinessObjects.Entities;
using ProjectManagementAPl.Models;
using ProjectManagementAPl.ViewModels;

namespace ProjectManagementAPl.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Product, ProductViewModel>()
            .ForMember(dest => dest.categoryName, opt => opt.MapFrom(src => src.Category.categoryName));
            CreateMap<Product, ProductModelApi>().ReverseMap();
            CreateMap<Category, CategoryModelApi>().ReverseMap();
            //CreateMap<Author, AuthorViewModel>().ReverseMap();
            //CreateMap<Publisher, PublisherViewModel>().ReverseMap();
            //CreateMap<User, UserViewModel>().ReverseMap();
            //CreateMap<User, UserRegiter>().ReverseMap();
            CreateMap<UserRegiter, UserViewModel>()
            .ForMember(dest => dest.email_address, opt => opt.MapFrom(src => src.Email)).ReverseMap();
        }
    }
}
