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
    public class BlogController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BlogController(AppDbContext dbContext)
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

            var blogDetails = _dbContext.BlogDetails.Where(x => x.isDelete == false)
                                                  .Include(x => x.Blog).OrderByDescending(x => x.Id)
                                                  .FirstOrDefault(x => x.BlogId == id);
            if (blogDetails == null)
                return NotFound();

            var blogViewModel = new BlogViewModel
            {
                BlogDetail = blogDetails,
                Blogs = _dbContext.Blogs.Where(x => x.IsDelete == false).Take(3).ToList()
            };
            return View(blogViewModel);
        }

    }
}
