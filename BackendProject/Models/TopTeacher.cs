using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class TopTeacher
    {
        public int Id { get; set; }

        [Required]
        public string Teacher { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
