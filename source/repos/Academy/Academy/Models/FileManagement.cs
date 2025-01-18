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

        public void SaveAllStudentsInfoToSingleFile(int id)
        {
            try
            {
                var students = _context.Students.ToList();

                if (!students.Any())
                {
                    Console.WriteLine("No students found.");
                    return;
                }

                string fileName = Path.Combine("StudentInfoLogs", "AllStudentsInfo.txt");
                Directory.CreateDirectory("StudentInfoLogs");

                using (StreamWriter writer = new StreamWriter(fileName, append: true))  
                {
                    foreach (var student in students)
                    {
                        writer.WriteLine($"Student: {student.FirstName} {student.LastName}, Personal Number: {student.PersonalNumber}, Current Semester: 1st Semester");
                        writer.WriteLine(new string('-', 50));

                        var studentCourses = _context.StudentCourses.Where(sc => sc.StudentId == student.Id).ToList();
                        if (studentCourses.Any())
                        {
                            double totalGPA = 0, totalCredits = 0;

                            foreach (var studentCourse in studentCourses)
                            {
                                var course = _context.Courses.FirstOrDefault(c => c.Id == studentCourse.CourseId);
                                var grade = _context.Grades.FirstOrDefault(g => g.StudentId == student.Id && g.CourseId == course.Id);
                                double gradeValue = grade?.Score ?? 0;

                                totalGPA += (gradeValue / 100.0) * 4 * course.Credits;
                                totalCredits += course.Credits;

                                writer.WriteLine($"- {course.Name} - Grade: {gradeValue} (Credits: {course.Credits})");
                            }

                            double finalGPA = totalCredits > 0 ? totalGPA / totalCredits : 0;
                            writer.WriteLine($"GPA: {finalGPA:F2}");
                        }
                        else
                        {
                            writer.WriteLine("No courses found.");
                        }

                        writer.WriteLine(new string('-', 50));
                    }
                }

                Console.WriteLine($"All student info saved to {fileName}");
                LogAction($"All student info saved to {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                LogAction($"Error while saving all student info: {ex.Message}");
            }
        }


        public void LogAction(string actionDescription)
        {
            try
            {
                string logFilePath = @"C:\Users\raul0\source\repos\Academy\Academy\bin\Debug\net8.0\StudentInfoLogs\log.txt";
                string directory = Path.GetDirectoryName(logFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
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
