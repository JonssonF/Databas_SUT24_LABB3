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
                            Console.WriteLine(new string('-', 73));
                            Console.WriteLine($".:{"Full Name".PadRight(17)} | {"Position".PadRight(15)} | {"Department".PadRight(15)} | {"Years Worked:."}");
                            Console.WriteLine(new string('-', 73));
                            while (reader.Read())
                            {
                                string fullName = reader["FullName"].ToString();
                                string role = reader["Role"].ToString();
                                string department = reader["Department"].ToString();
                                int yearsAtSchool = (int)reader["YearsAtSchool"];

                                Console.WriteLine($".:{fullName.PadRight(17)} | {role.PadRight(15)} | {department.PadRight(15)} | {yearsAtSchool.ToString().PadLeft(3)} {"years".PadRight(5)}      |");
                            }
                            Console.WriteLine(new string('-', 73));
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
    }
}
