using BackendProject.Areas.AdminPanel.Utils;
using BackendProject.DataAccessLayer;
using BackendProject.Models;
using BackendProject.Utils;
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
        
        public IActionResult Create()
        {
            ViewBag.Speakers = _dbContext.Speakers.Where(x => x.IsDelete == false).ToList();
            ViewBag.Categories = _dbContext.Categories.Where(x => x.IsDeleted == false).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event events, List<int?> SpeakersId, EventDetail eventDetail, List<int?> CategoriesId)
        {
            ViewBag.Speakers = _dbContext.Speakers.Where(x => x.IsDelete == false).ToList();
            ViewBag.Categories = _dbContext.Categories.Where(x => x.IsDeleted == false).ToList();

            if (!ModelState.IsValid)
            {
                return View();
            }           
            if (SpeakersId.Count == 0)
            {
                ModelState.AddModelError("", "Please Select Speaker");
                return View();
            }

            if (CategoriesId.Count == 0)
            {
                ModelState.AddModelError("", "Please Select Category");
                return View();
            }
            if (eventDetail.Id == 0)
            {

                ModelState.AddModelError("", "Please ....");
                return View();
            }

            if (events.Photo == null)
            {
                ModelState.AddModelError("Photo", "Please Select Photo");
                return View();
            }

            if (!events.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Not the image you uploaded");
                return View();
            }

            if (!events.Photo.IsSizeAllowed(1024))
            {
                ModelState.AddModelError("Photo", "The size of the uploaded image is high");
                return View();
            }

            var fileName = await FilesUtil.GenerateFileAsync(Constants.EventImageFolderPath, events.Photo);
            events.Image = fileName;
            events.IsDelete = false;

            var eventSpeakers = new List<EventDetailSpeaker>();

            await _dbContext.Events.AddAsync(events);
            events.EventDetail.EventId = events.Id;
            foreach (int eventspeaker in SpeakersId)
            {
                var eventSpeaker = new EventDetailSpeaker();
                eventSpeaker.EventDetail.Id = events.Id;
                eventSpeaker.Speaker.Id = eventspeaker;
                eventSpeakers.Add(eventSpeaker);
            }

            var eventCategories = new List<EventCategory>();

            foreach (var ec in CategoriesId)
            {
                var eventCategory = new EventCategory();
                eventCategory.EventId = events.Id;
                eventCategory.CategoryId = (int)ec;
                eventCategories.Add(eventCategory);
            }

            events.EventDetail = eventDetail;
            events.EventCategories = eventCategories;
            await _dbContext.EventDetail.AddAsync(events.EventDetail);
            await _dbContext.SaveChangesAsync();

            string href = "https://localhost:44302/Event/Detail/" + events.Id;
            string subject = "New Event Created";
            string msgBody = $"<a href={href}>New Event Created for see you click here</a> ";

            foreach (var item in (await _dbContext.Subcribes.ToListAsync()))
            {
                await Helper.SendMessage(subject, msgBody, item.Email);
            }

            return RedirectToAction("Index");
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

        public IActionResult Update(int? id)
        {
            ViewBag.Speakers = _dbContext.Speakers.Where(x => x.IsDelete == false).ToList();
            ViewBag.Categories = _dbContext.Categories.Where(x => x.IsDeleted == false).ToList();

            if (id == null)
                return NotFound();

            var dbEvent = _dbContext.Events.Where(x => x.IsDelete == false).Include(x => x.EventDetail).ThenInclude(x => x.EventDetailSpeakers).ThenInclude(x => x.Speaker)
                                                .Include(y => y.EventCategories).ThenInclude(y => y.Category).FirstOrDefault(x => x.Id == id);

            if (dbEvent == null)
                return NotFound();

            return View(dbEvent);

        }
    }
  
}
