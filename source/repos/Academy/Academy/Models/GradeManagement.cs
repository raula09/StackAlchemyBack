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
        public void AssignGrade(int studentId, int courseId, double score)
        {
            try
            {
                var grade = _context.Grades
                                    .FirstOrDefault(g => g.StudentId == studentId && g.CourseId == courseId);

                if (grade == null)
                {
                    
                    grade = new Grade
                    {
                        StudentId = studentId,
                        CourseId = courseId,
                        Score = score
                    };

                    _context.Grades.Add(grade);
                }
                else
                {
              
                    grade.Score = score;
                }

                _context.SaveChanges();

               
                var logManager = new LogManager();
                logManager.LogAction($"Score updated for student ID {studentId} in course ID {courseId}. New score: {score}");

                Console.WriteLine($"Score for student {studentId} in course {courseId} updated to {score}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
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
        public void GetStudentGrade(int studentId, int courseId)
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
                    Console.WriteLine("Grade not found for the specified student and course.");
                    return;
                }

                Console.WriteLine("Student Details:");
                Console.WriteLine($"Name: {student.FirstName} {student.LastName}");
                Console.WriteLine($"Personal Number: {student.PersonalNumber}");

                Console.WriteLine("\nCourse Details:");
                Console.WriteLine($"Course Name: {course.Name}");
                Console.WriteLine($"Grade: {grade.Score} ({grade.Status})");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public void GetCourseGrades(int courseId)
        {
            try
            {
              
                var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);
                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return;
                }

               
                var grades = _context.Grades.Where(g => g.CourseId == courseId).ToList();
                if (!grades.Any())
                {
                    Console.WriteLine("no student or grades found");
                    return;
                }

                
                Console.WriteLine($"Course Name: {course.Name}\n");

                
                Console.WriteLine("Student Grades:");
                foreach (var grade in grades)
                {
                    
                    var student = _context.Students.FirstOrDefault(s => s.Id == grade.StudentId);
                    if (student != null)
                    {
                        Console.WriteLine($"Name: {student.FirstName} {student.LastName}");
                        Console.WriteLine($"Personal Number: {student.PersonalNumber}");
                        Console.WriteLine($"Grade: {grade.Score} ({grade.Status})\n");
                    }
                    else
                    {
                        Console.WriteLine("Student not found for the grade .\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }




    }
}
