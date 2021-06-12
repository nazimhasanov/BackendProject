using BackendProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewModels
{
    public class EventViewModel
    {
        public List<Event> Events { get; set; }

        public EventDetail EventDetail { get; set; }
    }
}
