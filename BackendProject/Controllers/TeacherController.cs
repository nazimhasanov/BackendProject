using BackendProject.DataAccessLayer;
using BackendProject.ViewModels;
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
            
            var teacherDetail = _dbContext.TeacherDetails.Where(x => x.IsDeleted == false).Include(x => x.Teacher).ThenInclude(x => x.SocialMedias) 
                .Include(x => x.Teacher).ThenInclude(x => x.Position).FirstOrDefault(z => z.Id == id);
            
            if (teacherDetail == null)
                return NotFound();


            return View(teacherDetail);

        }
    }
}
