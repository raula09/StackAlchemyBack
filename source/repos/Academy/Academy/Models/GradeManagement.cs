using Academy.Data;
using System;
using System.Collections.Generic;
using System.Linq;

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
                var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);

                if (student == null || course == null)
                {
                    Console.WriteLine("Student or Course not found.");
                    return;
                }

               
                var grade = new Grade
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    Score = score,
                    Status = "Assigned" 
                };

                
                _context.Grades.Add(grade);
                _context.SaveChanges();

                Console.WriteLine($"Grade {score} assigned to student {student.FirstName} {student.LastName} for course {course.Name}.");
                var fileManagement = new FileManagement(_context); 
                fileManagement.LogAction($"Grade assigned: Student ID: {grade.StudentId}, Course ID: {grade.CourseId}, Score: {grade.Score}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner Stack Trace: {ex.InnerException.StackTrace}");
                }
            }
        }


        public void UpdateGrade(int studentId, int courseId, int newScore)
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("---- Grade Management ----");
                    Console.WriteLine("1. Update Grade");
                    Console.WriteLine("2. View Grade");
                    Console.WriteLine("3. Return to Main Menu");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
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
                            break;

                        case "2":
                            var gradeView = _context.Grades
                                .FirstOrDefault(g => g.StudentId == studentId && g.CourseId == courseId);

                            if (gradeView == null)
                            {
                                Console.WriteLine("Grade not found.");
                            }
                            else
                            {
                                var studentView = _context.Students.FirstOrDefault(s => s.Id == studentId);
                                var courseView = _context.Courses.FirstOrDefault(c => c.Id == courseId);

                                Console.WriteLine($"Student: {studentView.FirstName} {studentView.LastName}");
                                Console.WriteLine($"Course: {courseView.Name}");
                                Console.WriteLine($"Grade: {gradeView.Score}");
                            }
                            break;

                        case "3":
                            return;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
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
                    Console.WriteLine("No student or grades found.");
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
                        Console.WriteLine("Student not found for the grade.\n");
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
