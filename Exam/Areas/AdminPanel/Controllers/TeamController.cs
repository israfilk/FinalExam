using Exam.Areas.AdminPanel.ViewModels;
using Exam.Areas.AdminPanel.ViewModels.Team;
using Exam.Data;
using Exam.Models;
using Exam.Utilits.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
           _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<GetTeamVM> getTrainerVMs = await _context.Teams
                .Where(t => !t.IsDeleted)
                .Include(t => t.Role)
                .Select(t => new GetTeamVM
                {
                    Id = t.Id,
                    Name = t.Name,
                    Image = t.Image,
                    RoleName = t.Role.Name
                }).ToListAsync();

            return View(getTrainerVMs);
        }


      

        public async Task<IActionResult> Create()
        {
            CreateTeamVM createTeamVM = new()
            {
                Roles = await _context.Roles.ToListAsync(),

            };

            return View(createTeamVM);

        }
       [HttpPost] 
        public  async Task<IActionResult> Create(CreateTeamVM createTeamVM)
        {
            createTeamVM.Roles= await _context.Roles.ToListAsync();
            {
                if (!ModelState.IsValid)
                {
                    return View(createTeamVM);
                }

                if (!createTeamVM.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError(nameof(createTeamVM.Photo), "File type is incorrect");
                    return View(createTeamVM);
                }

                bool existRole = createTeamVM.Roles.Any(r => r.Id == createTeamVM.RoleId);

                if (!existRole)
                {
                    ModelState.AddModelError(nameof(createTeamVM.RoleId), "Role does not exist!");
                    return View(createTeamVM);
                }

                Team trainer = new()
                {
                    Name = createTeamVM.Name,
             
                    RoleId = createTeamVM.RoleId.Value,
                    Image = await createTeamVM.Photo.CreateFileAsync(_env.WebRootPath, "assets/images"),
                };
                await _context.Teams.AddAsync(trainer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == null) return NotFound(); 
            Team team = await _context.Teams.FindAsync(id);
            if(team == null) return NotFound();
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }


    }
}
