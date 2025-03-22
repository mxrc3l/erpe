using System;
using MySql.Data.MySqlClient;
using GTANetworkAPI;
using erpe.Utils;
using BCrypt.Net;

namespace erpe.Modules.Auth
{
    internal class AuthSystem : Script
    {
        public bool RegisterPlayer(Player player, string username, string password)
        {
            try
            {
                string socialClubUsername = player.SocialClubName;
                string hwid = player.Serial;
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                using (var connection = MySQL.GetConnection())
                {
                    string query = "INSERT INTO accounts (username, password, hwid, socialclubusername, position_x, position_y, position_z) VALUES (@username, @password, @hwid, @socialclubusername, @position_x, @position_y, @position_z)";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);
                        cmd.Parameters.AddWithValue("@hwid", hwid);
                        cmd.Parameters.AddWithValue("@socialclubusername", socialClubUsername);
                        cmd.Parameters.AddWithValue("@position_x", player.Position.X);
                        cmd.Parameters.AddWithValue("@position_y", player.Position.Y);
                        cmd.Parameters.AddWithValue("@position_z", player.Position.Z);

                        cmd.ExecuteNonQuery();
                    }
                }

                player.SendChatMessage("~g~Registration successful!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during registration: {ex.Message}");
                player.SendChatMessage("~r~An error occurred during registration.");
                return false;
            }
        }

        public bool LoginPlayer(Player player, string username, string password)
        {
            try
            {
                using (var connection = MySQL.GetConnection())
                {
                    string query = "SELECT password, position_x, position_y, position_z FROM accounts WHERE username = @username";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedPassword = reader.GetString("password");
                                if (BCrypt.Net.BCrypt.Verify(password, storedPassword))
                                {
                                    float positionX = reader.GetFloat("position_x");
                                    float positionY = reader.GetFloat("position_y");
                                    float positionZ = reader.GetFloat("position_z");

                                    player.Position = new Vector3(positionX, positionY, positionZ);
                                    player.SendChatMessage("~g~Login successful!");
                                    return true;
                                }
                                else
                                {
                                    // Wrong password
                                    return false;
                                }
                            }
                            else
                            {
                                // Username not found
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] during login: {ex.Message}");
                return false;
            }
        }
    }
}
