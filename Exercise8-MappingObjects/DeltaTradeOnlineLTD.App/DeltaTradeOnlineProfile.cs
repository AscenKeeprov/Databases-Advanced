using System;
using System.Globalization;
using AutoMapper;
using DeltaTradeOnlineLTD.Data.EntityDataTransferObjects;
using DeltaTradeOnlineLTD.Models;

namespace DeltaTradeOnlineLTD.App
{
    public class DeltaTradeOnlineProfile : Profile
    {
	private const string DateFormat = "dd-MM-yyyy";
	private readonly CultureInfo cultureInfo = CultureInfo.InvariantCulture;

	public DeltaTradeOnlineProfile()
	{
	    CreateMap<Employee, EmployeeBirthdayDTO>()
		.ForMember(dto => dto.Id, opt => opt.MapFrom(e => e.EmployeeId))
		.ForMember(dto => dto.BirthDate, opt => opt.MapFrom(e => e.Birthday != null ? e.Birthday.Value.ToString(DateFormat, cultureInfo) : "unknown"))
		.ReverseMap()
		.ForMember(e => e.Birthday, opt => opt.MapFrom(dto => DateTime.Parse(dto.BirthDate)));

	    CreateMap<Employee, EmployeeInfoBasicDTO>().ReverseMap();

	    CreateMap<Employee, EmployeeInfoExtraDTO>()
		.ForMember(dto => dto.Id, opt => opt.MapFrom(e => e.EmployeeId))
		.ReverseMap();

	    CreateMap<Employee, EmployeeInfoFullDTO>()
		.ForMember(dto => dto.Id, opt => opt.MapFrom(e => e.EmployeeId))
		.ForMember(dto => dto.BirthDate, opt => opt.MapFrom(e => e.Birthday != null ? e.Birthday.Value.ToString(DateFormat, cultureInfo) : "unknown"))
		.ForMember(dto => dto.Address, opt => opt.MapFrom(e => e.Address))
		.ForMember(dto => dto.Address, opt => opt.NullSubstitute("unknown"))
		.ReverseMap()
		.ForMember(e => e.Birthday, opt => opt.MapFrom(dto => DateTime.Parse(dto.BirthDate)));

	    CreateMap<Employee, EmployeeManagerDTO>()
		.ForMember(dto => dto.EmployeeId, opt => opt.MapFrom(e => e.EmployeeId))
		.ForMember(dto => dto.EmployeeName, opt => opt.MapFrom(e => $"{e.FirstName} {e.LastName}"))
		.ForMember(dto => dto.ManagerId, opt => opt.MapFrom(e => e.ManagerId))
		.ForMember(dto => dto.ManagerName, opt => opt.MapFrom(e => $"{e.Manager.FirstName} {e.Manager.LastName}"))
		.ForMember(dto => dto.ManagerName, opt => opt.NullSubstitute("None"))
		.ReverseMap()
		.ForMember(e => e.FirstName, opt => opt.MapFrom(dto => dto.EmployeeName.Split()[0]))
		.ForMember(e => e.LastName, opt => opt.MapFrom(dto => dto.EmployeeName.Split()[1]));

	    CreateMap<Employee, ManagerInfoDTO>()
		.ForMember(dto => dto.Name, opt => opt.MapFrom(e => $"{e.FirstName} {e.LastName}"))
		.ForMember(dto => dto.SubordinatesCount, opt => opt.MapFrom(e => e.Subordinates.Count));

	    CreateMap<Employee, EmployeeSalManDTO>()
		.ForMember(dto => dto.EmployeeFullName, opt => opt.MapFrom(e => $"{e.FirstName} {e.LastName}"))
		.ForMember(dto => dto.EmployeeSalary, opt => opt.MapFrom(e => e.Salary))
		.ForMember(dto => dto.ManagerLastName, opt => opt.MapFrom(e => e.Manager.LastName))
		.ForMember(dto => dto.ManagerLastName, opt => opt.NullSubstitute("[no manager]"));
	}
    }
}
