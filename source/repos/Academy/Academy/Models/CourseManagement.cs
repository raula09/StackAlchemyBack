using Academy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Academy.Models
{
    public class CourseManagement
    {
        private readonly AcademyDbContext _context;
        private static Random _random = new Random();
        public CourseManagement(AcademyDbContext context)
        {
            _context = context;
        }

        public void AddCourse(string name, int credits, int semester)
        {


            Course course = new Course()
            {
                Name = name,
                Credits = credits,
                Semester = semester,
                DateOfCreation = DateTime.Now
            };

            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public void UpdateCourse(int courseId, string newName, int newCredits, int newSemester)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course != null)
            {
                course.Name = newName;
                course.Credits = newCredits;
                course.Semester = newSemester;

                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Course not found");
            }
        }

        public void DeleteCourse(int courseId)
        {
            try
            {
                Console.WriteLine("Enter course id to remove");
                var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);

                if (course == null)
                {
                    Console.WriteLine("Course could not be found");
                    return;
                }

                _context.Courses.Remove(course);
                _context.SaveChanges();
                Console.WriteLine($"Course {course.Name} ID: {course.Id} deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void ViewAllCourses()
        {
            try
            {
                var courses = _context.Courses
                                       .Include(s => s.Name)
                                       .ToList();
                if (courses != null)
                {
                    foreach (var course in courses)
                    {
                        Console.WriteLine($"Id:{course.Id} \n Name: {course.Name} \n max Credits: {course.Credits} \n Semester: {course.Semester} \n Creation date: {course.DateOfCreation}");
                        Console.WriteLine("_______________________________________________");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error:{ex.Message}");
            }

        }
        public void EnrollStudentInCourse(int studentId, int courseId)
        {
            
                   var student = _context.Students.Find(studentId);
            var course = _context.Courses.Find(courseId);

            if (student == null || course == null)
            {
                throw new Exception("Student or Course not found");
            }

            var studentCourse = new StudentCourse
            {
                StudentId = studentId,
                CourseId = courseId
            };


            _context.StudentCourses.Add(studentCourse);
            _context.SaveChanges();
            
           
           
        }
    }
}
