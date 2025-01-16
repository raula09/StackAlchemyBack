using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Models
{
    public class Student
    {
        [Required]
        
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }


        [Required]
        [StringLength(11)]
        public string PersonalNumber { get; set; }

        public DateTime DateOfEnrollment { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
