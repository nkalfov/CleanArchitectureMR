using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Application.Employees.ViewModels;

namespace CleanArchitecture.Application.Employees.Commands.EditEmployee
{
    public class EditEmployeeCommand : IEditEmployeeCommand
    {
        private readonly IDatabaseService _databaseService;

        public EditEmployeeCommand(
            IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task ExecuteAsync(EmployeeModel model)
        {
            var employee = await _databaseService.Employees.FindAsync(model.Id);

            if (employee is null)
                return;

            employee.Name = model.Name;

            await _databaseService.SaveAsync();
        }
    }
}

