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
    }
}
