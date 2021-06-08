using BackendProject.DataAccessLayer;
using BackendProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            var courses = _dbContext.Courses.ToList();
            var coursesDetails = _dbContext.CourseDetails.FirstOrDefault();
            var videoTour = _dbContext.VideoTour.FirstOrDefault();
            var notiiceBoard = _dbContext.NoticeBoards.FirstOrDefault();


            return View();
        }

      
    }
}
