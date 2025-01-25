namespace FREDRIK_JONSSON_SUT24_LABB3
{
    public class Meny
    {
        public static void Menu()
        {
            bool menuBool = true;
            while (menuBool)
            {
                Console.WriteLine("Hello and welcome to the School's data navigation system.");
                Console.WriteLine($"Please choose a option below:\n");
                Console.WriteLine(
                    "1." + 
                    "2." +
                    "3." +
                    "4.Exit program.");

                switch (Choice(4))
                {
                    case 1:

                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:
                        menuBool = false;
                        break;
                    default:
                        Console.WriteLine("Plase try again. Make sure u write an integer between 1-4.");
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
                isNumber = Char.IsAsciiDigit(key.KeyChar);
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
