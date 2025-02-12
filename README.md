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

## ERD

![Database Screenshot](https://github.com/JonssonF/Databas_SUT24_LABB3/blob/main/ER%20Labb3.drawio.pdf)

## License
This project is licensed under the MIT License.

Copyright (c) 2025 Fredrik Jonsson

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

