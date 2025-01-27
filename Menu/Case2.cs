using FREDRIK_JONSSON_SUT24_LABB3.Models;

namespace FREDRIK_JONSSON_SUT24_LABB3.Menu
{
    public class Case2
    {
        public static void CaseTwo()
        {
            bool caseTwo = true;
            while (caseTwo)
            {
                Console.WriteLine($"Welcome to the staff section, what can i help you with:\n" +
                    "\n1. List all employees." +
                    "\n2. Add new employee." +
                    "\n3. Return to main menu.");
                int userChoice = General.Choice(4);
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
                        Console.WriteLine("Plase try again. Make sure you write an integer between 1-4.");
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
                Console.Write("\n\nPress any key to return to the menu.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public static void AddStaff()
        {
            Console.WriteLine("Hejsan vilken perosnal vill du lägga till mannen brusch?!");
        }
    }
}
