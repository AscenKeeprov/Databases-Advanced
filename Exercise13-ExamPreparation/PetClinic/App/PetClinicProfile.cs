namespace PetClinic.App
{
    using System;
    using System.Globalization;
    using AutoMapper;
    using PetClinic.Data.DataTransferObjects;
    using PetClinic.Models;

    public class PetClinicProfile : Profile
    {
	public PetClinicProfile()
	{
	    CreateMap<AnimalAidDto, AnimalAid>().ReverseMap();

	    CreateMap<AnimalAidNameDto, AnimalAid>().ReverseMap();

	    CreateMap<AnimalImportDto, Animal>().ReverseMap();

	    CreateMap<PassportImportDto, Passport>()
		.ForMember(p => p.RegistrationDate, opt => opt.MapFrom(dto => DateTime.ParseExact(dto.RegistrationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture)))
		.ReverseMap();

	    CreateMap<ProcedureImportDto, Procedure>()
		.ForPath(p => p.Vet.Name, opt => opt.MapFrom(dto => dto.VetName))
		.ForPath(p => p.Animal.PassportSerialNumber, opt => opt.MapFrom(dto => dto.AnimalPassportSerialNumber))
		.ForMember(p => p.DateTime, opt => opt.MapFrom(dto => DateTime.ParseExact(dto.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture)))
		.ReverseMap();

	    CreateMap<VetImportDto, Vet>().ReverseMap();
	}
    }
}
