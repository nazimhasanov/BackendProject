using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class CourseDetail
    {
        public string About { get; set; }
        public string Apply { get; set; }
        public string Certification { get; set; }
        public string Reply { get; set; }
        public string CourseStart { get; set; }
        public int CourseDuration { get; set; }
        public int CourseClassDuration { get; set; }
        public string CourseSkillLevel { get; set; }
        public string CourseLanguage { get; set; }
        public string CourseStudent { get; set; }
        public int CourseAssesment { get; set; }
        public int CourseFee { get; set; }
        public int CourseId { get; set; }
        public Course Courses { get; set; }



    }
}
