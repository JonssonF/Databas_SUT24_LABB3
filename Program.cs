using Microsoft.Data.SqlClient;

namespace FREDRIK_JONSSON_SUT24_LABB3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionString = @"Data Source = localhost; Initial Catalog = LabbSchool; Integrated Security = true;";

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();



        }
    }
}
