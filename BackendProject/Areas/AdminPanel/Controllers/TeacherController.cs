using BackendProject.Areas.AdminPanel.Utils;
using BackendProject.DataAccessLayer;
using BackendProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> Create(Teacher teacher, int? positionId)
        {
            ViewBag.position = await _dbContext.Position.Where(x => x.IsDelete == false).ToListAsync();

            if (positionId == null)
            {
                ModelState.AddModelError("", "Please select position");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (teacher.Photo == null)
            {
                ModelState.AddModelError("Photo", "Please select Photo");
                return View();
            }

            if (!teacher.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Please Select Photo");
                return View();
            }

            if (!teacher.Photo.IsSizeAllowed(2048))
            {
                ModelState.AddModelError("Photo", "Photo's size is high 2 MB");
                return View();
            }

            var fileName = await FilesUtil.GenerateFileAsync(Constants.TeacherImageFolderPath, teacher.Photo);
            teacher.Image = fileName;
            teacher.IsDeleted = false;
            teacher.PositionId = (int)positionId;
            await _dbContext.Teachers.AddAsync(teacher);
            teacher.TeacherDetail.TeacherId = teacher.Id;
            await _dbContext.TeacherDetails.AddAsync(teacher.TeacherDetail);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}
