using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Data;

namespace FREDRIK_JONSSON_SUT24_LABB3.Menu
{
    public class Case4 // ADO Section
    {
        public static void CaseFour()
        {
            bool CaseFour = true;
            while (CaseFour)
            {
                General.Heading();
                Console.WriteLine($"\nWelcome to the student section, what can i help you with:\n" +
                    "\n1. Show staff information" +
                    "\n2. Add new staff" +
                    "\n3. Show grade for specific student" +
                    "\n4. Salary by department" +
                    "\n5. Average salary by department" +
                    "\n6. Show specific student information" +
                    "\n7. Grade a student" +
                    "\n8. Remove staff" +
                    "\n9. Return to main menu");
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
                        //Vi vill kunna ta fram alla betyg för en elev i varje kurs/ämne de läst och vi vill kunna
                        //se vilken lärare som satt betygen, vi vill också se vilka datum betygen satts. (SQL via ADO.Net)
                        General.Return();
                        break;
                    case 4:
                        Console.Clear();
                        General.Heading();
                        ADO.SalaryDepartment("Total");
                        General.Return();
                        break;
                    case 5:
                        Console.Clear();
                        General.Heading();
                        ADO.SalaryDepartment("Average");
                        General.Return();
                        break;
                    case 6:
                        Console.Clear();
                        FetchStudent();
                        General.Return();
                        break;
                    case 7:
                        Console.Clear();
                        General.Heading();
                        //Sätt betyg på en elev genom att använda Transactions ifall något går fel. (SQL via ADO.Net)
                        General.Return();
                        break;
                    case 8:
                        Console.Clear();
                        General.Heading();
                        ADO.ShowStaff();
                        RemoveStaff();
                        General.Return();
                        break;
                    case 9:
                        Console.Clear();
                        CaseFour = false;
                        Menu.Start();
                        break;
                    default:
                        Console.WriteLine("Please try again. Make sure you write an integer between 1-9.");
                        break;
                }
            }
        }
        public static void FetchStudent()
        {
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
                            Console.WriteLine("Invalid grade choice, please choose between 7, 8 or 9.");
                        }
                    }
                }
                Registration = false;
                ADO.AddStaff(firstName, lastName, roleId, email, dob, departmentId);
            }
        }
    }
}
