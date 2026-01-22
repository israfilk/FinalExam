namespace Exam.Models
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public List<Team> Teams { get; set; }
    }
}
