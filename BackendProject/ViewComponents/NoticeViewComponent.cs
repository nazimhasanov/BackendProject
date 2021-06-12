using BackendProject.DataAccessLayer;
using BackendProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewComponents
{
    public class NoticeViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public NoticeViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var noticeBoards = await _dbContext.NoticeBoards.Take(6).ToListAsync();
            var videoTour = await _dbContext.VideoTour.FirstOrDefaultAsync();

            var noticeViewModel = new NoticeViewModel
            {
                NoticeBoards = noticeBoards,
                VideoTour = videoTour
            };

            return View(noticeViewModel);
        }
    }
}
