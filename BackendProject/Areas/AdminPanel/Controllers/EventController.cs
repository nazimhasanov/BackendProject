using BackendProject.DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = RoleConstant.Admin)]
    public class EventController : Controller
    {
        private readonly AppDbContext _dbContext;

        public EventController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int page = 1)
        {
            ViewBag.PageCount = Decimal.Ceiling((decimal)_dbContext.Events.Where(s => s.IsDelete == false).Count() / 4);
            ViewBag.Page = page;

            if (ViewBag.PageCount < page)
            {
                return NotFound();
            }

            var events = _dbContext.Events.Where(x => x.IsDelete == false).OrderByDescending(y => y.Id).Include(x => x.EventDetail)
                                          .ThenInclude(x => x.EventDetailSpeakers).ThenInclude(x => x.Speaker).Skip(((int)page - 1) * 4)
                                          .Take(4).ToList();

            return View(events);
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var eventDetail = _dbContext.Events.Where(x => x.IsDelete == false).OrderByDescending(y => y.Id).Include(x => x.EventDetail)
                                               .ThenInclude(x => x.EventDetailSpeakers).ThenInclude(x => x.Speaker).FirstOrDefault(z => z.Id == id);

            if (eventDetail == null)
                return NotFound();

            return View(eventDetail);
        }
    }
}
