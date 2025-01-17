using Academy.Data;
using System;
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

        public void SaveStudentInfoToFile(int studentId)
        {
            try
            {
                var student = _context.Students
                                      .FirstOrDefault(s => s.Id == studentId);

                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    LogAction($"Failed to save student info: Student with ID {studentId} not found.");
                    return;
                }

                string directoryPath = "StudentInfoLogs";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string fileName = Path.Combine(directoryPath, $"student_{student.Id}_info.txt");

                using (StreamWriter writer = new StreamWriter(fileName))
                {
               
                    writer.WriteLine($"Student Info: {student.FirstName} {student.LastName} ID: {student.Id} Personal Number: {student.PersonalNumber}");
                    writer.WriteLine(new string('-', 50));

                  
                    var studentCourses = _context.StudentCourses
                                                 .Where(sc => sc.StudentId == studentId)
                                                 .ToList();

                   
                    if (!studentCourses.Any())
                    {
                        writer.WriteLine("No courses found for this student.");
                    }
                    else
                    {
                        writer.WriteLine("Enrolled Courses and Grades:");
                        double totalGPA = 0;
                        int totalCredits = 0;

                        foreach (var studentCourse in studentCourses)
                        {
                            var course = _context.Courses.FirstOrDefault(c => c.Id == studentCourse.CourseId);
                            if (course != null)
                            {
                                var grade = _context.Grades
                                                    .FirstOrDefault(g => g.StudentId == studentId && g.CourseId == course.Id);
                                double gradeValue = grade != null ? grade.Score : 0;

                               
                                totalGPA += (gradeValue / 100.0) * 4 * course.Credits;
                                totalCredits += course.Credits;

                                writer.WriteLine($"- {course.Name} - Grade: {gradeValue} (Credits: {course.Credits})");
                            }
                        }

                       
                        double finalGPA = totalCredits > 0 ? totalGPA / totalCredits : 0;
                        writer.WriteLine(new string('-', 50));
                        writer.WriteLine($"GPA: {finalGPA:F2}");
                    }

                   
                    writer.WriteLine("Current Semester: 1st Semester");
                    writer.WriteLine(new string('-', 50));
                }

                Console.WriteLine($"Student info saved to {fileName}");
                LogAction($"Student info for {student.FirstName} {student.LastName} ID: {student.Id} saved to {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                LogAction($"Error while saving student info for student ID {studentId}: {ex.Message}");
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
