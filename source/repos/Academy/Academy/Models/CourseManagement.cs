using Academy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AddCourse(string courseName, int credits, int semester)
        {
            try
            {
                var course = new Course
                {
                    Name = courseName,
                    Credits = credits,
                    Semester = semester,
                    DateOfCreation = DateTime.Now
                };

                _context.Courses.Add(course);
                _context.SaveChanges();

                Console.WriteLine($"Course {courseName} added successfully.");

               
                var fileManagement = new FileManagement(_context);
                fileManagement.LogAction($"New course added: {course.Name}, Course ID: {course.Id}, Credits: {course.Credits}, Semester: {course.Semester}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding course: {ex.Message}");

              
                var fileManagement = new FileManagement(_context); 
                fileManagement.LogAction($"Error adding course {courseName}: {ex.Message}");
            }
        }


        public void UpdateCourse(int courseId, string newName, int newCredits, int newSemester)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found");
                return;
            }

            Console.WriteLine($"Updating details for {course.Name} {course.Credits}  {course.Semester} ID: {course.Id}");
            Console.WriteLine("1. Update Name");
            Console.WriteLine("2. Update Credits");
            Console.WriteLine("3. Update Semester");
            Console.WriteLine("4. Exit");
            Console.Write("Select an option to update: ");
            string updateChoice = Console.ReadLine();

            switch (updateChoice)
            {
                case "1":
                    Console.Write("Enter new name: ");
                    course.Name = Console.ReadLine();
                    break;
                case "2":
                    Console.Write("Enter new credits: ");
                    course.Credits = int.Parse(Console.ReadLine());
                    break;
                case "3":
                    Console.Write("Enter new Semester: ");
                    course.Semester = int.Parse(Console.ReadLine());
                    break;

                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option selected.");
                    break;
            }

            _context.SaveChanges();
            Console.WriteLine("Course details updated successfully.");
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
                
                var courses = _context.Courses.ToList();

                if (courses != null && courses.Any())
                {
                    foreach (var course in courses)
                    {
                        Console.WriteLine($"Id: {course.Id} \n Name: {course.Name} \n Max Credits: {course.Credits} \n Semester: {course.Semester} \n Creation Date: {course.DateOfCreation}");
                        Console.WriteLine("_______________________________________________");
                    }
                }
                else
                {
                    Console.WriteLine("No courses found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
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
