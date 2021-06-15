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
        public DbSet<About> About { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogDetail> BlogDetails { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<SocialMedia> SocialMedia { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherDetail> TeacherDetails { get; set; }
        public DbSet<Testimonial> Testimonial { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventDetail> EventDetail { get; set; } 
        public DbSet<Speaker> Speakers { get; set; } 
        public DbSet<EventDetailSpeaker> EventDetailSpeakers { get; set; }
        public DbSet<BannerArea> BannerAreas { get; set; }
        public DbSet<Subcribe> Subcribes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Bio> Bios { get; set; }












    }
}
