using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class EventDetail
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; }



        public ICollection<EventDetailSpeaker> EventDetailSpeakers { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
