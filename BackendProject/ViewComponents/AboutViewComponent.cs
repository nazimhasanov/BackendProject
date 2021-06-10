using BackendProject.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewComponents
{
        public class AboutViewComponent : ViewComponent
        {
            private readonly AppDbContext _dbContext;

            public AboutViewComponent(AppDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<IViewComponentResult> InvokeAsync()
            {
                var about = await _dbContext.About.FirstOrDefaultAsync();

                return View(about);
            }
        }
    
}
