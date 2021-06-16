using BackendProject.DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
    }
}
