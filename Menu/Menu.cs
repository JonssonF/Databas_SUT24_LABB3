using FREDRIK_JONSSON_SUT24_LABB3.Models;

namespace FREDRIK_JONSSON_SUT24_LABB3.Menu
{
    public class Menu
    {
        public static void Start()
        {
            bool menuBool = true;
            while (menuBool)
            {
                Console.Clear();
                General.Heading();
                Console.WriteLine("Hello and welcome to the School's data navigation system.");
                Console.WriteLine($"In which section do you seek knowledge:\n");
                Console.WriteLine(
                    "1.Student\n" +
                    "2.Staff\n" +
                    "3.Departments\n" +
                    "4.ADO.NET Section\n\n" +
                    "5.Exit program");
                int userChoice = General.Choice(5);
                switch (userChoice)
                {
                    case 1:
                        Console.Clear();
                        Case1.CaseOne();
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.Clear();
                        Case2.CaseTwo();    
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.Clear();
                        Case3.CaseThree();
                        Console.ReadLine();
                        break;
                    case 4:
                        Console.Clear();
                        Case4.CaseFour();
                        Console.ReadLine();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Thanks for using our navigation service. Welcome back.");
                        Thread.Sleep(2500);
                        menuBool = false;
                        break;
                    default:
                        Console.WriteLine("Plase try again. Make sure you write an integer between 1-5.");
                        break;
                }
            }
        }
    }
}
