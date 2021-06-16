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
    [Authorize(Roles = RoleConstant.Admin)]
    public class CategoryController : Controller
    {

        private readonly AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int page = 1 )
        {
            ViewBag.PageCount = Math.Ceiling((decimal)_dbContext.Categories.Where(s => s.IsDeleted == false).Count() / 4);
            ViewBag.Page = page;

            if (ViewBag.PageCount < page || page <= 0)
            {
                return NotFound();
            }

            var categories = _dbContext.Categories.Where(x => x.IsDeleted == false).OrderByDescending(y => y.Id).Skip(((int)page - 1) * 4).Take(4).ToList();


            return View(categories);
         }
       public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExist = _dbContext.Categories.Any(x => x.Title.ToLower() == category.Title.ToLower());
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Category is already EXIST ! ");
                return View();
            }

            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            var category = await _dbContext.Categories.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null)
                return NotFound();

            var dbCategory = await _dbContext.Categories.Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.Id == id);

            if (dbCategory == null)
                return NotFound();

            var isExist = await _dbContext.Categories.AnyAsync(x => x.Title.ToLower() == category.Title.ToLower() && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "This Category is already EXIST !");
                return View();
            }

            dbCategory.Title = category.Title;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
