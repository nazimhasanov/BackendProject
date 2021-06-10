using BackendProject.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _dbContext;

        public TeacherController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var teacherDetail = _dbContext.TeacherDetails.Where(x => x.IsDeleted == false).Include(x => x.Teacher).ThenInclude(y => y.SocialMedias)
                .Include(t => t.Teacher).ThenInclude(t => t.Position).FirstOrDefault(z => z.TeacherId == id);

            if (teacherDetail == null)
                return NotFound();

            return View(teacherDetail);
        }
    }
}
