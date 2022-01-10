using Dalamud.IoC;
using Dalamud.Data;
using Dalamud.Game;
using Dalamud.Game.Gui;
using Dalamud.Game.Command;
using Dalamud.Game.ClientState.Party;

namespace FFLogsLookup
{
    public class Service
    {
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static Configuration Configuration { get; set; }
        [PluginService][RequiredVersion("1.0")] public static CommandManager CommandManager { get; private set; }
        [PluginService][RequiredVersion("1.0")] public static PartyList PartyList { get; private set; }
        [PluginService][RequiredVersion("1.0")] public static ChatGui ChatGui { get; private set; }
        [PluginService][RequiredVersion("1.0")] public static SigScanner SigScanner { get; private set; }
        [PluginService][RequiredVersion("1.0")] public static DataManager DataManager { get; private set; }
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
