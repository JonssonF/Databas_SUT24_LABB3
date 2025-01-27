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
                    "\n1. List all students." +
                    "\n2. Information about specific student." +
                    "\n3. Students by class." +
                    "\n4. Return to main menu.");
                int userChoice = General.Choice(4);
                switch (userChoice)
                {
                    case 1:

                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 4:

                        break;
                    default:
                        Console.WriteLine("Plase try again. Make sure you write an integer between 1-4.");
                        break;

                }

            }
        }

        public static void AddStaff()
        {
            Console.WriteLine("Hejsan vilken perosnal vill du lägga till mannen brusch?!");
        }
    }
}
