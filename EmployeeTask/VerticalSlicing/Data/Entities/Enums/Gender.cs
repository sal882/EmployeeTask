using System.Runtime.Serialization;

namespace EmployeeTask.API.VerticalSlicing.Data.Enums
{
    public enum Gender
    {
        [EnumMember(Value = "Female")]
        Female,

        [EnumMember(Value = "Male")]
        Male,
    }
}
