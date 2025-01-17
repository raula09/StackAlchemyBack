using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Models
{
    public class StudentDetails
    {
        public int Id { get; set; }
        public int StudentId { get; set; } 
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Address { get; set; }
        [RegularExpression(@"^\d{9,}$")]
        public string PhoneNumber { get; set; }
        [Required]
        public int CurrentSemester { get; set; }

        public Student Student { get; set; }
    }

}
