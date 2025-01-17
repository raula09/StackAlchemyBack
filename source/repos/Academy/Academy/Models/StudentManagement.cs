using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Academy.Data;

namespace Academy.Models
{
    public class StudentManagement
    {
        private readonly AcademyDbContext _context;
        private static Random _random = new Random();
        public StudentManagement(AcademyDbContext context)
        {
            _context = context;
        }
        public void AddStudent(string firstName, string lastName)
        {
            try
            {
               
                string personalNumber = GeneratePersonalNumber();

               
                Student student = new Student()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfEnrollment = DateTime.Now,
                    PersonalNumber = personalNumber
                };

            
                _context.Students.Add(student);
                _context.SaveChanges();
                FileManagement _fileManagement = new FileManagement(_context);


                _fileManagement.LogAction($"Added new student: {firstName} {lastName} (ID: {student.Id})");

                Console.WriteLine($"Student {firstName} {lastName} added successfully with ID: {student.Id}");

                
                int courseCount = 0;

                while (courseCount < 4)
                {
                    Console.WriteLine("Please choose an option:");
                    Console.WriteLine("1. Add a course");
                    Console.WriteLine("2. Exit to main panel");
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                           
                            Console.WriteLine("Enter Course ID:");
                            int courseId = int.Parse(Console.ReadLine());

                            var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);
                            if (course == null)
                            {
                                Console.WriteLine("Course not found.");
                            }
                            else
                            {
                           
                                var studentCourse = new StudentCourse
                                {
                                    StudentId = student.Id,
                                    CourseId = courseId
                                };

                                _context.StudentCourses.Add(studentCourse);
                                _context.SaveChanges();
                                Console.WriteLine($"Course {course.Name} added to student {student.FirstName} {student.LastName}.");

                                courseCount++; 
                            }
                            break;

                        case "2":
                           
                            Console.WriteLine("Exiting to the main panel.");
                            return;

                        default:
                            Console.WriteLine("Invalid option, please try again.");
                            break;
                    }
                }

             
                _fileManagement.StudentTranscript(student.Id);
            }
            catch (Exception ex)
            {
                FileManagement _fileManagement = new FileManagement(_context);

                Console.WriteLine($"Error adding student: {ex.Message}");
                _fileManagement.LogAction($"Error adding student: {ex.Message}");
            }
        }




        private string GeneratePersonalNumber()
        {
            Random random = new Random();
            string r = "";


            for (int i = 0; i < 11; i++)
            {
                r += random.Next(0, 10).ToString();
            }

            return r;
        }
        public void UpdateStudent(int studentId, string firstName, string lastName)
        {
            try
            {
                FileManagement _fileManagement = new FileManagement(_context);
                var student = _context.Students.FirstOrDefault(s => s.Id == studentId);

                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    _fileManagement.LogAction($"Student with ID {studentId} not found for update.");
                    return;
                }

             
                student.FirstName = firstName;
                student.LastName = lastName;

               
                _context.SaveChanges();

                Console.WriteLine($"Student ID: {studentId} updated successfully.");
                _fileManagement.LogAction($"Updated student: {firstName} {lastName} (ID: {studentId})");
            }
            catch (Exception ex)
            {
                FileManagement _fileManagement = new FileManagement(_context);
                Console.WriteLine($"Error updating student: {ex.Message}");
                _fileManagement.LogAction($"Error updating student with ID {studentId}: {ex.Message}");
            }
        }

      
        public void DeleteStudent(int studentId)
        {
            try
            {
                var student = _context.Students.FirstOrDefault(s => s.Id == studentId);
                FileManagement _fileManagement = new FileManagement(_context);
                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    _fileManagement.LogAction($"Student with ID {studentId} not found for deletion.");
                    return;
                }

                
                _context.Students.Remove(student);
                _context.SaveChanges();

                Console.WriteLine($"Student ID: {studentId} deleted successfully.");
                _fileManagement.LogAction($"Deleted student with ID {studentId}");
            }
            catch (Exception ex)
            {
                FileManagement _fileManagement = new FileManagement(_context);
                Console.WriteLine($"Error deleting student: {ex.Message}");
                _fileManagement.LogAction($"Error deleting student with ID {studentId}: {ex.Message}");
            }
        }
    }

}


