using System;
using System.Linq;
using AutoMapper;
using CarDealer.Data.DataTransferObjects;
using CarDealer.Models;

namespace CarDealer.App
{
    public class CarDealerProfile : Profile
    {
	public CarDealerProfile()
	{
	    CreateMap<CarDto, Car>().ReverseMap();

	    CreateMap<Customer, CustomerExpenditureDto>()
		.ForMember(dto => dto.FullName, opt => opt.MapFrom(c => c.Name))
		.ForMember(dto => dto.CarsBought, opt => opt.MapFrom(c => c.CarPurchases.Count))
		.ForMember(dto => dto.MoneySpent, opt => opt.MapFrom(c => String.Format("{0:#.00}", c.CarPurchases.Sum(cp => cp.CarPriceDiscounted))));

	    CreateMap<CustomerDto, Customer>().ReverseMap();

	    CreateMap<PartDto, Part>().ReverseMap();

	    CreateMap<SupplierDto, Supplier>().ReverseMap();
	}
    }
}
