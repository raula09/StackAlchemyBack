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
            if (grades == null)
            {
                Console.WriteLine("No grades available.");
                return;
            }

            var studentsWithGrades = grades.GroupBy(g => g.StudentId)
                                             .Where(g => g.Any()) 
                                             .ToList();

            foreach (var group in studentsWithGrades)
            {
             
                var student = _context.Students.FirstOrDefault(i => i.Id == group.Key);
                var studentGrades = group.ToList(); 

                if (studentGrades.Any())
                {
                    double totalGPA = 0;
                    int totalCredits = 0;

                    foreach (var grade in studentGrades)
                    {
                        totalGPA += (grade.Score / 100.0) * 4 * grade.Course.Credits;  
                        totalCredits += grade.Course.Credits;
                    }

                    double finalGPA = totalCredits > 0 ? totalGPA / totalCredits : 0;
                    Console.WriteLine($"{student.FirstName} {student.LastName} ID: {student.Id} - GPA: {finalGPA:F2}");
                }
                else
                {
                    Console.WriteLine($"{student.FirstName} {student.LastName} ID: {student.Id} has not taken any classes.");
                }
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
        public void GetStudentsWithNoGrade()
        {
            try
            {
                // Get students who do not have any grades assigned
                var studentsWithNoGrade = _context.Students
                                                  .Where(s => !_context.Grades.Any(g => g.StudentId == s.Id))
                                                  .ToList();

                if (!studentsWithNoGrade.Any())
                {
                    Console.WriteLine("All students have received grades.");
                    return;
                }

                // Display students who haven't received any grades
                foreach (var student in studentsWithNoGrade)
                {
                    Console.WriteLine($"Student {student.FirstName} {student.LastName} (ID: {student.Id}) has not received a grade.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}
