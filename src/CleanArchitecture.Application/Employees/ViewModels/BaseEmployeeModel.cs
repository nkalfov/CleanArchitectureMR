using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Common.Dimensions;

namespace CleanArchitecture.Application.Employees.ViewModels
{
    public class BaseEmployeeModel
    {
        [StringLength(
            EmployeeDimensions.LengthMax,
            MinimumLength = EmployeeDimensions.LengthMin)]
        public string Name { get; set; } = string.Empty;
    }
}

