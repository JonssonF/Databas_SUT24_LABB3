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
                Console.WriteLine("Hello and welcome to the School's data navigation system.");
                Console.WriteLine($"Please choose a option below:\n");
                Console.WriteLine(
                    "1.\n" +
                    "2.\n" +
                    "3.\n" +
                    "4.Exit program.");
                int userChoice = Choice(4);
                switch (userChoice)
                {
                    case 1:
                        Console.Clear();
                        Student.GetStudent();
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



        public static int Choice(int max) // Method to only accept valid keypress.
        {
            bool isNumber;
            int num = 0;
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                // Checks if the choice is a number
                // If it is, converts keypress
                // into an int. 
                isNumber = char.IsAsciiDigit(key.KeyChar);
                if (isNumber)
                {
                    num = Convert.ToInt32(key.KeyChar.ToString());
                }

            } while (!isNumber || num > max || num == 0); // exits while loop only if number is not bigger
                                                          // than the max index of the active users accounts.
            return num;
        }
    }
}
