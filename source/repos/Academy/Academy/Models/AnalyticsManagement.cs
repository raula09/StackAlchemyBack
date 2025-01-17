using Academy.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Academy.Models
{
    public class AnalyticsManagement
    {
        private readonly AcademyDbContext _context;

        public AnalyticsManagement(AcademyDbContext context)
        {
            _context = context;
        }

        public void DisplayStudentsGPA()
        {
            try
            {
                var grades = _context.Grades.Include(g => g.Course).ToList();

                if (grades == null || !grades.Any())
                {
                    Console.WriteLine("No grades available.");
                    return;
                }

                var studentsWithGrades = grades.GroupBy(g => g.StudentId)
                                               .Where(g => g.Any())
                                               .ToList();

                foreach (var group in studentsWithGrades)
                {
                    var student = _context.Students.FirstOrDefault(i => i.Id == group.Key);
                    var studentGrades = group.ToList();

                    if (studentGrades.Any())
                    {
                        double totalGPA = 0;
                        int totalCredits = 0;

                        foreach (var grade in studentGrades)
                        {
                            totalGPA += (grade.Score / 100.0) * 4 * grade.Course.Credits;
                            totalCredits += grade.Course.Credits;
                        }

                        double finalGPA = totalCredits > 0 ? totalGPA / totalCredits : 0;
                        Console.WriteLine($"{student.FirstName} {student.LastName} ID: {student.Id} - GPA: {finalGPA:F2}");
                    }
                    else
                    {
                        Console.WriteLine($"{student.FirstName} {student.LastName} ID: {student.Id} has not taken any classes.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void GetStudentsWithNoGrade()
        {
            try
            {
                var studentsWithNoGrade = _context.Students
                                                  .Where(s => !_context.Grades.Any(g => g.StudentId == s.Id))
                                                  .ToList();

                if (!studentsWithNoGrade.Any())
                {
                    Console.WriteLine("All students have received grades.");
                    return;
                }

                foreach (var student in studentsWithNoGrade)
                {
                    Console.WriteLine($"Student {student.FirstName} {student.LastName} (ID: {student.Id}) has not received a grade.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void DisplayStudentRankingsInCourse(int courseId)
        {
            try
            {
                var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);

                if (course == null)
                {
                    Console.WriteLine("Course not found.");
                    return;
                }

                var studentsInCourse = _context.StudentCourses
                                               .Where(sc => sc.CourseId == courseId)
                                               .Select(sc => sc.StudentId)
                                               .ToList();

                if (!studentsInCourse.Any())
                {
                    Console.WriteLine($"No students are enrolled in {course.Name}.");
                    return;
                }

                var sortedRankings = _context.Grades
                                             .Where(g => studentsInCourse.Contains(g.StudentId) && g.CourseId == courseId)
                                             .Join(_context.Students, g => g.StudentId, s => s.Id, (g, s) => new
                                             {
                                                 StudentId = s.Id,
                                                 StudentName = $"{s.FirstName} {s.LastName}",
                                                 Grade = g.Score
                                             })
                                             .OrderByDescending(r => r.Grade)
                                             .ToList();

                Console.WriteLine($"Rankings for {course.Name}:");
                int rank = 1;
                foreach (var ranking in sortedRankings)
                {
                    Console.WriteLine($"Rank {rank}: {ranking.StudentName} - Grade: {ranking.Grade}");
                    rank++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void AvgCourseGrade(int studentId, int courseId)
        {
            try
            {
                var student = _context.Students.FirstOrDefault(s => s.Id == studentId);
                var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);

                if (student == null)
                {
                    Console.WriteLine("Student not found");
                    return;
                }
                if (course == null)
                {
                    Console.WriteLine("Course not found");
                    return;
                }

                var grades = _context.Grades.Where(g => g.StudentId == studentId && g.CourseId == courseId).ToList();

                if (!grades.Any())
                {
                    Console.WriteLine($"{student.FirstName} {student.LastName} has no grades in {course.Name}.");
                    return;
                }

                double avgGrade = grades.Average(g => g.Score);

                Console.WriteLine($"{student.FirstName} {student.LastName} average grade in {course.Name} is: {avgGrade:F2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
