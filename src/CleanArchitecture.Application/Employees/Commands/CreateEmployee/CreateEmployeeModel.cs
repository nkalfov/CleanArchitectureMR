using System;
using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Common.Dimensions;

namespace CleanArchitecture.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeModel
    {
        [StringLength(
            EmployeeDimensions.LengthMax,
            MinimumLength = EmployeeDimensions.LengthMin)]
        public string Name { get; set; } = string.Empty;
    }
}

