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


    }
}
