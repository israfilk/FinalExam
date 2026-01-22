using Exam.Models;

namespace Exam.Areas.AdminPanel.ViewModels
{
    public class CreateTeamVM

    {
        public IFormFile Photo { get; set; }

        public string Name { get; set; }

    

        public int? RoleId { get; set; }

        public List<Role>? Roles { get; set; }
    }
}
