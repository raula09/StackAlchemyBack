using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        [Range(0, 100)]
        public int Score { get; set; }
        public DateTime Date { get; set; }

        public string Status { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
