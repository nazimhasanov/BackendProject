using BackendProject.DataAccessLayer;
using BackendProject.Models;
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

        public IActionResult Index(int? categoryId)
        {
            if (categoryId == null)
            {
                return View();
            }
            else
            {
                List<Course> courses = new List<Course>();
                List<CourseCategory> courseCategories = _dbContext.CourseCategories.Include(x => x.Course).ToList();
                foreach (CourseCategory courseCategory in courseCategories)
                {
                    if (courseCategory.CategoryId == categoryId && courseCategory.Course.IsDelete == false)
                    {
                        courses.Add(courseCategory.Course);
                    }
                }
                return View(courses);
            }
                

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
                Blogs = _dbContext.Blogs.Where(x => x.IsDelete == false).Take(3).ToList(),
                Categories = _dbContext.Categories.Include(x => x.CourseCategories).ToList()
            };
            return View(courseViewModel);
        }

    }
}
