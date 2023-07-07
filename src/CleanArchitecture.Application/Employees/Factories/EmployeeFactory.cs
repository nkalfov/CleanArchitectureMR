using CleanArchitecture.Application.Employees.Factories.Contracts;
using CleanArchitecture.Application.Employees.ViewModels;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Employees.Factories
{
    public class EmployeeFactory : IEmployeeFactory
    {
        public Employee Create(BaseEmployeeModel model)
        {
            var result = new Employee
            {
                Name = model.Name
            };

            return result;
        }
    }
}

