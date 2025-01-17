using Academy.Data;
using System;
using System.IO;

namespace Academy.Models
{
    public class PanelManagement
    {
        private readonly AcademyDbContext _context;
        private readonly StudentManagement _studentManagement;
        private readonly CourseManagement _courseManagement;
        private readonly GradeManagement _gradeManagement;
        private readonly AnalyticsManagement _analyticsManagement;

        public PanelManagement(AcademyDbContext context)
        {
            _context = context;
            _studentManagement = new StudentManagement(_context);
            _courseManagement = new CourseManagement(_context);
            _gradeManagement = new GradeManagement(_context);
            _analyticsManagement = new AnalyticsManagement(_context);
        }

        public void MainMenu()
        {
            while (true)
            {
               
                Console.WriteLine("---- Main Menu ----");
                Console.WriteLine("1. Student Management");
                Console.WriteLine("2. Course Management");
                Console.WriteLine("3. Grade Management");
                Console.WriteLine("4. Analytics Management");
                Console.WriteLine("5. Exit");
                Console.WriteLine("6. View System Log");

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
                        GradeManagementMenu();
                        break;

                    case "4":
                        AnalyticsManagementMenu();
                        break;

                    case "5":
                        Console.WriteLine("Bye bye");
                        return;

                    case "6":
                       
                        ViewSystemLog();
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        public void ViewSystemLog()
        {
            try
            {
                string logFilePath = "system_log.txt"; 
                if (File.Exists(logFilePath))
                {
                    string logContent = File.ReadAllText(logFilePath); 
                    Console.WriteLine("---- System Log ----");
                    Console.WriteLine(logContent);
                }
                else
                {
                    Console.WriteLine("No system log found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading system log: {ex.Message}");
            }
        }

        public void StudentManagementMenu()
        {
            while (true)
            {
               
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

                       
                        DateTime dateOfBirth;
                        Console.Write("Enter Date of Birth (yyyy-mm-dd): ");
                        while (!DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
                        {
                            Console.WriteLine("Invalid date format. Please enter the date in yyyy-mm-dd format.");
                        }

                       
                        Console.Write("Enter phone number: ");
                        string phoneNumber = Console.ReadLine();

                        
                        Console.Write("Enter address: ");
                        string address = Console.ReadLine();

                        
                        var student = new Student
                        {
                            FirstName = firstName,
                            LastName = lastName
                        };

                        var studentDetails = new StudentDetails
                        {
                            DateOfBirth = dateOfBirth,
                            PhoneNumber = phoneNumber,
                            Address = address,
                            CurrentSemester = 1  
                        };

                  
                        _studentManagement.AddStudent(firstName, lastName,dateOfBirth, phoneNumber, address);
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
                        Console.Write("Enter Course ID to update: ");
                        int courseIdUpdate = int.Parse(Console.ReadLine());

                   
                        Console.Write("Enter new Course Name: ");
                        string newName = Console.ReadLine();
                        Console.Write("Enter new Credits: ");
                        int newCredits = int.Parse(Console.ReadLine());
                        Console.Write("Enter new Semester: ");
                        int newSemester = int.Parse(Console.ReadLine());

                        
                        _courseManagement.UpdateCourse(courseIdUpdate, newName, newCredits, newSemester);
                        break;

                    case "4":
                        Console.Write("Enter Course ID to delete: ");
                        int courseIdDelete = int.Parse(Console.ReadLine());
                        _courseManagement.DeleteCourse(courseIdDelete);
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

        public void AnalyticsManagementMenu()
        {
            while (true)
            {
               
                Console.WriteLine("---- Analytics Management ----");
                Console.WriteLine("1. Display Students GPA");
                Console.WriteLine("2. Get Students with No Grade");
                Console.WriteLine("3. View Student Rankings in Course");
                Console.WriteLine("4. View Course Average Grade");
                Console.WriteLine("5. Return to Main Menu");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _analyticsManagement.DisplayStudentsGPA();
                        break;

                    case "2":
                        _analyticsManagement.GetStudentsWithNoGrade();
                        break;

                    case "3":
                        Console.WriteLine("Enter course Id: ");
                        int courseId = int.Parse(Console.ReadLine());
                        _analyticsManagement.DisplayStudentRankingsInCourse(courseId);
                        break;

                    case "4":
                        Console.WriteLine("Enter student Id: ");
                        int studentId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter course Id: ");
                        int courseId2 = int.Parse(Console.ReadLine());
                        _analyticsManagement.AvgCourseGrade(studentId, courseId2);
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
