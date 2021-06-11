using BackendProject.DataAccessLayer;
using BackendProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewComponents
{
    public class TeacherViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public TeacherViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? take, int skip)
        {

            if (take == null)
            {
                List<Teacher> teachers = await _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(y => y.SocialMedias)
                                                                       .Include(z => z.Position).ToListAsync();
                return View(teachers);
            }
            else
            {
                List<Teacher> teachers = await _dbContext.Teachers.Where(x => x.IsDeleted == false).Include(y => y.SocialMedias)
                                                        .Include(z => z.Position).Skip((skip - 1) * 6).Take((int)take).ToListAsync();
                return View(teachers);
            }

        }
    }
}
