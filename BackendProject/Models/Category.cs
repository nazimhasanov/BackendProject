﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<BlogCategory> BlogCategories { get; set; }
        public ICollection<CourseCategory> CourseCategories { get; set; }
        public ICollection<EventCategory> EventCategories { get; set; }
    }
}
