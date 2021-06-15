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
    public class BlogController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BlogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var blogs = _dbContext.Blogs.Where(x => x.IsDelete == false).OrderByDescending(x => x.Id).ToList();
            return View(blogs);
        }

        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var blog = _dbContext.Blogs.Where(x => x.IsDelete == false).Include(x => x.Blogdetail).FirstOrDefault(x => x.Id == id);

            if (blog == null)
                return NotFound();

            return View(blog);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (blog.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo cannot be empty");
                return View();
            }
            if (!blog.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "You must choose only Image");
                return View();
            }
            if (!blog.Photo.IsSizeAllowed(2048))
            {
                ModelState.AddModelError("Photo", "Image size can be 2 MB");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            var blogImg = Path.Combine(Constants.EventImageFolderPath, "blog");
            var fileName = await FilesUtil.GenerateFileAsync(blogImg, blog.Photo);
            blog.Image = fileName;

            var isExist = await _dbContext.Courses.AnyAsync(x => x.IsDelete == false && x.Title.ToLower() == blog.Name.ToLower());

            if (isExist)
            {
                ModelState.AddModelError("Name", "This name is available");
                return View();
            }

            await _dbContext.AddRangeAsync(blog, blog.Blogdetail);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public IActionResult Update(int? id)
        {

            if (id == null)
                return NotFound();

            var blog = _dbContext.Blogs
                .Include(x => x.Blogdetail).Where(y => y.IsDelete == false)
                .FirstOrDefault(x => x.Id == id);
            if (blog == null)
                return NotFound();

            return View(blog);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Blog blog)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null)
                return NotFound();

            if (id != blog.Id)
                return BadRequest();

            var dbBlog = await _dbContext.Blogs
                .Include(x => x.Blogdetail).Where(y => y.IsDelete == false)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dbBlog == null)
                return NotFound();


            var isExist = await _dbContext.Blogs.Where(x => x.IsDelete == false)
                                                .AnyAsync(x => x.Name == blog.Name && x.Id != blog.Id);

            if (isExist)
            {
                ModelState.AddModelError("Name", "This blog name is already Exist!");
                return View();
            }




            if (blog.Image != null)
            {
                var path = Path.Combine(Constants.EventImageFolderPath, "blog", dbBlog.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                var courseImg = Path.Combine(Constants.EventImageFolderPath, "blog");
                var fileName = await FilesUtil.GenerateFileAsync(courseImg, blog.Photo);

                if (blog.Photo == null)
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!blog.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!blog.Photo.IsSizeAllowed(2048))
                {
                    ModelState.AddModelError("Photo", "Max size is 2 MB.");
                    return View();
                }
                blog.Image = fileName;
            }

            dbBlog.Image = blog.Image;
            dbBlog.Name = blog.Name;
            dbBlog.Date = blog.Date;
            dbBlog.Comment = blog.Comment;
            dbBlog.Description = blog.Description;
            dbBlog.Blogdetail.Title = blog.Blogdetail.Title;
            dbBlog.Blogdetail.Subtitle = blog.Blogdetail.Subtitle;


            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var blog = _dbContext.Blogs
                .Include(x => x.Blogdetail).Where(y => y.IsDelete == false)
                .FirstOrDefault(x => x.Id == id);

            if (blog == null)
                return NotFound();

            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteBlog(int? id)
        {
            if (id == null)
                return NotFound();

            var blog = _dbContext.Blogs
                .Include(x => x.Blogdetail).Where(y => y.IsDelete == false)
                .FirstOrDefault(x => x.Id == id);

            if (blog == null)
                return NotFound();

            blog.IsDelete = true;
            blog.Blogdetail.isDelete = true;
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
    }
