using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application.Employees.Factories.Contracts;
using CleanArchitecture.Application.Employees.ViewModels;

namespace CleanArchitecture.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : ICreateEmployeeCommand
    {
        private readonly IEmployeeFactory _employeeFactory;
        private readonly IDatabaseService _databaseService;

        public CreateEmployeeCommand(
            IEmployeeFactory employeeFactory,
            IDatabaseService databaseService)
        {
            _employeeFactory = employeeFactory;
            _databaseService = databaseService;
        }

        public async Task ExecuteAsync(BaseEmployeeModel model)
        {
            var employee = _employeeFactory.Create(model);

            await _databaseService.Employees.AddAsync(employee);
            await _databaseService.SaveAsync();
        }
    }
}

