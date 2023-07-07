using CleanArchitecture.Application.Employees.ViewModels;

namespace CleanArchitecture.Application.Employees.Queries.GetEmployeesList
{
	public interface IGetEmployeesListQuery
	{
		Task<IList<EmployeeModel>> ExecuteAsync();
	}
}

