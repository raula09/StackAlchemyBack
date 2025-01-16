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

            string personalNumber = GeneratePersonalNumber();

            Student student = new Student()
            {
                FirstName = firstName,
                LastName = lastName,
                PersonalNumber = personalNumber,
                DateOfEnrollment = DateTime.Now
            };

            _context.Students.Add(student);
            _context.SaveChanges();
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
        public void UpdateStudent(int studentId, string newFirstName, string newLastName)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == studentId);

            if (student != null)
            {
                student.FirstName = newFirstName;
                student.LastName = newLastName;

                _context.SaveChanges();
            }
            else
            {

                Console.WriteLine("Student not found");
            }
        }
        public void DeleteStudent(int studentId)
        {
            try
            {
                Console.WriteLine("enter student id to remove");
                var student = _context.Students.FirstOrDefault(s => s.Id == studentId);
                if (student != null)
                {
                    Console.WriteLine("student could not be found");
                    return;
                }
                _context.Students.Remove(student);
                _context.SaveChanges();
                Console.WriteLine($"{student.FirstName} {student.Id} deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error {ex.Message}");
            }

        }
        public void ViewAllStudents()
        {
            try
            {
                var students = _context.Students
                                       .Include(s => s.FirstName)
                                       .ToList();
                if (students != null)
                {
                    foreach (var student in students)
                    {
                        Console.WriteLine($"Id:{student.Id} \n First Name: {student.FirstName} \n Last Name: {student.LastName} \n Personal Number: {student.PersonalNumber} \n Enrollement date: {student.DateOfEnrollment}");
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

