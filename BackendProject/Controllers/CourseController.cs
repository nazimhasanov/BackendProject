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
    public class CourseController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CourseController (AppDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public IActionResult Index()
        {
            var courses = _dbContext.Courses.Where(c => c.IsDelete == false).ToList();      

            return View(courses);
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var courseDetails = _dbContext.CourseDetails.Where(x => x.IsDelete == false).Include(x => x.Courses).OrderByDescending(t => t.Id)
                                                                                                        .FirstOrDefault(y => y.CourseId == id);

            if (courseDetails == null)
                return NotFound();

            var courseViewModel = new CourseViewModel
            {
                CourseDetail = courseDetails,
                Blogs = _dbContext.Blogs.Where(x => x.IsDelete == false).Take(3).ToList()
            };
            return View(courseViewModel);
        }

    }
}
