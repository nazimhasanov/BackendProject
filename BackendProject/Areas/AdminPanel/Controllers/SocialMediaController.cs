using BackendProject.Data;
using BackendProject.DataAccessLayer;
using BackendProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    //[Authorize(Roles = RoleConstants.AdminRole)]
    public class SocialMediaController : Controller
    {
        private readonly AppDbContext _dbContext;

        public SocialMediaController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var socialMedias = _dbContext.SocialMedia.Where(x => x.IsDeleted == false).Include(x => x.Teacher).ToList();
            return View(socialMedias);
        }
       
        public IActionResult Create()
        {
            var teachers = _dbContext.Teachers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Teacher = teachers;
            ViewBag.SocialMedias = _dbContext.SocialMedia.ToList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialMedia socialMedia, int TeacherId)
        {
            var teachers = _dbContext.Teachers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Teacher = teachers;
            ViewBag.SocialMedias = _dbContext.SocialMedia.ToList();


            if (!ModelState.IsValid)
            {
                return View();
            }


            socialMedia.TeacherId = TeacherId;

            await _dbContext.AddRangeAsync(socialMedia);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {

            var teachers = _dbContext.Teachers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Teacher = teachers;

            if (id == null)
                return NotFound();

            var socialMedia = await _dbContext.SocialMedia.Where(x => x.IsDeleted == false).Include(x => x.Teacher)
                                                          .FirstOrDefaultAsync(x => x.Id == id);


            if (socialMedia == null)
                return NotFound();


            return View(socialMedia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, SocialMedia socialMedia, int? TeacherId)
        {
            var teachers = _dbContext.Teachers.Where(x => x.IsDeleted == false).ToList();
            ViewBag.Teacher = teachers;

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null)
                return NotFound();

            if (id != socialMedia.Id)
                return BadRequest();

            var dbSocialMedia = await _dbContext.SocialMedia.Where(x => x.IsDeleted == false).Include(x => x.Teacher)
                                                         .FirstOrDefaultAsync(x => x.Id == id);

            if (dbSocialMedia == null)
                return NotFound();

            dbSocialMedia.Icon = socialMedia.Icon;
            dbSocialMedia.Link = socialMedia.Link;
            dbSocialMedia.TeacherId = (int)TeacherId;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var socialMedia = await _dbContext.SocialMedia.Where(x => x.IsDeleted == false).Include(x => x.Teacher)
                                                         .FirstOrDefaultAsync(x => x.Id == id);


            if (socialMedia == null)
                return NotFound();

            return View(socialMedia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteSocialMedia(int? id)
        {
            if (id == null)
                return NotFound();

            var socialMedia = await _dbContext.SocialMedia.Where(x => x.IsDeleted == false).Include(x => x.Teacher)
                                                         .FirstOrDefaultAsync(x => x.Id == id);


            if (socialMedia == null)
                return NotFound();

            socialMedia.IsDeleted = true;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}
