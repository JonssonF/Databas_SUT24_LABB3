using FREDRIK_JONSSON_SUT24_LABB3.Models;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Data;
using System.Diagnostics;

namespace FREDRIK_JONSSON_SUT24_LABB3.Menu
{
    public class Case4 //ADO relatead methods.
    {
        public static void CaseFour()
        {
            bool CaseFour = true;
            while (CaseFour)
            {
                General.Heading();
                Console.WriteLine($"" +
                    "\n1. Show staff information" +
                    "\n2. Add staff" +
                    "\n3. Remove staff" +
                    "\n4. Salary Statistics" +
                    "\n5. Show student information" +
                    "\n6. Grade a student" +
                    "\n7. Return to main menu");
                int userChoice = General.Choice(9);
                switch (userChoice)
                {
                    case 1:
                        Console.Clear();
                        General.Heading();
                        ADO.ShowStaff();
                        General.Return();
                        break;
                    case 2:
                        Console.Clear();
                        General.Heading();
                        NewStaff();
                        General.Return();
                        break;
                    case 3:
                        Console.Clear();
                        General.Heading();
                        ADO.ShowStaff();
                        RemoveStaff();
                        General.Return();
                        break;
                    case 4:
                        Console.Clear();
                        General.Heading();
                        SalarySelection();
                        General.Return();
                        break;
                    case 5:
                        General.ClearAll();
                        General.Heading();
                        FetchStudent();
                        General.ClearAll();
                        break;
                    case 6:
                        General.ClearAll();
                        General.Heading();
                        Grade();
                        General.Return();
                        break;
                    case 7:
                        Console.Clear();
                        CaseFour = false;
                        Menu.Start();
                        break;
                    default:
                        Console.WriteLine("Please try again. Make sure you write an integer between 1-7.");
                        break;
                }
            }
        }
        public static void SalarySelection()
        {
            General.ClearAll();
            General.Heading();
            Console.WriteLine("\n1.Total Salary per Department");
            Console.WriteLine("2.Average Salary per Department");
            Console.Write("Option: ");
            int salarySelect = General.Choice(2);
            switch (salarySelect)
            {
                case 1:
                    ADO.SalaryDepartment("Total");
                    break;
                case 2:
                    ADO.SalaryDepartment("Average");
                    break;
            }
        }
        public static void FetchStudent()
        {
            General.ClearAll();
            Case1.GetStudents();
            Console.WriteLine("Enter student ID for more detailed information.");
            int studentId = Convert.ToInt32(Console.ReadLine());
            ADO.StudentInformation(studentId);
        }

        public static void RemoveStaff()
        {
            Console.WriteLine("\nEnter the ID of the staff you would like to remove.");
            int deleteId = Convert.ToInt32(Console.ReadLine());
            ADO.DeleteStaff(deleteId);
        }

        public static void NewStaff()
        {
            bool Registration = true;
            while (Registration)
            {
                Console.Write("We will need some information before we continue.\n\n" +
                    "What is your firstname? ");
                string firstName = Console.ReadLine();
                Console.Write("\nLastname: ");
                string lastName = Console.ReadLine();
                Console.Write("\nEmail adress: ");
                string email = Console.ReadLine();
                Console.WriteLine();

                DateOnly? dob = null;
                Console.Write("What is your date of birth? (YYYY-MM-DD)\nBirthdate: ");
                if (DateOnly.TryParse(Console.ReadLine(), out DateOnly parsedDob))
                {
                    dob = parsedDob;
                }
                else
                {
                    Console.WriteLine("Invalid formatting. Registration cancelled");
                    Thread.Sleep(2000);
                    Registration = false;
                    Menu.Start();
                    return;
                }
                Console.WriteLine();

                int roleId = ADO.GetRoles();

                if (roleId == -1)
                {
                    Console.WriteLine("Something went wrong while selecting role.");
                    General.Return();
                    return;
                }           

                int departmentId = ADO.DepartmentId(roleId);
                if (roleId == 6)
                {
                    Console.Write("\nSince you are applying as a teacher, which grade are you teaching?\n" +
                        "1. 7th Grade\n" +
                        "2. 8th Grade\n" +
                        "3. 9th Grade\n" +
                        "Option: ");
                    int gradeChoice;
                    while (true)
                    {
                        if(int.TryParse(Console.ReadLine(), out gradeChoice) && gradeChoice >= 1 && gradeChoice <= 3)
                        {
                            departmentId = gradeChoice + 1;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid grade choice, please choose between 1.(7), 2.(8) or 3.(9)");
                        }
                    }
                }
                Registration = false;
                ADO.AddStaff(firstName, lastName, roleId, email, dob, departmentId);
            }
        }

        public static void Grade()
        {
            using (var context = new LabbSchoolContext())
            {
                int studentId;               
                var validStudentIds = context.Students.Select(s => s.StudentId).ToList();              
                do
                {
                    Console.WriteLine("\nPlease enter the ID of the student you would like to grade.");
                    Console.Write("Student ID: ");
                    bool isValid = int.TryParse(Console.ReadLine(), out studentId);

                    if (!isValid || !validStudentIds.Contains(studentId))
                    {
                        Console.WriteLine("Invalid student ID. Please enter a valid ID.");
                    }
                }
                while (!validStudentIds.Contains(studentId));

                Console.WriteLine("\n\nPlease enter the ID of the subject you would like to grade.");
                Console.WriteLine("1. Math");
                Console.WriteLine("2. English");
                Console.WriteLine("3. Science");
                Console.WriteLine("4. History");
                Console.WriteLine("5. Art");
                Console.WriteLine("6. Physical Education");
                Console.Write("Subject ID: ");
                int subjectId = General.Choice(6);

                string[] validGrades = { "A", "B", "C", "D", "E", "F" };
                string grade;
                do
                {
                    Console.WriteLine("\n\nEnter grade from A to F.");
                    Console.Write("Grade: ");
                    grade = Console.ReadLine().ToUpper(); // Gör input till versaler för att matcha validering
                }
                while (!validGrades.Contains(grade));


                ADO.SetGrade(studentId, subjectId, grade);
            }
        }
    }
}
