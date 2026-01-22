using System.Diagnostics;
using Exam.Data;
using Exam.Models;
using Exam.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {

            List<Team> teams = await _context.Teams
                .Where(t => !t.IsDeleted)
                .Include(t => t.Role)
                .ToListAsync();
            HomeVM homeVM = new()
            {
                Teams = teams
            };
            return View(homeVM);
        }


      


    }
}
