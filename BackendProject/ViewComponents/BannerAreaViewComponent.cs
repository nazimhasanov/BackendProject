using BackendProject.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewComponents
{
    public class BannerAreaViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;
        public BannerAreaViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<IViewComponentResult> InvokeAsync(string title, string key)
        {
            ViewBag.BannerArea = title;
            ViewBag.Key = key;

            var bannerArea = await _dbContext.BannerAreas.FirstOrDefaultAsync();
            return View(bannerArea);
        }
    }
}
