using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public string Title { get; set; }
        public string Venue { get; set; }
        public bool IsDelete { get; set; }


        public EventDetail EventDetail { get; set; }

    }

}
