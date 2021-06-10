using BackendProject.DataAccessLayer;
using BackendProject.Models;
using BackendProject.ViewModels;
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
            var videoTour = _dbContext.VideoTour.FirstOrDefault();
            var noticeBoard = _dbContext.NoticeBoards.FirstOrDefault();

            var homeViewModel = new HomeViewModel
            {
                Slider = sliders,
                

            };

            return View(homeViewModel);
        }

      
    }
}
