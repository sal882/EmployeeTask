namespace EmployeeTask.VerticalSlicing.Data.Entities
{
    public class UserRoleChangeLog: BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int OldRoleId { get; set; }
        public Role OldRole { get; set; }

        public int NewRoleId { get; set; }
        public Role NewRole { get; set; }

        public int ChangedByUserId { get; set; }
        public User ChangedByUser { get; set; }
    }
}
