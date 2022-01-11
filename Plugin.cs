using System;
using System.Linq;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Game.Text;
using Dalamud.Game.Command;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

namespace FFLogsLookup
{
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "FFLogs Lookup";
        private const string CommandName = "/fflogs";

        public Plugin([RequiredVersion("1.0")] DalamudPluginInterface pluginInterface)
        {
            pluginInterface.Create<Service>();
            Service.Configuration = pluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            Service.Configuration.Initialize(pluginInterface);
            Crossworld.Init();
            Service.CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
            {
                HelpMessage = "Get the FFLogs profile url for each party member"
            });
        }

        public void Dispose()
        {
            Service.CommandManager.RemoveHandler(CommandName);
        }

        private void OnCommand(string command, string args)
        {
            byte partySize = Crossworld.getCrossRealmPartySize();
            if (partySize > 1)
            {
                for (int i = 1; i < partySize; i++)
                {
                    (byte HomeWorld, string Name) player = Crossworld.GetCrossRealmPlayer(i);
                    SendUrlToChat(player.Name, player.HomeWorld);
                }
            }
            else
            {
                Service.ChatGui.PrintChat(new XivChatEntry()
                {
                    Message = "No party members found",
                    Type = XivChatType.ErrorMessage
                });
            }
        }

        private void SendUrlToChat(string name, byte worldId)
        {
            ExcelSheet<World>? worldSheet = Service.DataManager.GetExcelSheet<World>();
            World[]? worldArray = worldSheet?.ToArray();
            if (worldArray != null)
            {
                World? world = Array.Find(worldArray, x => x.RowId == worldId);
                if (world != null)
                {
                    Service.ChatGui.PrintChat(new XivChatEntry()
                    {
                        Message = $"https://www.fflogs.com/character/na/{world.Name}/{Uri.EscapeDataString(name)}",
                        Type = XivChatType.Echo
                    });
                }
                else
                {
                    Service.ChatGui.PrintChat(new XivChatEntry()
                    {
                        Message = $"Unknown world for {name}",
                        Type = XivChatType.ErrorMessage
                    });
                }
            }
            else
            {
                Service.ChatGui.PrintChat(new XivChatEntry()
                {
                    Message = "Lumina Excel sheet for Worlds missing",
                    Type = XivChatType.ErrorMessage
                });
            }
        }
    }
}
