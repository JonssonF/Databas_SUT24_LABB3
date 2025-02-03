namespace FREDRIK_JONSSON_SUT24_LABB3
{
    public class General
    {
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

        public static void Process()
        {
            string Proccess = "Searching DataBase...";
            string rounds = "1";
            foreach (char c in rounds)
            {
                //Console.Clear();
                
                foreach (char d in Proccess)
                {
                    Console.Write(d);
                    Thread.Sleep(35);
                }             
            }
        }

        public static void Heading()
        {
            Console.WriteLine("{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}\r\n{} ____  _       _ ____       _   _ _       _     ____       _                 _  {}\r\n{}|  _ \\( )_ __ ( )  _ \\     | | | (_) __ _| |__ / ___|  ___| |__   ___   ___ | | {}\r\n{}| |_) |/| '_ \\|/| |_) |____| |_| | |/ _` | '_ \\\\___ \\ / __| '_ \\ / _ \\ / _ \\| | {}\r\n{}|  _ <  | | | | |  _ <_____|  _  | | (_| | | | |___) | (__| | | | (_) | (_) | | {}\r\n{}|_| \\_\\ |_| |_| |_| \\_\\    |_| |_|_|\\__, |_| |_|____/ \\___|_| |_|\\___/ \\___/|_| {}\r\n{}                                    |___/                                       {}\r\n{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}{}");
        }

        public static void ClearAll()
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.Clear();
        }

        public static void Return()
        {
            Console.Write("\n\nPress any key to return to the menu.");
            Console.ReadKey();
            General.ClearAll();
        }
    }
}
