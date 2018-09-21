using System;
using System.Collections.Generic;
using DeltaTradeOnlineLTD.Data.EntityDataTransferObjects;

namespace DeltaTradeOnlineLTD.App.Interfaces
{
    public interface IEmployeeController
    {
	EmployeeInfoBasicDTO Fire(int employeeId);
	string GetAddress(int employeeId);
	EmployeeBirthdayDTO GetBirthday(int employeeId);
	EmployeeInfoExtraDTO GetInfo(int employeeId);
	ManagerInfoDTO GetManagerInfo(int managerId);
	EmployeeInfoFullDTO GetPersonalInfo(int employeeId);
	void Hire(EmployeeInfoBasicDTO employeeDTO);
	ICollection<EmployeeSalManDTO> ListEmployeesOlderThan(int age);
	void SetBirthday(int employeeId, DateTime birthDate);
	void SetBirthday(EmployeeBirthdayDTO employeeDTO);
	void SetAddress(int employeeId, string address);
	void SetManager(int employeeId, int managerId);
	EmployeeManagerDTO SetManager(EmployeeManagerDTO employeeManagerDTO);
    }
}
