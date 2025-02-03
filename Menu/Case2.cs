using FREDRIK_JONSSON_SUT24_LABB3.Models;
using System.Security.Principal;

namespace FREDRIK_JONSSON_SUT24_LABB3.Menu
{
    public class Case2
    {
        public static void CaseTwo()
        {
            bool caseTwo = true;
            while (caseTwo)
            {
                General.Heading();
                Console.WriteLine($"\nWelcome to the staff section, what can i help you with:\n" +
                    "\n1. List all employees." +
                    "\n2. Add new employee." +
                    "\n3. Return to main menu.");
                int userChoice = General.Choice(3);
                switch (userChoice)
                {
                    case 1:
                        Console.Clear();
                        ListStaff();
                        break;
                    case 2:
                        Console.Clear();
                        AddStaff();
                        break;
                    case 3:
                        Console.Clear();
                        caseTwo = false;
                        Menu.Start();
                        break;
                    default:
                        Console.WriteLine("Plase try again. Make sure you write an integer between 1-3.");
                        break;
                }
            }
        }

        public static void ListStaff()
        {
            using (var context = new LabbSchoolContext())
            {
                var staff = context.Staff.AsQueryable();
                Console.Clear();
                Console.WriteLine($"{".:ID:.".PadRight(5)}{".:Firstname:.".PadRight(15)}{".:Lastname:.".PadRight(15)}{".:Position:.".PadRight(15)}");
                Console.WriteLine(new string('-', 50));

                foreach (var employee in staff)
                {
                    Console.WriteLine($"" +
                        $"  {employee.StaffId.ToString().PadRight(6)}" +
                        $"{employee.FirstName.PadRight(15)}" +
                        $"{employee.LastName.PadRight(15)}" +
                        $"{employee.Role.PadRight(15)}");
                }
                General.Return();
            }
        }

        public static void AddStaff()
        {
            try
            {
                using (var context = new LabbSchoolContext())
                {
                    Console.WriteLine("Welcome to our employee registration.");
                    Console.WriteLine("\n\nPlease enter following information about our new member of this school.");
                    Console.Write("\nFirstname: ");
                    string firstName = Console.ReadLine();
                    Console.Write("\nLastname: ");
                    string lastName = Console.ReadLine();
                    Console.Write("\nEmail-Adress: ");
                    string email = Console.ReadLine();

                    DateOnly? dob = null;
                    int tries = 0;
                    bool dateCheck = true;
                    while (dateCheck)
                    {
                        Console.Write("\nDate of Birth (YYYY-MM-DD): ");
                        if (tries < 3)
                        {
                            string input = Console.ReadLine();

                            if (DateOnly.TryParse(input, out DateOnly parsedDate))
                            {
                                dob = parsedDate;
                                break;
                            }
                            else
                            {
                                tries++;
                                Console.WriteLine($"The date was in the wrong format, please try again. You have made {tries}/3 attempts.");
                            }
                        }
                        else
                        {
                            dateCheck = false;
                            Console.WriteLine("Was in the wrong format. Failed to add new employees.");
                            Thread.Sleep(2000);
                            Console.Clear();
                            CaseTwo();
                        }
                    }
                    Console.WriteLine("\nWhich role will you have when you start your employment?");
                    var staff = context.Staff.AsQueryable();
                    var uniqueRoles = staff.Select(s => s.Role).Distinct();
                    Console.WriteLine(new string('-', 55));
                    List<string> validRoles = new List<string>();
                    foreach (var position in uniqueRoles)
                    {
                        Console.WriteLine($"{position}.");
                        validRoles.Add(position);
                    }
                    string role = string.Empty;
                    while (!validRoles.Contains(role))
                    {
                        Console.Write("\nRole: ");
                        role = Console.ReadLine();

                        if (!validRoles.Contains(role))
                        {
                            Console.WriteLine("Unvalid position. Choose from the list above and try again.");
                        }
                    }
                    var newStaff = new Staff
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        DoB = dob,
                        Role = role
                    };

                    context.Staff.Add(newStaff);
                    context.SaveChanges();
                    Console.WriteLine(new string('-', 55));
                    Console.WriteLine($"\nWelcome to our team {firstName}!\nI hope you will enjoy it here at our school.");

                    General.Return();
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Something went wrong, please try again.");
                AddStaff();
            }
        }
    }
}
