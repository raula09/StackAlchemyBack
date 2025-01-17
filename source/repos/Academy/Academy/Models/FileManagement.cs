using Academy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
namespace Academy.Models
{
    public class FileManagement
    {
        private readonly AcademyDbContext _context;

        public FileManagement(AcademyDbContext context)
        {
            _context = context;
        }



        public void GenerateStudentTranscript(int studentId)
        {
            try
            {
                var student = _context.Students
                                      .FirstOrDefault(s => s.Id == studentId);

                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    return;
                }

                var studentCourses = _context.StudentCourses
                                             .Where(sc => sc.StudentId == studentId)
                                             .ToList();

                if (!studentCourses.Any())
                {
                    Console.WriteLine($"No courses found for student {student.FirstName} {student.LastName}.");
                    return;
                }

               
                string directoryPath = "StudentLog";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string fileName = Path.Combine(directoryPath, $"student_{student.Id}_transcript.txt");

                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.WriteLine($"Transcript for {student.FirstName} {student.LastName} (ID: {student.Id})");
                    writer.WriteLine(new string('-', 50));

                    foreach (var studentCourse in studentCourses)
                    {
                        var course = _context.Courses
                                             .FirstOrDefault(c => c.Id == studentCourse.CourseId);

                        if (course != null)
                        {
                            var grade = _context.Grades
                                                .FirstOrDefault(g => g.StudentId == studentId && g.CourseId == course.Id);

                          
                            writer.WriteLine($"Course: {course.Name} - Grade: {(grade != null ? grade.Score.ToString() : "Not Graded")}");
                            writer.WriteLine(new string('-', 50));
                        }
                    }
                }

                Console.WriteLine($"Transcript generated for {student.FirstName} {student.LastName} and saved to {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


    }
}

