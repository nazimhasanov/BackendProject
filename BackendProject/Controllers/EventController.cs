using BackendProject.DataAccessLayer;
using BackendProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _dbContext;

        public EventController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            
            var events = _dbContext.Events.Where(c => c.IsDelete == false).ToList();

            return View(events);
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var eventDetail = _dbContext.EventDetail.Where(x => x.IsDelete == false).Include(x => x.Event).ThenInclude(y => y.EventDetail)
                                                    .Include(t => t.EventDetailSpeakers).ThenInclude(t => t.Speaker)
                                                    .OrderByDescending(t => t.Id).FirstOrDefault(z => z.EventId == id);


            var eventViewModel = new EventViewModel
            {
                Speakers = _dbContext.Speakers.Where(x => x.IsDelete == false).Take(3).ToList(),
                EventDetail = eventDetail,
                Blogs = _dbContext.Blogs.Where(x => x.IsDelete == false).Take(3).ToList()
            };

            if (eventDetail == null)
                return NotFound();

            return View(eventViewModel);
        }

        public IActionResult Search(string search)
        {
            if (search == null)
                return NotFound();

            var events = _dbContext.Events.Where(p => p.Title.Contains(search) && p.IsDelete == false)
                                                            .Take(8).OrderByDescending(p => p.Id).ToList();
            return PartialView("_EventSearchPartial", events);
        }
        
       

       
    }
}
