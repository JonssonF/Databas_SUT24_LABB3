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
                Console.WriteLine($"\nHere you can find some information about the classes at this school:\n" +
                    "\n1. Show current classes" +
                    "\n2. Return to main menu");

                int userChoice = General.Choice(2);
                switch (userChoice)
                {
                    case 1:
                        Console.Clear();
                        ShowCurrentClasses();
                        break;
                    case 2:
                        Console.Clear();
                        CaseThree = false;
                        Menu.Start();
                        break;
                    default:
                        Console.WriteLine("Please try again. Make sure you write an integer between 1-2.");
                        break;
                }
            }
        }



        public static void ShowCurrentClasses()
        {
            using (var context = new LabbSchoolContext())
            {
                General.Heading();
                Console.WriteLine("\nThis is a list of all current classes at R'n'R HighSchool.\n");
                var classes = context.Subjects.AsQueryable();

                Console.WriteLine({});

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
