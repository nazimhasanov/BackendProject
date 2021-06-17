using BackendProject.DataAccessLayer;
using BackendProject.Models;
using BackendProject.Utils;
using BackendProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public IActionResult Index()
        {
            var sliders = _dbContext.Sliders.ToList();
            var videoTour = _dbContext.VideoTour.FirstOrDefault();
            var noticeBoard = _dbContext.NoticeBoards.FirstOrDefault();

            var homeViewModel = new HomeViewModel
            {
                Slider = sliders,


            };

            return View(homeViewModel);
        }

        public async Task<IActionResult> Subscribe(string email)
        {
            if (email == null)
            {
                return Content("Email Null ola bilmez");
            }
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                return Content("Please enter correct email");
            }
            var isExist = await _dbContext.Subcribes.AnyAsync(x => x.Email == email);
            if (isExist)
            {
                return Content("Your email is already exist");
            }

            Subcribe subcribe = new Subcribe { Email = email };
            await _dbContext.Subcribes.AddAsync(subcribe);
            await _dbContext.SaveChangesAsync();
            return Content("Done !");


            //List<Subcribe> subcribes = _dbContext.Subcribes.ToList();
            //string subject = "Create event";
            //string url = "https://localhost:44302/Event/Detail/" + 5;
            //string message = $"<a href={url}>yeni event yarandi baxmaq ucun click edin</a>";
            //foreach (Subcribe sub in subcribes)
            //{
            //    await Helper.SendMessage(subject, message, sub.Email);
            //}


        }
        
        public IActionResult Search(string search)
        {
            if (search == null)
                return NotFound();

            var searchViewModel = new SearchViewModel
            {
                Teachers = _dbContext.Teachers.Where(x => x.IsDeleted == false && x.FullName.Contains(search)).Take(4).ToList(),
                Events = _dbContext.Events.Where(x => x.IsDelete == false && x.Title.Contains(search)).Take(4).ToList(),
                Courses = _dbContext.Courses.Where(x => x.IsDelete == false && x.Title.Contains(search)).Take(4).ToList(),
                Blogs = _dbContext.Blogs.Where(x => x.IsDelete == false && x.Name.Contains(search)).Take(4).ToList(),
            };
            return PartialView("_SearchViewPartial", searchViewModel);
        }

       
    }
}
