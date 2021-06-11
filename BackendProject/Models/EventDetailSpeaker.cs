using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class EventDetailSpeaker
    {
        public int Id { get; set; }

        public EventDetail EventDetail { get; set; }
        public int EventDetailId { get; set; }

        public Speaker Speaker { get; set; }
        public int SpeakerId { get; set; }

    }
}
