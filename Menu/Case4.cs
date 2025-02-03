using Microsoft.VisualBasic;

namespace FREDRIK_JONSSON_SUT24_LABB3.Menu
{
    public class Case4
    {
        public static void CaseFour()
        {
            bool CaseFour = true;
            while (CaseFour)
            {
                General.Heading();
                Console.WriteLine($"\nWelcome to the student section, what can i help you with:\n" +
                    "\n1. Show staff information" +
                    "\n2. Show" +
                    "\n3. Students by class" +
                    "\n4. Return to main menu");
                int userChoice = General.Choice(8);
                switch (userChoice)
                {
                    case 1:
                        Console.Clear();
                        //Skolan vill kunna ta fram en översikt över all personal där det framgår
                        //namn och vilka befattningar de har samt hur många år de har arbetat på skolan.
                        break;
                    case 2:
                        Console.Clear();
                        //Administratören vill också ha möjlighet att spara ner ny personal. (SQL via ADO.Net)
                        break;
                    case 3:
                        Console.Clear();
                        //Vi vill kunna ta fram alla betyg för en elev i varje kurs/ämne de läst och vi vill kunna
                        //se vilken lärare som satt betygen, vi vill också se vilka datum betygen satts. (SQL via ADO.Net)
                        break;
                    case 4:
                        Console.Clear();
                        //Hur mycket betalar respektive avdelning ut i lön varje månad? (SQL via ADO.Net)
                        break;
                    case 5:
                        Console.Clear();
                        //Hur mycket är medellönen för de olika avdelningarna? (SQL via ADO.Net)
                        break;
                    case 6:
                        Console.Clear();
                        //Skapa en Stored Procedure som tar emot ett Id och returnerar viktig information om den elev som är registrerad med aktuellt Id. (SQL via ADO.Net)
                        break;
                    case 7:
                        Console.Clear();
                        //Sätt betyg på en elev genom att använda Transactions ifall något går fel. (SQL via ADO.Net)
                        break;
                    case 8:
                        Console.Clear();
                        CaseFour = false;
                        Menu.Start();
                        break;
                    default:
                        Console.WriteLine("Please try again. Make sure you write an integer between 1-8.");
                        break;
                }
            }
        }

        public static void ShowStaff()
        {
        
        }

    }
}
