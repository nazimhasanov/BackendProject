using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class CourseDetail
    {
        public int Id { get; set; }
        public string About { get; set; }
        public string Apply { get; set; }
        public string Certification { get; set; }
        public string Reply { get; set; }
        public DateTime CourseStart { get; set; }
        public int CourseDuration { get; set; }
        public string CourseClassDuration { get; set; }
        public string CourseSkillLevel { get; set; }
        public string CourseLanguage { get; set; }
        public int CourseStudent { get; set; }
        public int CourseAssesment { get; set; }
        public double CourseFee { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Courses { get; set; }



    }
}
