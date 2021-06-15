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

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var course = _dbContext.Courses.Include(x => x.CourseDetails).FirstOrDefault(x => x.Id == id);

            if (course == null)
                return NotFound();

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteCourse(int? id)
        {
            if (id == null)
                return NotFound();

            var course = _dbContext.Courses.Include(x => x.CourseDetails).FirstOrDefault(x => x.Id == id);

            if (course == null)
                return NotFound();

            course.IsDelete = true;
            course.CourseDetails.IsDelete = true;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {

            if (id == null)
                return NotFound();

            var course = _dbContext.Courses
                .Include(x => x.CourseDetails).Where(y => y.IsDelete == false)
                .FirstOrDefault(x => x.Id == id);
            if (course == null)
                return NotFound();

            return View(course);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Course course)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null)
                return NotFound();

            if (id != course.Id)
                return BadRequest();

            var dbCourse = _dbContext.Courses
               .Include(x => x.CourseDetails).Where(y => y.IsDelete == false)
               .FirstOrDefault(x => x.Id == id);

            if (dbCourse == null)
                return NotFound();


            var isExist = await _dbContext.Courses.Where(x => x.IsDelete == false)
                                                .AnyAsync(x => x.Title == course.Title && x.Id != course.Id);

            if (isExist)
            {
                ModelState.AddModelError("Name", "This course name already exist.");
                return View();
            }




            if (course.Image != null)
            {
                var path = Path.Combine(Constants.EventImageFolderPath, "course", dbCourse.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                var courseImg = Path.Combine(Constants.EventImageFolderPath, "course");
                var fileName = await FilesUtil.GenerateFileAsync(courseImg, course.Photo);

                if (course.Photo == null)
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!course.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!course.Photo.IsSizeAllowed(2048))
                {
                    ModelState.AddModelError("Photo", "Max size is 2 MB.");
                    return View();
                }
                course.Image = fileName;
            }


            dbCourse.Title = course.Title;
            dbCourse.Subtitle = course.Subtitle;
            dbCourse.CourseDetails.About= course.CourseDetails.About;
            dbCourse.CourseDetails.Apply = course.CourseDetails.Apply;
            dbCourse.CourseDetails.Certification = course.CourseDetails.Certification;
            dbCourse.CourseDetails.CourseStart = course.CourseDetails.CourseStart;
            dbCourse.CourseDetails.CourseDuration = course.CourseDetails.CourseDuration;
            dbCourse.CourseDetails.CourseClassDuration = course.CourseDetails.CourseClassDuration;
            dbCourse.CourseDetails.CourseSkillLevel = course.CourseDetails.CourseSkillLevel;
            dbCourse.CourseDetails.CourseLanguage = course.CourseDetails.CourseLanguage;
            dbCourse.CourseDetails.CourseStudent = course.CourseDetails.CourseStudent;
            dbCourse.CourseDetails.CourseAssesment = course.CourseDetails.CourseAssesment;
            dbCourse.CourseDetails.CourseFee = course.CourseDetails.CourseFee;

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
