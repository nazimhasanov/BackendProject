using BackendProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewModels
{
    public class TeacherViewModel
    {
        public List<Blog> Blogs { get; set; }
        public List<SocialMedia> SocialMedias { get; set; }
        public TeacherDetail TeacherDetail { get; set; }

    }
}
