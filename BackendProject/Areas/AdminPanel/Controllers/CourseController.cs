using BackendProject.Areas.AdminPanel.Utils;
using BackendProject.DataAccessLayer;
using BackendProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CourseController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var courses = _dbContext.Courses.Where(x => x.IsDelete == false).OrderByDescending(x => x.Id).ToList();

            return View(courses);
        }
       

        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var course = _dbContext.Courses.Where(x => x.IsDelete == false).Include(x => x.CourseDetails)
                                           .FirstOrDefault(x => x.Id == id);

            if (course == null)
                return NotFound();

            return View(course);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (course.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo cannot be empty");
                return View();
            }
            if (!course.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "You must choose only Image");
                return View();
            }
            if (!course.Photo.IsSizeAllowed(2048))
            {
                ModelState.AddModelError("Photo", "Image size can be 2 MB");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            var courseImg = Path.Combine(Constants.EventImageFolderPath, "course");
            var fileName = await FilesUtil.GenerateFileAsync(courseImg, course.Photo);
            course.Image = fileName;

            var isExist = await _dbContext.Courses.AnyAsync(x => x.IsDelete == false && x.Title.ToLower() == course.Title.ToLower());

            if (isExist)
            {
                ModelState.AddModelError("Name", "This name is available");
                return View();
            }

            await _dbContext.AddRangeAsync(course, course.CourseDetails);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
