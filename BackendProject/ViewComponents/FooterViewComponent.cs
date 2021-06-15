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
    public class FooterViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public FooterViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var bio = await _dbContext.Bios.FirstOrDefaultAsync();
            var contact = await _dbContext.Contact.FirstOrDefaultAsync();

            var layoutViewModel = new LayoutViewModel
            {
                Bio = bio,
            };
            return View(layoutViewModel);
        }
    }
}
