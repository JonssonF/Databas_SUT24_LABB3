using FREDRIK_JONSSON_SUT24_LABB3.Menu;
using FREDRIK_JONSSON_SUT24_LABB3.Models;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;

namespace FREDRIK_JONSSON_SUT24_LABB3
{
    public static class ADO
    {
        private static readonly string _connectionString = "Data Source=localhost;Database=LabbSchool;Integrated Security=True; Trust Server Certificate=true;";
        // Method to display staff information with years worked.
        public static void ShowStaff()
        {
            // SQL query to retrieve staff information, including full name, role, department, and years at school
            string query = @"
                    SELECT
                    s.StaffId,
                    s.FirstName + ' ' + LastName AS FullName,
                    r.RoleName AS Role,
                    d.DepartmentName AS Department,
                    DATEDIFF(YEAR, HireDate, GETDATE()) AS YearsAtSchool 
                    FROM Staff s
                    LEFT JOIN Role r ON s.Role_Id = r.RoleId
                    LEFT JOIN Department d ON s.Department_Id = d.DepartmentId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Console.WriteLine("\nStaff information:");
                            Console.WriteLine(new string('-', 81));
                            Console.WriteLine($".:{"ID".PadRight(5)} | {"Full Name".PadRight(17)} | {"Position".PadRight(15)} | {"Department".PadRight(15)} | {"Years Worked:.|"}");
                            Console.WriteLine(new string('-', 81));
                            while (reader.Read())
                            {
                                int staffId = (int)reader["StaffId"];
                                string fullName = reader["FullName"].ToString();
                                string role = reader["Role"].ToString();
                                string department = reader["Department"].ToString();
                                int yearsAtSchool = (int)reader["YearsAtSchool"];

                                Console.WriteLine($".:{staffId.ToString().PadRight(5)} | {fullName.PadRight(17)} | {role.PadRight(15)} | {department.PadRight(15)} | {yearsAtSchool.ToString().PadLeft(3)} {"years".PadRight(5)}      |");
                            }
                            Console.WriteLine(new string('-', 81));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        //Adds new employees to the database.
        public static void AddStaff(string firstName, string lastName, int roleId, string email, DateOnly? dob, int departmentId)
        {
            // SQL query to insert a new staff member into the Staff table.
            string query = "INSERT INTO Staff (FirstName, LastName, Role_Id, HireDate, Email, DoB, Department_Id)" +
                           "VALUES (@FirstName, @LastName, @RoleId, @HireDate, @Email, @DoB, @DepartmentId)";

            using SqlConnection connection = new SqlConnection(_connectionString);
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the SQL command to prevent SQL injection.
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@RoleId", roleId);
                    command.Parameters.AddWithValue("@HireDate", DateTime.Now);
                    command.Parameters.AddWithValue("@Email", email);
                    // Handle nullable DoB, convert to DateTime if present, or DBNull if not.
                    command.Parameters.AddWithValue("@DoB", dob.HasValue ? dob.Value.ToDateTime(TimeOnly.MinValue) : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DepartmentId", departmentId);
                    // Execute the query and get the number of affected rows.
                    int rowsAffected = command.ExecuteNonQuery();
                    // Display a success or failure message based on the number of affected rows.
                    Console.WriteLine(rowsAffected > 0
                        ? "New staff added, welcome to R'n'R HighSchool."
                        : "Failed to add staff.");
                }
            }
        }
        //Method to add new employee into the right department based by role choice.
        public static int DepartmentId(int roleId)
        {
            string query = "SELECT Department_Id FROM Staff WHERE Role_Id = @RoleId";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@RoleId", roleId);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        Console.WriteLine("No department found for the selected role.");
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }
        //Method the fetch all roles to display when adding staff.
        public static int GetRoles()
        {
            // SQL query to select all RoleId and RoleName from the Role table.
            string query = "SELECT RoleId, RoleName FROM Role";
            // Dictionary to store RoleId and RoleName for easy validation.
            Dictionary<int, string> roles = new Dictionary<int, string>();
            // Establish a connection to the database using the connection string.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {   // Open the database connection.
                    connection.Open();
                    // Create a SQL command with the query and connection.
                    SqlCommand command = new SqlCommand(query, connection);
                    // Execute the query and get a SqlDataReader to read the results.
                    SqlDataReader reader = command.ExecuteReader();
                    // Display available roles to the user.
                    Console.WriteLine("Available Roles:");
                    Console.WriteLine(new string('-', 35));

                    while (reader.Read())
                    {   // Extract RoleId and RoleName from the current row.
                        int roleId = (int)reader["RoleId"];
                        string roleName = reader["RoleName"].ToString();
                        // Store the RoleId and RoleName in the dictionary.
                        roles[roleId] = roleName;
                        // Display the role options to the console.
                        Console.WriteLine($"{roleId}.{roleName}");
                    }
                    // Close the reader after reading all data.
                    reader.Close();
                    Console.WriteLine(new string('-', 35));
                    // Continuously prompt the user to enter a valid role number.
                    while (true)
                    {
                        Console.WriteLine("Please enter the number of the role you are applying for: ");
                        // Try to parse the user's input as an integer and validate it against the available roles.
                        if (int.TryParse(Console.ReadLine(), out int selectedRoleId) && roles.ContainsKey(selectedRoleId))
                        {   
                            // If the input is a valid RoleId, return it.
                            return selectedRoleId;
                        }
                        else
                        {
                            // If the input is invalid, display an error message.
                            Console.WriteLine("Invalid number. Please choose from the list above.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
            }
        }
        //Method to remove staff from the database using ADO.
        public static void DeleteStaff(int staffId)
        {
            string query = "DELETE FROM Staff WHERE StaffId = @StaffId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StaffId", staffId);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine($"Staff with ID {staffId} has been succesfully removed.");
                        }
                        else
                        {
                            Console.WriteLine("No staff found with that ID, please make sure you select a valid ID from the list above.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
        //Method to display salary statistics.
        public static void SalaryDepartment(string type = "Total")
        {

            // Determine the SQL aggregate function based on the 'type' parameter
            // If type is "Average", use AVG(); otherwise, use SUM().
            string aggregateFunction = type == "Average" ? "AVG(d.Salary)" : "SUM(d.Salary)";


            // Construct the SQL query using string interpolation
            // The query joins the Staff and Department tables, groups by department,
            // and orders the results by salary in descending order.
            string query = $@"
             SELECT
                d.DepartmentName,
                {aggregateFunction} AS Salary
             FROM
                Staff s
             JOIN
                Department d ON s.Department_Id = d.DepartmentId
             GROUP BY
                d.DepartmentName
             ORDER BY
                Salary DESC;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Determine the label for output based on the 'type' parameter.
                            string label = type == "Average" ? "Average Salary" : "Total Salary";
                            Console.WriteLine($"\n{label} for each department:");
                            Console.WriteLine(new string('-', 40));
                            while (reader.Read())
                            {
                                // Extract department name and salary from the current row.
                                string departmentName = reader["DepartmentName"].ToString();
                                decimal salary = (decimal)reader["Salary"];

                                Console.WriteLine($"Department: {departmentName}");
                                Console.WriteLine($"{label}: {salary:C}");
                                Console.WriteLine(new string('-', 35));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        //Method to present information about a specific student by ID.
        public static void StudentInformation(int studentId)
        {
            // Name of the stored procedure to retrieve student information.
            string storedProcedure = "GetGradesByStudent";

            // Establish a connection to the database using the connection string.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    // Open the database connection.
                    connection.Open();

                    // Create a SQL command to execute the stored procedure.
                    using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                    {
                        // Specify that the command is a stored procedure.
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        // Add the StudentId parameter to the stored procedure.
                        command.Parameters.AddWithValue("@StudentId", studentId);
                        // Execute the stored procedure and read the results.
                        using (SqlDataReader reader = command.ExecuteReader())
                        {   // Check if the reader has any rows.
                            if (reader.HasRows)
                            {
                                // Clear the console and display the heading.
                                General.ClearAll();
                                General.Heading();
                                Console.WriteLine($"\nInformation for student with ID:{studentId}");
                                Console.WriteLine(new string('-', 50));
                                // Iterate through each row returned by the stored procedure.
                                while (reader.Read())
                                {
                                    // Retrieve student information from the current row.
                                    string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                                    int id = reader.GetInt32(reader.GetOrdinal("StudentId"));
                                    string lastName = reader.GetString(reader.GetOrdinal("LastName"));
                                    DateTime dob = reader.GetDateTime(reader.GetOrdinal("BirthDate"));
                                    string email = reader.GetString(reader.GetOrdinal("Email"));
                                    string className = reader.GetString(reader.GetOrdinal("ClassName"));

                                    // Flag to ensure the basic student info is only printed once.
                                    bool firstInfo = true;
                                    if (firstInfo == true)
                                    {
                                        // Display the retrieved student information.
                                        Console.WriteLine($".:ID: {id}");
                                        Console.WriteLine($".:Name: {firstName} {lastName}");
                                        Console.WriteLine($".:Date of Birth: {dob.ToShortDateString()}");
                                        Console.WriteLine($".:Email: {email}");
                                        Console.WriteLine($".:Class: {className}");
                                        // Ask the user if they want to see more information about the student
                                        Console.WriteLine($"\n\nWould you like to see even more information about {firstName}?");
                                        Console.WriteLine("1. Yes");
                                        Console.WriteLine("2. No i would like to return to the menu");
                                    }

                                    int moreInfo = General.Choice(2);
                                    switch (moreInfo)
                                    {
                                        case 1:
                                            // Set firstInfo to false to prevent re-displaying basic info.
                                            firstInfo = false;
                                            Console.WriteLine(new string('-', 50));
                                            // Call the MoreInfo method to display additional information.
                                            MoreInfo(studentId);
                                            break;
                                        case 2:
                                            firstInfo = false;
                                            return;
                                    }
                                }
                            }
                            else
                            {
                                // If no student is found with the given ID, display an error message.
                                Console.WriteLine("Could not find a student with the requested ID.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // If an exception occurs, display the error message.
                    Console.WriteLine("An error occured when trying to fetch information about student:" + ex.Message);
                }
            }
        }
        //Another method for even more information about student.
        public static void MoreInfo(int studentId)
        {
            // Name of the stored procedure to be executed.
            string storedProcedure = "GetGradesByStudent";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                    {
                        // Specify that the command is a stored procedure.
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StudentId", studentId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Iterate through each row returned by the stored procedure.
                            while (reader.Read())
                            {
                                string grade = reader.GetString(reader.GetOrdinal("Grade"));
                                string subjectName = reader.GetString(reader.GetOrdinal("SubjectName"));
                                DateTime gradeDate = reader.GetDateTime(reader.GetOrdinal("GradeDate"));
                                int teacherid = reader.GetInt32(reader.GetOrdinal("TeacherId"));
                                string teacherFirstName = reader.GetString(reader.GetOrdinal("TeacherFirstName"));
                                string teacherLastName = reader.GetString(reader.GetOrdinal("TeacherLastName"));

                                // Display the retrieved information to the console.
                                Console.WriteLine($".:Grade: {grade}");
                                Console.WriteLine($".:Subject: {subjectName}");
                                Console.WriteLine($".:Teacher: {teacherFirstName} {teacherLastName}");
                                Console.WriteLine($".:Grade Date: {gradeDate.ToShortDateString()}");
                                Console.WriteLine(new string('-', 50));
                            }
                            General.Return();
                            Case4.CaseFour();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured when trying to fetch information about student:" + ex.Message);
                }
            }
        }
        //Method to grade a student.
        public static void SetGrade(int studentId, int subjectId, string grade)
        {
            // SQL query to insert a new grade record into the Grade table.
            string query = "" +
                "INSERT INTO Grade (Student_Id, Subject_Id, Grade, DateAwarded)" +
                "VALUES (@StudentId, @SubjectId, @Grade, @DateAwarded)";

            // Establish a connection to the database using the connection string.
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    // Open the database connection.
                    connection.Open();

                    // Start a new transaction to ensure atomicity of the operation.
                    SqlTransaction transaction = connection.BeginTransaction();

                    // Create a SQL command to execute the insert query.
                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        // Add parameters to the SQL command to prevent SQL injection and to pass values.
                        command.Parameters.AddWithValue("@StudentId", studentId);
                        command.Parameters.AddWithValue("@SubjectId", subjectId);
                        command.Parameters.AddWithValue("@Grade", grade);
                        command.Parameters.AddWithValue("@DateAwarded", DateTime.Now);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0) 
                        {
                            throw new Exception("Could not register grade.");
                        }
                        // Commit the transaction to save the changes to the database.
                        transaction.Commit();
                        Console.WriteLine("Grade registered.");
                        General.Return();
                        Case4.CaseFour();
                    }
                }
                catch(Exception ex) 
                {
                    // If an exception occurs, display the error message.
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
