using FREDRIK_JONSSON_SUT24_LABB3.Menu;
using FREDRIK_JONSSON_SUT24_LABB3.Models;
using Microsoft.Data.SqlClient;

namespace FREDRIK_JONSSON_SUT24_LABB3
{
    public static class ADO
    {
        private static readonly string _connectionString = "Data Source=localhost;Database=LabbSchool;Integrated Security=True; Trust Server Certificate=true;";

        public static void ShowStaff()
        {
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
        public static void AddStaff(string firstName, string lastName, int roleId, string email, DateOnly? dob, int departmentId)
        {
            string query = "INSERT INTO Staff (FirstName, LastName, Role_Id, HireDate, Email, DoB, Department_Id)" +
                           "VALUES (@FirstName, @LastName, @RoleId, @HireDate, @Email, @DoB, @DepartmentId)";

            using SqlConnection connection = new SqlConnection(_connectionString);
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@RoleId", roleId);
                    command.Parameters.AddWithValue("@HireDate", DateTime.Now);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@DoB", dob.HasValue ? dob.Value.ToDateTime(TimeOnly.MinValue) : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DepartmentId", departmentId);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected > 0
                        ? "New staff added, welcome to R'n'R HighSchool."
                        : "Failed to add staff.");
                }
            }
        }
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
        public static int GetRoles()
        {
            string query = "SELECT RoleId, RoleName FROM Role";
            Dictionary<int, string> roles = new Dictionary<int, string>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    Console.WriteLine("Available Roles:");
                    Console.WriteLine(new string('-', 35));

                    while (reader.Read())
                    {
                        int roleId = (int)reader["RoleId"];
                        string roleName = reader["RoleName"].ToString();
                        roles[roleId] = roleName;
                        Console.WriteLine($"{roleId}.{roleName}");
                    }
                    reader.Close();
                    Console.WriteLine(new string('-', 35));

                    while (true)
                    {
                        Console.WriteLine("Please enter the number of the role you are applying for: ");

                        if (int.TryParse(Console.ReadLine(), out int selectedRoleId) && roles.ContainsKey(selectedRoleId))
                        {
                            return selectedRoleId;
                        }
                        else
                        {
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
        public static void SalaryDepartment(string type = "Total")
        {
            string aggregateFunction = type == "Average" ? "AVG(d.Salary)" : "SUM(d.Salary)";

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
                            string label = type == "Average" ? "Average Salary" : "Total Salary";
                            Console.WriteLine($"\n{label} for each department:");
                            Console.WriteLine(new string('-', 40));
                            while (reader.Read())
                            {
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
        public static void StudentInformation(int studentId)
        {
            string storedProcedure = "GetGradesByStudent";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StudentId", studentId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                General.ClearAll();
                                General.Heading();
                                Console.WriteLine($"\nInformation for student with ID:{studentId}");
                                Console.WriteLine(new string('-', 50));
                                while (reader.Read())
                                {
                                    string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                                    int id = reader.GetInt32(reader.GetOrdinal("StudentId"));
                                    string lastName = reader.GetString(reader.GetOrdinal("LastName"));
                                    DateTime dob = reader.GetDateTime(reader.GetOrdinal("BirthDate"));
                                    string email = reader.GetString(reader.GetOrdinal("Email"));
                                    string className = reader.GetString(reader.GetOrdinal("ClassName"));
                                    bool firstInfo = true;
                                    if (firstInfo == true)
                                    {
                                        Console.WriteLine($".:ID: {id}");
                                        Console.WriteLine($".:Name: {firstName} {lastName}");
                                        Console.WriteLine($".:Date of Birth: {dob.ToShortDateString()}");
                                        Console.WriteLine($".:Email: {email}");
                                        Console.WriteLine($".:Class: {className}");
                                        Console.WriteLine($"\n\nWould you like to see even more information about {firstName}?");
                                        Console.WriteLine("1. Yes");
                                        Console.WriteLine("2. No i would like to return to the menu");
                                    }

                                    int moreInfo = General.Choice(2);
                                    switch (moreInfo)
                                    {
                                        case 1:
                                            firstInfo = false;
                                            Console.WriteLine(new string('-', 50));
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
                                Console.WriteLine("Could not find a student with the requested ID.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured when trying to fetch information about student:" + ex.Message);
                }
            }
        }
        public static void MoreInfo(int studentId)
        {
            string storedProcedure = "GetGradesByStudent";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(storedProcedure, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StudentId", studentId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string grade = reader.GetString(reader.GetOrdinal("Grade"));
                                string subjectName = reader.GetString(reader.GetOrdinal("SubjectName"));
                                DateTime gradeDate = reader.GetDateTime(reader.GetOrdinal("GradeDate"));
                                int teacherid = reader.GetInt32(reader.GetOrdinal("TeacherId"));
                                string teacherFirstName = reader.GetString(reader.GetOrdinal("TeacherFirstName"));
                                string teacherLastName = reader.GetString(reader.GetOrdinal("TeacherLastName"));

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

        

        public static void SetGrade(int studentId, int subjectId, string grade)
        {
            string query = "" +
                "INSERT INTO Grade (Student_Id, Subject_Id, Grade, DateAwarded)" +
                "VALUES (@StudentId, @SubjectId, @Grade, @DateAwarded)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {

                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@StudentId", studentId);
                        command.Parameters.AddWithValue("@SubjectId", subjectId);
                        command.Parameters.AddWithValue("@Grade", grade);
                        command.Parameters.AddWithValue("@DateAwarded", DateTime.Now);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0) 
                        {
                            throw new Exception("Could not register grade.");
                        }

                        transaction.Commit();
                        Console.WriteLine("Grade registered.");
                        General.Return();
                        Case4.CaseFour();
                    }
                }
                catch(Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
