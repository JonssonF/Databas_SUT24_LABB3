using FREDRIK_JONSSON_SUT24_LABB3.Models;
using Microsoft.EntityFrameworkCore;
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
                var staff = context.Staff
                    .Include(r => r.Role)
                    .Include(d => d.Department)
                    .AsQueryable();
                Console.Clear();
                Console.WriteLine($"{".:ID:.".PadRight(5)}{".:Firstname:.".PadRight(15)}{".:Lastname:.".PadRight(15)}{".:Position:.".PadRight(20)}{".:Department:.".PadRight(20)}{".:Hired:.".PadRight(15)}");
                Console.WriteLine(new string('-', 85));

                foreach (var employee in staff)
                {
                    Console.WriteLine($"" +
                        $"  {employee.StaffId.ToString().PadRight(6)}" +
                        $"{employee.FirstName.PadRight(15)}" +
                        $"{employee.LastName.PadRight(15)}" +
                        $"{employee.Role.RoleName.ToString().PadRight(18)}" +
                        $"{employee.Department.DepartmentName.PadRight(18)}" +
                        $"{employee.HireDate.ToString().PadRight(15)}");
                        Console.WriteLine(new string('-', 85));
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
                    var roles = context.Roles.ToList();
                    Console.WriteLine(new string('-', 55));
                    foreach (var r in roles)
                    {
                        Console.WriteLine($"{r.RoleId}: {r.RoleName}");
                    }

                    int roleId = 0;
                    Role selectedRole = null;
                    while (selectedRole == null)
                    {
                        Console.Write("\nEnter Role ID: ");
                        string roleInput = Console.ReadLine();

                        if (int.TryParse(roleInput, out roleId))
                        {
                            selectedRole = roles.FirstOrDefault(r => r.RoleId == roleId);
                        }

                        if (selectedRole == null)
                        {
                            Console.WriteLine("Invalid role ID. Please choose a valid role from the list.");
                        }
                    }

                    var newStaff = new Staff
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        DoB = dob,
                        RoleId = selectedRole.RoleId  
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
