using BackendProject.Areas.AdminPanel.Utils;
using BackendProject.DataAccessLayer;
using BackendProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    //[Authorize(Roles = RoleConstant.Admin)]
    public class SpeakerController : Controller
    {
        private readonly AppDbContext _dbContext;

        public SpeakerController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.PageCount = Math.Ceiling((decimal)_dbContext.Speakers.Where(s => s.IsDelete == false).Count() / 4);
            ViewBag.Page = page;
            
            if (ViewBag.PageCount < ViewBag.Page || page <= 0)
            {
                return BadRequest();
            }

            var speakers = _dbContext.Speakers.Where(x => x.IsDelete == false).OrderByDescending(x => x.Id).Skip(((int)page - 1) * 4).Take(4).ToList();

            return View();
        }
        public IActionResult Create()
        {
            var eventDetails = _dbContext.EventDetail.Where(x => x.IsDelete == false).Include(x => x.Event).ToList();
            ViewBag.EventDetails = eventDetails;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Speaker speaker, List<int> eventDetailsId)
        {
            var eventDetails = _dbContext.EventDetail.Where(x => x.IsDelete == false).Include(x => x.Event).ToList();
            ViewBag.EventDetails = eventDetails;

            if (speaker.Photo == null)
            {
                ModelState.AddModelError("Photo", "Photo cannot be empty");
                return View();
            }
            if (!speaker.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "You must choose only Image");
                return View();
            }
            if (!speaker.Photo.IsSizeAllowed(2048))
            {
                ModelState.AddModelError("Photo", "Image size upper 2 MB");
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in eventDetailsId)
            {
                if (eventDetails.All(x => x.Id != item))
                {
                    return NotFound();
                }
            }

            var speakerImg = Path.Combine(Constants.ImageFolderPath, "speaker");
            var fileName = await FilesUtil.GenerateFileAsync(speakerImg, speaker.Photo);
            speaker.Image = fileName;

            var eventDetailSpeakerList = new List<EventDetailSpeaker>();
            foreach (int item in eventDetailsId)
            {

                var eventDetailSpeaker = new EventDetailSpeaker
                {
                    EventDetailId = item,
                    SpeakerId = speaker.Id

                };
                eventDetailSpeakerList.Add(eventDetailSpeaker);
            }
            speaker.EventDetailSpeakers = eventDetailSpeakerList;
            speaker.IsDelete = false;

            await _dbContext.Speakers.AddAsync(speaker);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {

            var eventDetails = _dbContext.EventDetail.Where(x => x.IsDelete == false).Include(x => x.Event).ToList();
            ViewBag.EventDetails = eventDetails;

            if (id == null)
                return NotFound();

            var speaker = await _dbContext.Speakers.Where(x => x.IsDelete == false).Include(x => x.EventDetailSpeakers)
                                           .ThenInclude(x => x.EventDetail).ThenInclude(x => x.Event)
                                           .FirstOrDefaultAsync(x => x.Id == id);

            if (speaker == null)
                return NotFound();


            return View(speaker);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id, Speaker speaker, List<int> eventDetailsId)
        {
            var eventDetails = _dbContext.EventDetail.Where(x => x.IsDelete == false).Include(x => x.Event).ToList();
            ViewBag.EventDetails = eventDetails;

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null)
                return NotFound();

            if (id != speaker.Id)
                return BadRequest();

            var dbSpeaker = await _dbContext.Speakers.Where(x => x.IsDelete == false).Include(x => x.EventDetailSpeakers)
                                           .ThenInclude(x => x.EventDetail).ThenInclude(x => x.Event)
                                           .FirstOrDefaultAsync(x => x.Id == id);

            if (dbSpeaker == null)
                return NotFound();

            if (speaker.Photo != null)
            {
                var path = Path.Combine(Constants.ImageFolderPath, "event", dbSpeaker.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                var speakerImg = Path.Combine(Constants.ImageFolderPath, "event");
                var fileName = await FilesUtil.GenerateFileAsync(speakerImg, speaker.Photo);

                if (speaker.Photo == null)
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!speaker.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Select photo.");
                    return View();
                }

                if (!speaker.Photo.IsSizeAllowed(2048))
                {
                    ModelState.AddModelError("Photo", "Max size is 2 MB.");
                    return View();
                }
                dbSpeaker.Image = fileName;
            }

            dbSpeaker.FullName = speaker.FullName;

            var eventDetailSpeakerList = new List<EventDetailSpeaker>();
            foreach (int item in eventDetailsId)
            {

                var eventDetailSpeaker = new EventDetailSpeaker
                {
                    EventDetailId = item,
                    SpeakerId = speaker.Id

                };
                eventDetailSpeakerList.Add(eventDetailSpeaker);
            }
            dbSpeaker.EventDetailSpeakers = eventDetailSpeakerList;


            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
