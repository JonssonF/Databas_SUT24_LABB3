using FREDRIK_JONSSON_SUT24_LABB3.Models;
using System;

namespace FREDRIK_JONSSON_SUT24_LABB3.Menu
{
    public class Case1
    {
        public static void CaseOne()
        {
            bool CaseOne = true;
            while (CaseOne)
            {
                Console.WriteLine($"Welcome to the student section, what can i help you with:\n" +
                    "\n1. List all students." +
                    "\n2. Information about specific student." +
                    "\n3. Students by class." +
                    "\n4. Return to main menu.");
                int userChoice = General.Choice(4);
                switch (userChoice)
                {
                    case 1:
                        Console.Clear();
                        GetStudents();
                        break;
                    case 2:
                        Console.Clear();
                        SpecifyStudent();
                        break;
                    case 3:
                        Console.Clear();
                        ByClass();
                        break;
                    case 4:
                        Console.Clear();
                        CaseOne = false;
                        Menu.Start();
                        break;
                    default:
                        Console.WriteLine("Plase try again. Make sure you write an integer between 1-3.");
                        break;
                }
            }
        }

        public static void GetStudents()
        {
            using (var context = new LabbSchoolContext())
            {
                var students = context.Students.AsQueryable();
                students = SortStudents(students);
               
                Console.Clear();
                Console.WriteLine($"{".:ID:.".PadRight(5)}{".:Firstname:.".PadRight(15)}{".:Lastname:.".PadRight(15)}");
                Console.WriteLine(new string('-',35));
                
                foreach (var student in students)
                {
                    Console.WriteLine($"" +
                        $"  {student.StudentId.ToString().PadRight(6)}" +
                        $"{student.FirstName.PadRight(15)}" +
                        $"{student.LastName.PadRight(15)}");
                }
                Console.Write("\n\nPress any key to return to the menu.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public static void SpecifyStudent()
        {
            using (var context = new LabbSchoolContext())
            {
                Console.Write("Please write the specific ID of the student you would like to find.\n\nStudent ID: ");
                string userInputString = Console.ReadLine();

                General.Process();
                if (int.TryParse(userInputString, out int userInput)) 
                { 
                    var specStudent = context.Students.FirstOrDefault(student => student.StudentId == userInput);
                    if (specStudent != null)
                    {
                        Console.WriteLine("We have a match!");
                        Thread.Sleep(1500);
                        Console.Clear();
                        Console.WriteLine(
                            $".:ID: {specStudent.StudentId}\n" +
                            $".:Firstname: {specStudent.FirstName}\n" +
                            $".:Lastname: {specStudent.LastName}\n" +
                            $".:Date of birth: {specStudent.DoB}\n" +
                            $".:Email adress: {specStudent.Email}");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"Sorry i could not find what you were searching for.\n" +
                                $"If you are unsure of the ID, feel free to look at the student list.");
                    }
                }
                Console.Write("\n\nPress any key to return to the menu.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public static void ByClass()
        {
            Console.Clear();
            using (var context = new LabbSchoolContext())
            {
                var classes = context.Classes;
                int nrClasses = context.Classes.Count();
                int count = 0;
                Console.WriteLine($"Currently, we have {nrClasses} classes at this school.");
                Console.WriteLine("Please choose a class to view the list of students.\n");

                foreach (var c in classes)
                {
                    Console.Write($"{c.ClassName},");

                    count++;

                    if (count % 3 == 0)
                    {
                        Console.WriteLine();
                    }
                }
                Console.Write($"\nOption: ");
                string option = Console.ReadLine();
                //Console.Write($"{option}\n");
                General.Process();
                Console.Clear();

                var selectedClass = classes.FirstOrDefault(c => c.ClassName.Equals(option));
                if (selectedClass != null)
                {
                    var studentsInClass = context.Students
                     .Where(s => s.Classes.Any(c => c.ClassName == selectedClass.ClassName))
                     .AsQueryable();

                    studentsInClass = SortStudents(studentsInClass);

                    Console.WriteLine($"Students in class {selectedClass.ClassName}.");
                    Console.WriteLine(new string('-', 35));
                    Console.WriteLine($"{".:ID:.".PadRight(5)}{".:Firstname:.".PadRight(15)}{".:Lastname:.".PadRight(15)}");
                    foreach (var student in studentsInClass)
                    {
                        Console.WriteLine($"" +
                        $"  {student.StudentId.ToString().PadRight(6)}" +
                        $"{student.FirstName.PadRight(15)}" +
                        $"{student.LastName.PadRight(15)}");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Sorry i could not find what you were searching for.\n" +
                            $"If you are unsure of the classname, feel free to look at the list and try again.");
                }
                Console.Write("\n\nPress any key to return to the menu.");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public static IQueryable<Student> SortStudents(IQueryable<Student> students)
        {
            using (var context = new LabbSchoolContext())
            {
                Console.Write("How would you like to sort the students:\n1. By firstname\n2. By lastname \nOption:");
                int sortChoice = General.Choice(2);
                Console.Write(sortChoice);
                Console.Write("\n\nWould you like the list descending or ascending?:\n1. Ascending\n2. Descending \nOption:");
                int sortOrder = General.Choice(2);
                Console.Write($"{sortOrder}\n");
                Console.WriteLine(new string('-', 35));

                if (sortChoice == 1)
                {
                    students = sortOrder == 1 ?
                               students.OrderBy(s => s.FirstName) :
                               students.OrderByDescending(s => s.FirstName);
                }
                else
                {
                    students = sortOrder == 1 ?
                               students.OrderBy(s => s.LastName) :
                               students.OrderByDescending(s => s.LastName);
                }
                return students;
            }
        }
    }
}
