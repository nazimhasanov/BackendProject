using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Image { get; set; }

        [Required]
        public string Title { get; set; }

        public bool IsDelete { get; set; }

        public string Subtitle { get; set; }
        public CourseDetail CourseDetails { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

    }
}
