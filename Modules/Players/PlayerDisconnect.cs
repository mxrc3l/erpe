using System;
using GTANetworkAPI;
using erpe.Utils;
using MySql.Data.MySqlClient;

namespace erpe.Modules.Players
{
    internal class PlayerDisconnect : Script
    {
        [ServerEvent(Event.PlayerDisconnected)]
        public void OnPlayerDisconnected(Player player, DisconnectionType type, string reason)
        {
            SavePlayerPosition(player);
        }

        private void SavePlayerPosition(Player player)
        {
            using (var connection = MySQL.GetConnection())
            {
                string query = "UPDATE accounts SET position_x = @position_x, position_y = @position_y, position_z = @position_z WHERE socialclubusername = @socialclubusername";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@position_x", player.Position.X);
                    cmd.Parameters.AddWithValue("@position_y", player.Position.Y);
                    cmd.Parameters.AddWithValue("@position_z", player.Position.Z);
                    cmd.Parameters.AddWithValue("@socialclubusername", player.SocialClubName);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
