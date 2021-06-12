using BackendProject.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ContactController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var contact = _dbContext.Contact.FirstOrDefault();
            return View(contact);
        }

    }
}
