using BackendProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewModels
{
    public class EventViewModel
    {
        public List<Blog> Blogs { get; set; }

        public EventDetail EventDetail { get; set; }
        public List<Speaker> Speakers { get; set; }
        public List<Category> Categories { get; set; }
    }
}
