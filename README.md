# School Database Program

This is a small school database application developed as part of a course project using Entity Framework and SQL.

## Features

### Console (Entity Framework)
- **Fetch all students**: View students sorted by either first or last name, with an option for ascending or descending order.
- **Fetch students in a specific class**: Select a class from a list, and view all students enrolled in that class, with an option to sort them.
- **Add new staff**: Enter details of a new staff member, which will be saved to the database.

### SQL (Azure Data Studio)
- **Fetch staff**: View all staff or filter by category (e.g., teachers).
- **Fetch grades from the last month**: See a list of grades given in the past month, including student names, subjects, and grades.
- **Fetch subject statistics**: View average, highest, and lowest grades for each subject.
- **Add new students**: Enter details of a new student, which will be saved to the database.

## Setup
1. Clone the repository.
2. Open the project in Visual Studio.
3. Ensure that you have a SQL Server database set up locally, and update the connection string in the `OnConfiguring` method of `LabbSchoolContext`.
4. Run the program and interact with the console menu.

## Screenshots

![Database Screenshot](https://github.com/JonssonF/Databas_SUT24_LABB3/blob/main/ER%20Labb3.drawio.pdf)

## License
This project is licensed under the MIT License.

