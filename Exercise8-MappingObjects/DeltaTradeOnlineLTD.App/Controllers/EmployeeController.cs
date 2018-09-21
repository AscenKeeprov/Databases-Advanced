using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DeltaTradeOnlineLTD.App.Interfaces;
using DeltaTradeOnlineLTD.Data;
using DeltaTradeOnlineLTD.Data.EntityDataTransferObjects;
using DeltaTradeOnlineLTD.Models;

namespace DeltaTradeOnlineLTD.App.Controllers
{
    public class EmployeeController : IEmployeeController
    {
	private const string IdNotFound = "No employee with ID {0} works for the company.";
	private const string AgeNotFound = "None of the company employees is older than {0} years.";

	private readonly DeltaTradeOnlineDbContext context;
	private readonly IMapper mapper;

	public EmployeeController(DeltaTradeOnlineDbContext dbContext, IMapper autoMapper)
	{
	    context = dbContext;
	    mapper = autoMapper;
	}

	public EmployeeInfoBasicDTO Fire(int employeeId)
	{
	    Employee employee = context.Employees
		.Where(e => e.EmployeeId == employeeId)
		.SingleOrDefault();
	    if (employee == null)
		throw new ArgumentException(String.Format(IdNotFound, employeeId));
	    var employeeBasicInfoDTO = mapper.Map<EmployeeInfoBasicDTO>(employee);
	    context.Employees.Remove(employee);
	    context.SaveChanges();
	    return employeeBasicInfoDTO;
	}

	public string GetAddress(int employeeId)
	{
	    Employee employee = context.Employees
		.Where(e => e.EmployeeId == employeeId)
		.SingleOrDefault();
	    if (employee == null)
		throw new ArgumentException(String.Format(IdNotFound, employeeId));
	    return employee.Address;
	}

	public EmployeeBirthdayDTO GetBirthday(int employeeId)
	{
	    var employeeBirthdayDTO = context.Employees
		.Where(e => e.EmployeeId == employeeId)
		.ProjectTo<EmployeeBirthdayDTO>(mapper.ConfigurationProvider)
		.SingleOrDefault();
	    if (employeeBirthdayDTO == null)
		throw new ArgumentException(String.Format(IdNotFound, employeeId));
	    return employeeBirthdayDTO;
	}

	public EmployeeInfoExtraDTO GetInfo(int employeeId)
	{
	    Employee employee = context.Employees.Find(employeeId);
	    if (employee == null)
		throw new ArgumentException(String.Format(IdNotFound, employeeId));
	    var employeeExtraInfoDTO = mapper.Map<EmployeeInfoExtraDTO>(employee);
	    return employeeExtraInfoDTO;
	}

	public ManagerInfoDTO GetManagerInfo(int managerId)
	{
	    var managerInfoDTO = context.Employees
		.Where(e => e.EmployeeId == managerId)
		.ProjectTo<ManagerInfoDTO>(mapper.ConfigurationProvider)
		.SingleOrDefault();
	    if (managerInfoDTO == null)
		throw new ArgumentException(String.Format(IdNotFound, managerId));
	    return managerInfoDTO;
	}

	public EmployeeInfoFullDTO GetPersonalInfo(int employeeId)
	{
	    var employeeFullInfoDTO = context.Employees
		.Where(e => e.EmployeeId == employeeId)
		.ProjectTo<EmployeeInfoFullDTO>(mapper.ConfigurationProvider)
		.SingleOrDefault();
	    if (employeeFullInfoDTO == null)
		throw new ArgumentException(String.Format(IdNotFound, employeeId));
	    return employeeFullInfoDTO;
	}

	public void Hire(EmployeeInfoBasicDTO employeeDTO)
	{
	    Employee employee = mapper.Map<Employee>(employeeDTO);
	    context.Employees.Add(employee);
	    context.SaveChanges();
	}

	public ICollection<EmployeeSalManDTO> ListEmployeesOlderThan(int age)
	{
	    var employeesSalaryManagerInfo = context.Employees
		.Where(e => (DateTime.Now.Year - e.Birthday.Value.Year) > age)
		.ProjectTo<EmployeeSalManDTO>(mapper.ConfigurationProvider)
		.ToList();
	    if (employeesSalaryManagerInfo.Count == 0)
		throw new ArgumentException(String.Format(AgeNotFound, age));
	    return employeesSalaryManagerInfo;
	}

	public void SetAddress(int employeeId, string address)
	{
	    Employee employee = context.Employees.Find(employeeId);
	    if (employee == null)
		throw new ArgumentException(String.Format(IdNotFound, employeeId));
	    employee.Address = address;
	    context.SaveChanges();
	}

	public void SetBirthday(int employeeId, DateTime birthDate)
	{
	    Employee employee = context.Employees.Find(employeeId);
	    if (employee == null)
		throw new ArgumentException(String.Format(IdNotFound, employeeId));
	    employee.Birthday = birthDate;
	    context.SaveChanges();
	}

	public void SetBirthday(EmployeeBirthdayDTO employeeDTO)
	{
	    Employee employee = context.Employees.Find(employeeDTO.Id);
	    if (employee == null)
		throw new ArgumentException(String.Format(IdNotFound, employeeDTO.Id));
	    mapper.Map(employeeDTO, employee);
	    context.Employees.Update(employee);
	    context.SaveChanges();
	}

	public void SetManager(int employeeId, int managerId)
	{
	    Employee employee = context.Employees.Find(employeeId);
	    if (employee == null)
		throw new ArgumentException(String.Format(IdNotFound, employeeId));
	    Employee manager = context.Employees.Find(managerId);
	    if (manager == null)
		throw new ArgumentException(String.Format(IdNotFound, managerId));
	    employee.Manager = manager;
	    context.SaveChanges();
	}

	public EmployeeManagerDTO SetManager(EmployeeManagerDTO dto)
	{
	    Employee employee = context.Employees.Find(dto.EmployeeId);
	    if (employee == null)
		throw new ArgumentException(String.Format(IdNotFound, dto.EmployeeId));
	    Employee manager = context.Employees.Find(dto.ManagerId);
	    if (manager == null)
		throw new ArgumentException(String.Format(IdNotFound, dto.ManagerId));
	    employee.Manager = manager;
	    dto = mapper.Map<EmployeeManagerDTO>(employee);
	    context.SaveChanges();
	    return dto;
	}
    }
}
