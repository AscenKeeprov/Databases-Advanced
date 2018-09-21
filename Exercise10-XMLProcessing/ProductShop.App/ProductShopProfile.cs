using System;
using System.Linq;
using AutoMapper;
using ProductShop.Data.DataTransferObjects;
using ProductShop.Models;

namespace ProductShop.App
{
    public class ProductShopProfile : Profile
    {
	public ProductShopProfile()
	{
	    CreateMap<Category, CategoryProductsDto>()
		.ForMember(dto => dto.Name, opt => opt.MapFrom(c => c.Name))
		.ForMember(dto => dto.ProductsCount, opt => opt.MapFrom(c => c.CategoryProducts.Count))
		.ForMember(dto => dto.ProductsAveragePrice, opt => opt.MapFrom(
		    c => String.Format("{0:#.00}", c.CategoryProducts.Average(cp => cp.Product.Price))))
		.ForMember(dto => dto.ProductsAveragePrice, opt => opt.MapFrom(
		    c => String.Format("{0:#.00}", c.CategoryProducts.Sum(cp => cp.Product.Price))));

	    CreateMap<CategoryDto, Category>().ReverseMap();

	    CreateMap<Product, ProductBuyerDto>()
		.ForMember(dto => dto.Name, opt => opt.MapFrom(p => p.Name))
		.ForMember(dto => dto.Price, opt => opt.MapFrom(p => p.Price))
		.ForMember(dto => dto.Buyer, opt => opt.MapFrom(p => $"{(p.Buyer.FirstName != null ? $"{p.Buyer.FirstName} " : String.Empty)}{p.Buyer.LastName}"));

	    CreateMap<Product, ProductPriceDto>().ReverseMap();

	    CreateMap<ProductDto, Product>().ReverseMap();

	    CreateMap<User, UserProductsSoldDto>()
		.ForMember(dto => dto.FirstName, opt => opt.MapFrom(u => u.FirstName))
		.ForMember(dto => dto.LastName, opt => opt.MapFrom(u => u.LastName))
		.ForMember(dto => dto.ProductsSold, opt => opt.MapFrom(u => u.ProductsSold));

	    CreateMap<User, UserSoldProductsDto>()
		.ForMember(dto => dto.FirstName, opt => opt.MapFrom(u => u.FirstName))
		.ForMember(dto => dto.LastName, opt => opt.MapFrom(u => u.LastName))
		.ForMember(dto => dto.Age, opt => opt.MapFrom(u => (u.Age != null ? u.Age.ToString() : null)))
		.ForMember(dto => dto.SoldProducts, opt => opt.MapFrom(u => u.ProductsSold));

	    CreateMap<UserDto, User>()
		.ForMember(u => u.Age, opt => opt.MapFrom(dto => int.Parse(dto.Age)))
		.ReverseMap()
		.ForMember(dto => dto.Age, opt => opt.MapFrom(u => (u.Age != null ? u.Age.ToString() : null)));
	}
    }
}
