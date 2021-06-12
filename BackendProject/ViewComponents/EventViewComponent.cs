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
    public class EventViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public EventViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? take, int skip)
        {

            if (take == null)
            {
                List<Event> events = await _dbContext.Events.Where(x => x.IsDelete == false).Include(y => y.EventDetail)
                                                                 .ThenInclude(z => z.EventDetailSpeakers).ThenInclude(z=>z.Speaker).ToListAsync();
                return View(events);
            }
            else
            {
                List<Event> events = await _dbContext.Events.Where(x => x.IsDelete == false).Include(y => y.EventDetail)
                                                        .ThenInclude(z => z.EventDetailSpeakers).ThenInclude(z => z.Speaker).Take(6).ToListAsync();
                return View(events);
            }

        }

    }
}
