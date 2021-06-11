using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class About
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string FirstSubtitle { get; set; }

        [Required]
        public string SecondSubtitle { get; set; }
    }
}
