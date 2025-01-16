using Academy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Academy.Models
{
    public class GradeManagement
    {
        private readonly AcademyDbContext _context;
      
        public GradeManagement(AcademyDbContext context)
        {
            _context = context;
        }
        public void AssignGrade(int studentId, int courseId, int score)
        {
            try
            {
                var student = _context.Students.FirstOrDefault(s => s.Id == studentId);
                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    return;
                }


                var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);
                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return;
                }

                Grade grade = new Grade()
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    Score = score,
                    Date = DateTime.Now,
                    Status = score >= 50 ? "Completed" : "Active",
                    Student = student,
                    Course = course
                };


                _context.Grades.Add(grade);
                _context.SaveChanges();

                Console.WriteLine($"student {student.FirstName} in course {course.Name} assigned: {score}. ");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"error: {ex.Message}");
            }
         
        }
        public void UpdateGrade(int studentId, int courseId, int newScore)
        {
            try
            {
                var student = _context.Students.FirstOrDefault(s => s.Id == studentId);
                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    return;
                }

                var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);
                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return;
                }

                
                var grade = _context.Grades.FirstOrDefault(g => g.StudentId == studentId && g.CourseId == courseId);
                if (grade == null)
                {
                    Console.WriteLine("Grade not found for student or course");
                    return;
                }

              
                grade.Score = newScore;
                grade.Date = DateTime.Now;
                grade.Status = newScore >= 50 ? "Completed" : "Active";

               
                _context.SaveChanges();

                Console.WriteLine($"Student {student.FirstName} in course {course.Name} has been assigned a new score: {newScore}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}
