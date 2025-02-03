using Microsoft.Data.SqlClient;

namespace FREDRIK_JONSSON_SUT24_LABB3
{
    public static class ADO
    {
        private static readonly string _connectionString = "Data Source=localhost;Database=LabbSchool;Integrated Security=True; Trust Server Certificate=true;";

        public static void AddStaff(string firstName, string lastName, string role, string email, DateOnly? dob, int departmentId)
        {
            string query = "INSERT INTO Staff (FirstName, LastName, Role, HireDate, Email, Department_Id) VALUES";

            using SqlConnection connection = new SqlConnection(_connectionString);
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Role", role);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@DoB", dob);
                    command.Parameters.AddWithValue("@DepartmentId", departmentId);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine(rowsAffected < 0 ? "New staff added, welcome to R'n'R HighSchool." : "Failed to add staff.");
                }
            }
        }

        public static void GetRoles()
        {
            string query = "SELECT DISTINCT Role FROM Staff";

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
                        Console.WriteLine(reader["Role"].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
