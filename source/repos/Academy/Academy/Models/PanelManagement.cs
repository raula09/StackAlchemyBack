using Academy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Models
{
    public class PanelManagement
    {
        private readonly AcademyDbContext _context;
        private readonly StudentManagement _studentManagement;
        private readonly CourseManagement _courseManagement;
        private readonly GradeManagement _gradeManagement;
        public PanelManagement(AcademyDbContext context)
        {
            _context = context;
            _studentManagement = new StudentManagement(_context);
            _courseManagement = new CourseManagement(_context);
            _gradeManagement = new GradeManagement(_context);
        }

        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("---- Main Menu ----");
                Console.WriteLine("1. Student Management");
                Console.WriteLine("2. Course Management");
                Console.WriteLine("3. Grade Management");
                Console.WriteLine("4. Analytics Management");
                Console.WriteLine("5. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        StudentManagementMenu();
                        break;

                    case "2":
                        CourseManagementMenu();
                        break;

                    case "3":
                        Console.WriteLine("Grade Management is under construction.");
                        break;

                    case "4":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        public void StudentManagementMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("---- Student Management ----");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View Student");
                Console.WriteLine("3. Update Student");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. Return to Main Menu");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter first name: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Enter last name: ");
                        string lastName = Console.ReadLine();
                        _studentManagement.AddStudent(firstName, lastName);
                        break;

                    case "2":

                        _studentManagement.ViewStudents();
                        break;


                    case "3":
                        Console.Write("Enter student ID: ");
                        int studentIdUpdate = int.Parse(Console.ReadLine());
                        _studentManagement.UpdateStudent(studentIdUpdate);
                        break;

                    case "4":
                        Console.Write("Enter student ID: ");
                        int studentIdDelete = int.Parse(Console.ReadLine());
                        _studentManagement.DeleteStudent(studentIdDelete);
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }

        }
        public void CourseManagementMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("---- Course Management ----");
                Console.WriteLine("1. Add Course");
                Console.WriteLine("2. View Courses");
                Console.WriteLine("3. Update Course");
                Console.WriteLine("4. Delete Course");
                Console.WriteLine("5. Return to Main Menu");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter Name: ");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter Credits: ");
                        int credits = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Semesters:");
                        int semester = int.Parse(Console.ReadLine());
                        _courseManagement.AddCourse(name, credits, semester);
                        break;

                    case "2":

                        _courseManagement.ViewAllCourses();
                        break;


                    case "3":
                        Console.Write("Enter student ID: ");
                        int studentIdUpdate = int.Parse(Console.ReadLine());
                        _studentManagement.UpdateStudent(studentIdUpdate);
                        break;

                    case "4":
                        Console.Write("Enter student ID: ");
                        int studentIdDelete = int.Parse(Console.ReadLine());
                        _studentManagement.DeleteStudent(studentIdDelete);
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        public void GradeManagementMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("---- Grade Management ----");
                Console.WriteLine("1. Assign Grade");
                Console.WriteLine("2. Update Grade");
                Console.WriteLine("3. View Student Grade");
                Console.WriteLine("4. View Course Grades");
                Console.WriteLine("5. Return to Main Menu");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":

                        Console.WriteLine("Enter Student Id: ");
                        int studentIdAssign = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Course Id: ");
                        int courseIdAssign = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Score:");
                        int scoreAssign = int.Parse(Console.ReadLine());
                        _gradeManagement.AssignGrade(studentIdAssign, courseIdAssign, scoreAssign);
                        break;

                    case "2":

                        Console.WriteLine("Enter Student Id: ");
                        int studentIdUpdate = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Course Id: ");
                        int courseIdUpdate = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter New Score:");
                        int newScore = int.Parse(Console.ReadLine());
                        _gradeManagement.UpdateGrade(studentIdUpdate, courseIdUpdate, newScore);
                        break;

                    case "3":

                        Console.Write("Enter student ID: ");
                        int studentIdView = int.Parse(Console.ReadLine());
                        Console.Write("Enter course ID: ");
                        int courseIdView = int.Parse(Console.ReadLine());
                        _gradeManagement.GetStudentGrade(studentIdView, courseIdView);
                        break;

                    case "4":

                        Console.Write("Enter course ID: ");
                        int courseIdGrades = int.Parse(Console.ReadLine());
                        _gradeManagement.GetCourseGrades(courseIdGrades);
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

    }
}

