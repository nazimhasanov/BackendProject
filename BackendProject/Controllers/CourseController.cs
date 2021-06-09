using BackendProject.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CourseController (AppDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public IActionResult Index()
        {
            var courses = _dbContext.Courses.ToList();
            var coursesDetails = _dbContext.CourseDetails.FirstOrDefault();
            return View();
        }
    }
}
