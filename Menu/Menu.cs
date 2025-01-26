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
                Console.WriteLine("(BETA)-- This application is under development.\n\n");
                Console.WriteLine("Hello and welcome to the School's data navigation system.");
                Console.WriteLine($"In wich section do you seek knowledge:\n");
                Console.WriteLine(
                    "1.Student\n" +
                    "2.Staff\n" +
                    "3.Classes\n" +
                    "4.Exit program.");
                int userChoice = General.Choice(4);
                switch (userChoice)
                {
                    case 1:
                        Console.Clear();
                        Case1.CaseOne();
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Testa tvåan");
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Testa trean");
                        Console.ReadLine();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Thanks for using our navigation service. Welcome back.");
                        Thread.Sleep(2500);
                        menuBool = false;
                        break;
                    default:
                        Console.WriteLine("Plase try again. Make sure you write an integer between 1-4.");
                        break;
                }
            }
        }



        
    }
}
