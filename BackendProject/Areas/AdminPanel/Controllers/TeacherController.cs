using BackendProject.Areas.AdminPanel.Utils;
using BackendProject.DataAccessLayer;
using BackendProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    //[Authorize(Roles = RoleConstant.Admin)]
    public class TeacherController : Controller
    {      

        private readonly AppDbContext _dbContext;

        public TeacherController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)_dbContext.Teachers.Where(s => s.IsDeleted == false).Count() / 4);
            ViewBag.Page = page;

            if (ViewBag.PageCount < ViewBag.Page || page <= 0)
            {
                return BadRequest();
            }
            var teachers = _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(x => x.TeacherDetail).Include(x => x.Position)
                                        .OrderByDescending(y => y.Id).Include(x => x.SocialMedias).Skip(((int)page - 1) * 4).Take(4).ToList();

            return View(teachers);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.position = await _dbContext.Position.Where(x => x.IsDelete == false).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher teacher, int PositionId)
        {
            var positions = await _dbContext.Position.Where(x => x.IsDelete == false).ToListAsync();
            ViewBag.Position = positions;

            if (teacher.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo cannot be empty");
                return View();
            }
            if (!teacher.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "You must choose only Image");
                return View();
            }
            if (!teacher.Photo.IsSizeAllowed(2048))
            {
                ModelState.AddModelError("Photo", "Image size can be 2 MB");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            var teacherImg = Path.Combine(Constants.ImageFolderPath, "teacher");
            var fileName = await FilesUtil.GenerateFileAsync(teacherImg, teacher.Photo);
            teacher.Image = fileName;
            teacher.PositionId = PositionId;

            await _dbContext.AddRangeAsync(teacher, teacher.TeacherDetail);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Update(int? id)
        {

            var positions = await _dbContext.Position.Where(x => x.IsDelete == false).ToListAsync();
            ViewBag.Position = positions;

            if (id == null)
                return NotFound();

            var teacher = await _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(x => x.TeacherDetail)
                                                .Include(x => x.SocialMedias).Include(x => x.Position)
                                                .FirstOrDefaultAsync(x => x.Id == id);

            if (teacher == null)
                return NotFound();


            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id, Teacher teacher, int? PositionId)
        {
            var positions = await _dbContext.Position.Where(x => x.IsDelete == false).ToListAsync();
            ViewBag.Position = positions;

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null)
                return NotFound();

            if (id != teacher.Id)
                return BadRequest();

            var dbTeacher = await _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(x => x.TeacherDetail)
                                                .Include(x => x.SocialMedias).Include(x => x.Position)
                                                .FirstOrDefaultAsync(x => x.Id == id);

            if (dbTeacher == null)
                return NotFound();

            if (teacher.Photo != null)
            {
                var path = Path.Combine(Constants.ImageFolderPath, "teacher", dbTeacher.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                var teacherImg = Path.Combine(Constants.ImageFolderPath, "teacher");
                var fileName = await FilesUtil.GenerateFileAsync(teacherImg, teacher.Photo);

                if (teacher.Photo == null)
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!teacher.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!teacher.Photo.IsSizeAllowed(2048))
                {
                    ModelState.AddModelError("Photo", "Max size is 2 MB.");
                    return View();
                }
                dbTeacher.Image = fileName;
            }

            dbTeacher.FullName = teacher.FullName;
            dbTeacher.TeacherDetail.Title = teacher.TeacherDetail.Title;
            dbTeacher.TeacherDetail.Degree = teacher.TeacherDetail.Degree;
            dbTeacher.TeacherDetail.Experience = teacher.TeacherDetail.Experience;
            dbTeacher.TeacherDetail.Hobbies = teacher.TeacherDetail.Hobbies;
            dbTeacher.TeacherDetail.Faculty = teacher.TeacherDetail.Faculty;
            dbTeacher.TeacherDetail.Email = teacher.TeacherDetail.Email;
            dbTeacher.TeacherDetail.Skype = teacher.TeacherDetail.Skype;
            dbTeacher.TeacherDetail.Language = teacher.TeacherDetail.Language;
            dbTeacher.TeacherDetail.TeamLeader = teacher.TeacherDetail.TeamLeader;
            dbTeacher.TeacherDetail.Development = teacher.TeacherDetail.Development;
            dbTeacher.TeacherDetail.Design = teacher.TeacherDetail.Design;
            dbTeacher.TeacherDetail.Innovation = teacher.TeacherDetail.Innovation;
            dbTeacher.TeacherDetail.Communication = teacher.TeacherDetail.Communication;
            dbTeacher.PositionId = (int)PositionId;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    //if (id == null)
        //    //    return NotFound();

        //    //var teacher = await _context.Teachers.Where(x => x.IsDeleted == false).Include(x => x.TeacherDetail)
        //    //                                    .Include(x => x.SocialMedias).Include(x => x.Position)
        //    //                                    .FirstOrDefaultAsync(x => x.Id == id);

        //    //if (teacher == null)
        //    //    return NotFound();

        //    return View();
        //}

    }
}
