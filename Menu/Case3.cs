using FREDRIK_JONSSON_SUT24_LABB3.Models;

namespace FREDRIK_JONSSON_SUT24_LABB3.Menu
{
    public class Case3
    {
        public static void CaseThree()
        {
            bool CaseThree = true;
            while (CaseThree)
            {
                General.Heading();
                Console.WriteLine($"\nHere you can find information about the different departments at this school:\n" +
                    "\n1. Show current subjects" +
                    "\n2. Show number of teachers per department" +
                    "\n3. Return to main menu");

                int userChoice = General.Choice(3);
                switch (userChoice)
                {
                    case 1:
                        Console.Clear();
                        ShowCurrentClasses();
                        break;
                    case 2:
                        Console.Clear();
                        ShowTeachersOnDepartment();
                        break;
                    case 3:
                        Console.Clear();
                        CaseThree = false;
                        Menu.Start();
                        break;
                    default:
                        Console.WriteLine("Please try again. Make sure you write an integer between 1-3.");
                        break;
                }
            }
        }

        public static void ShowTeachersOnDepartment()
        {
            using (var context = new LabbSchoolContext())
            {
                General.Heading();
                var teacher = context.Staff
                    .Where(s => s.Role == "Teacher" && s.DepartmentId != null)
                    .GroupBy(s => s.Department.DepartmentName)
                    .Select(g => new
                    {
                        Department = g.Key,
                        TeacherCount = g.Count()
                    })
                    .ToList();
                Console.WriteLine($"{".:Department:.".PadRight(20)}{".:Count:.".PadRight(15)}");
                Console.WriteLine(new string('-', 35));
                foreach (var t in teacher)
                {
                    Console.WriteLine($".:{t.Department.PadRight(16)} {t.TeacherCount} teachers :.");
                }
                General.Return();
            }
        }

        public static void ShowCurrentClasses()
        {
            using (var context = new LabbSchoolContext())
            {
                General.Heading();
                Console.WriteLine("\nThis is a list of all current subjects we teach at R'n'R HighSchool.\n");
                var classes = context.Subjects.AsQueryable();

                Console.WriteLine();

                Console.WriteLine($".:SUBJECT:.");
                Console.WriteLine(new string('-', 35));

                foreach (var c in classes)
                {
                    Console.WriteLine($".:{c.SubjectName}");
                }

                General.Return();
            }

        }
    }
}
