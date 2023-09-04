using AutoMapper;
using BusinessObjects.Entities;
using DataAccess.ModelViewOdata;
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
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Category, CategoryModelApi>().ReverseMap();

            CreateMap<Blog, BlogModel>()
                .ForMember(dest => dest.content_blog, opt => opt.MapFrom(src => src.content))
                .ForMember(dest => dest.title_blog, opt => opt.MapFrom(src => src.title))
                .ReverseMap();

            CreateMap<Order, ListOrderCustomerByIdModel>()
             .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.Customer.address))
             .ForMember(dest => dest.phone, opt => opt.MapFrom(src => src.Customer.phone));

            CreateMap<Order, ListOrderCustomerByAdminModel>()
             .ForMember(dest => dest.address, opt => opt.MapFrom(src => src.Customer.address))
             .ForMember(dest => dest.phone, opt => opt.MapFrom(src => src.Customer.phone));

            CreateMap<Customer,UserViewModel>().ReverseMap();
            CreateMap<OrdersDetail, AllCartItemModel>()
            .ForMember(dest => dest.productName, opt => opt.MapFrom(src => src.Product.productName))
            .ForMember(dest => dest.image, opt => opt.MapFrom(src => src.Product.image))            
            .ForMember(dest => dest.totalBill, opt => opt.MapFrom(src => src.Order.totalmoney))            
            ;

            CreateMap<OrdersDetail, ViewOrderDetailModel>()
            .ForMember(dest => dest.productName, opt => opt.MapFrom(src => src.Product.productName))
            .ForMember(dest => dest.image, opt => opt.MapFrom(src => src.Product.image));
        }
    }
}
