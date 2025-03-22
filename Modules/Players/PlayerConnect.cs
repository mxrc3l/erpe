using System;
using GTANetworkAPI;
using erpe.Modules.Auth;

namespace erpe.Modules.Players
{
    internal class PlayerConnect : Script
    {
        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnected(Player player)
        {
            Console.WriteLine($"Player {player.Name} connected.");
            player.TriggerEvent("client:auth:open");
        }

        [RemoteEvent("server:auth:login")]
        public void LoginAccount(Player player, string username, string password)
        {
            var loginSuccessful = new AuthSystem().LoginPlayer(player, username, password);
            if (loginSuccessful)
            {
                player.TriggerEvent("client:auth:close");
            }
        }

        [RemoteEvent("server:auth:register")]
        public void RegisterAccount(Player player, string username, string password)
        {
            var registrationSuccessful = new AuthSystem().RegisterPlayer(player, username, password);
            if (registrationSuccessful)
            {
                player.TriggerEvent("client:auth:close");
            }
        }
    }
}
