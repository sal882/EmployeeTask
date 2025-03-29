using EmployeeTask.VerticalSlicing.Common;
using EmployeeTask.VerticalSlicing.Data.Entities;

namespace EmployeeTask.VerticalSlicing.Features.Employees
{
    public class EmployeeErrors
    {
        public static readonly Error CodeAlreadyExist =
           new("Code Already Exists", StatusCodes.Status400BadRequest);
        public static Error NotFound =>
        new("Employee not found", 404);

        public static Error CodeImmutable =>
            new("Employee code cannot be changed", 400);

        public static Error InvalidId =>
            new("Employee ID must be a positive number", 400);
        public static Error InvalidDeletion =>
            new("You can not Delete Active Users", 400);
    }
}
