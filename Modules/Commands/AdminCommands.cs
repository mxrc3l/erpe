using GTANetworkAPI;

namespace erpe.Modules.Commands
{
    internal class AdminCommands : Script
    {
        [Command("veh")]
        public void SpawnVehicle(Player player, string vehicleName)
        {
            uint vehicleHash = NAPI.Util.GetHashKey(vehicleName);
            if (vehicleHash != 0)
            {
                NAPI.Vehicle.CreateVehicle(vehicleHash, player.Position, player.Heading, 0, 0);
                player.SendChatMessage($"Vehicle {vehicleName} spawned.");
            }
            else
            {
                player.SendChatMessage($"Vehicle {vehicleName} not found.");
            }
        }

        [Command("pos")]
        public void GetPlayerPosition(Player player)
        {
            Vector3 position = player.Position;
            player.SendChatMessage($"Your current position is: X: {position.X}, Y: {position.Y}, Z: {position.Z}");
        }
    }
}
