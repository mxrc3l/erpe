using System;
using erpe.Utils;
using GTANetworkAPI;

namespace erpe
{
    internal class Program : Script
    {
        public Program()
        {
            Console.WriteLine("[ERPE] Script launched successfully!");

            string connectionString = "server=localhost;user=root;database=erpe;port=3306;password=";
            MySQL.Initialize(connectionString);
        }
    }
}
