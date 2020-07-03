using Sandbox.Game.World;
using VRage.Game.ModAPI;

namespace Torch.SharedLibrary.Utils
{
    public class FactionUtils
    {
        public static IMyFaction GetFaction(long factionId)
        {
            return MySession.Static.Factions.TryGetFactionById(factionId);
        }

        public static IMyFaction GetFaction(string factionTag)
        {
            return MySession.Static.Factions.TryGetFactionByTag(factionTag);
        }

        public static IMyFaction GetFactionOfPlayer(long playerId)
        {
            return MySession.Static.Factions.TryGetPlayerFaction(playerId);
        }

        public static string GetFactionTagOfPlayer(long playerId)
        {
            var faction = GetFactionOfPlayer(playerId);
            if (faction == null)
                return "";
            return faction.Tag;
        }

        public static bool HavePlayersSameFaction(long playerA_Id, long playerB_Id)
        {
            var factionA = GetFactionOfPlayer(playerA_Id);
            var factionB = GetFactionOfPlayer(playerB_Id);
            return factionA == factionB;
        }

        public static bool ExistsFaction(long factionId)
        {
            return GetFaction(factionId) != null;
        }

        public static bool ExistsFaction(string factionTag)
        {
            return GetFaction(factionTag) != null;
        }
    }
}
