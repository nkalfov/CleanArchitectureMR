using CleanArchitecture.Application.Employees.ViewModels;
using CleanArchitecture.Domain;

namespace CleanArchitecture.Application.Employees.Factories.Contracts
{
    public interface IEmployeeFactory
    {
        Employee Create(BaseEmployeeModel model);
    }
}

