namespace Exam.Models
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}
