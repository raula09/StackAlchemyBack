using Academy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Models
{
    public class AnalyticsManagement
    {
        private readonly AcademyDbContext _context;

        public AnalyticsManagement(AcademyDbContext context)
        {
            _context = context;
        }
        public void DisplayStudentsGPA(IEnumerable<Grade> grades)
        {
            if (grades == null || !grades.Any())
            {
                Console.WriteLine("No grades available.");
                return;
            }

            foreach (var grade in grades)
            {

                double gpa = (grade.Score / 100.0) * 4;


                var student = grade.Student;
                Console.WriteLine($"{student.FirstName} {student.LastName} ID: {student.Id} - GPA: {gpa:F2}");
            }
        }
        public void AssignGrade(int courseId)
        {
            try
            {
                var courseGrades = _context.Grades
                                     .Where(g => g.CourseId == courseId)
                                     .OrderByDescending(g => g.Score)
                                     .ToList();

                if (courseGrades.Count == null)
                
                {
                    Console.WriteLine("no students found for this course");
                    return;
                }
                Console.WriteLine($"top students for course {courseGrades[0].Course.Name}");
                foreach (var grade in courseGrades) 
                {
                    var student = _context.Students.FirstOrDefault(s => s.Id == grade.StudentId);
                    if (student != null)

                        Console.WriteLine($"Student: {student.FirstName} {student.LastName} Id:{student.Id} Score: {grade.Score}");
                    }
                
            }
            catch (Exception ex) 
            
            {
                Console.WriteLine($"error: {ex.Message}");
            }




        }
    }
}
