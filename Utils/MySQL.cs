using System;
using MySql.Data.MySqlClient;

namespace erpe.Utils
{
    internal class MySQL
    {
        private static MySqlConnection connection;

        public static void Initialize(string connectionString)
        {
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("[MySQL] Connection established.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MySQL] Connection error: {ex.Message}");
            }
        }

        public static MySqlConnection GetConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[MySQL] Failed to open connection: {ex.Message}");
                    return null;
                }
            }
            return connection;
        }
    }
}
