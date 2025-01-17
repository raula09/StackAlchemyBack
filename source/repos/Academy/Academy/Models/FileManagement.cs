using Academy.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Academy.Models
{
    public class FileManagement
    {
        private readonly AcademyDbContext _context;
        private readonly string logFilePath = "system_log.txt";

        public FileManagement(AcademyDbContext context)
        {
            _context = context;
        }

        public void StudentTranscript(int studentId)
        {
            try
            {
                var student = _context.Students
                                      .FirstOrDefault(s => s.Id == studentId);

                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    LogAction($"Failed to generate transcript: Student with ID {studentId} not found.");
                    return;
                }

                var studentCourses = _context.StudentCourses
                                             .Where(sc => sc.StudentId == studentId)
                                             .ToList();

                if (!studentCourses.Any())
                {
                    Console.WriteLine($"No courses found for student {student.FirstName} {student.LastName}.");
                    LogAction($"Student {student.FirstName} {student.LastName} has no courses.");
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
                LogAction($"Transcript generated for student {student.FirstName} {student.LastName} (ID: {student.Id}) and saved to {fileName}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                LogAction($"Error while generating transcript for student ID {studentId}: {ex.Message}");
            }
        }

        public void LogAction(string actionDescription)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {actionDescription}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while logging: {ex.Message}");
            }
        }
    }
}
