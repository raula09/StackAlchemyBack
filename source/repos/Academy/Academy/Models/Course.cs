using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Range(0, 15)]
        public int Credits { get; set; }
        public int Semester {  get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
