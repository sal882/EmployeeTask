using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTask.VerticalSlicing.Data.Entities
{
    public class Attachments :BaseEntity
    {
        public string FileId { get; set; } = null!;
        public string Extend { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Path { get; set; } = null!;


    }
}
