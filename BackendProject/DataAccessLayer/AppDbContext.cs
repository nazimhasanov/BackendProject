using BackendProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseDetail> CourseDetails { get; set; }
        public DbSet<VideoTour> VideoTour { get; set; }
        public DbSet<NoticeBoard> NoticeBoards { get; set; }

    }
}
