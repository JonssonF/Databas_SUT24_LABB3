using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Data;

namespace FREDRIK_JONSSON_SUT24_LABB3.Menu
{
    public class Case4
    {
        private static readonly string _connectionString = "Data Source=localhost;Database=LabbSchool;Integrated Security=True; Trust Server Certificate=true;";
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
                    "\n8. Return to main menu");
                int userChoice = General.Choice(8);
                switch (userChoice)
                {
                    case 1:
                        Console.Clear();
                        General.Heading();
                        ShowStaff();
                        General.Return();
                        break;
                    case 2:
                        Console.Clear();
                        General.Heading();
                        NewStaff();
                        //Administratören vill också ha möjlighet att spara ner ny personal. (SQL via ADO.Net)
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
                        //Hur mycket betalar respektive avdelning ut i lön varje månad? (SQL via ADO.Net)
                        General.Return();
                        break;
                    case 5:
                        Console.Clear();
                        General.Heading();
                        //Hur mycket är medellönen för de olika avdelningarna? (SQL via ADO.Net)
                        General.Return();
                        break;
                    case 6:
                        Console.Clear();
                        General.Heading();
                        //Skapa en Stored Procedure som tar emot ett Id och returnerar viktig information om den elev som är registrerad med aktuellt Id. (SQL via ADO.Net)
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
                        CaseFour = false;
                        Menu.Start();
                        break;
                    default:
                        Console.WriteLine("Please try again. Make sure you write an integer between 1-8.");
                        break;
                }
            }
        }

        public static void ShowStaff()
        {
            string query = "SELECT FirstName + ' ' + LastName AS FullName, Role, DATEDIFF(YEAR, HireDate, GETDATE()) AS YearsAtSchool FROM Staff";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Console.WriteLine("\nStaff information:");
                            Console.WriteLine(new string('-', 55));
                            Console.WriteLine($".:{"Full Name".PadRight(17)} | {"Position".PadRight(15)} | {"Years Worked:."}");
                            Console.WriteLine(new string('-', 55));
                            while (reader.Read())
                            {
                                string fullName = reader["FullName"].ToString();
                                string role = reader["Role"].ToString();
                                int yearsAtSchool = (int)reader["YearsAtSchool"];

                                Console.WriteLine($".:{fullName.PadRight(17)} | {role.PadRight(15)} | {yearsAtSchool.ToString().PadLeft(3)} years");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
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
                ADO.GetRoles();
                Console.Write("\nWich role are you applying for: ");
                string role = Console.ReadLine();
                DateOnly? dob = null;
                Console.Write("\nWhat is your date of birth? (YYYY-MM-DD)\nBirthdate: ");
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
                }

                int departmentId = 0;

                //ADO.AddStaff(firstName, lastName, role, email, dob, departmentId);
            }

        }

    }
}
