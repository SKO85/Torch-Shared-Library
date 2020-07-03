using Sandbox.Engine.Multiplayer;
using Sandbox.Game;
using Sandbox.Game.Entities;
using Sandbox.Game.World;
using Sandbox.ModAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Torch;
using VRage.Game.ModAPI;
using VRage.GameServices;
using VRage.Network;
using VRage.Serialization;

namespace Torch.SharedLibrary.Utils
{
    public class PlayerUtils
    {
        #region Getters

        public static IMyPlayer GetPlayer(ulong steamId)
        {
            var player = MySession.Static.Players.GetPlayerById(new MyPlayer.PlayerId(steamId));
            if (player != null)
                return player;
            else return null;
        }

        public static ulong GetSteamId(IMyPlayer player)
        {
            if (player == null)
                return 0L;
            return player.SteamUserId;
        }

        #endregion Getters

        public static MyIdentity GetIdentityByNameOrId(string playerNameOrSteamId)
        {
            foreach (var identity in MySession.Static.Players.GetAllIdentities())
            {
                if (identity.DisplayName == playerNameOrSteamId)
                    return identity;

                if (ulong.TryParse(playerNameOrSteamId, out ulong steamId))
                {
                    ulong id = MySession.Static.Players.TryGetSteamId(identity.IdentityId);
                    if (id == steamId)
                        return identity;
                }
            }

            return null;
        }

        public static long GetOwner(MyCubeGrid grid)
        {
            var gridOwnerList = grid.BigOwners;
            var ownerCnt = gridOwnerList.Count;
            var gridOwner = 0L;

            if (ownerCnt > 0 && gridOwnerList[0] != 0)
                return gridOwnerList[0];
            else if (ownerCnt > 1)
                return gridOwnerList[1];

            return gridOwner;
        }

        public static MyIdentity GetIdentityByName(string playerName)
        {
            foreach (var identity in MySession.Static.Players.GetAllIdentities())
                if (identity.DisplayName == playerName)
                    return identity;

            return null;
        }

        public static MyIdentity GetIdentityById(long playerId)
        {
            foreach (var identity in MySession.Static.Players.GetAllIdentities())
                if (identity.IdentityId == playerId)
                    return identity;

            return null;
        }

        public static string GetPlayerNameById(long playerId)
        {
            MyIdentity identity = GetIdentityById(playerId);

            if (identity != null)
                return identity.DisplayName;

            return "Nobody";
        }

        public static bool IsNpc(long playerId)
        {
            return MySession.Static.Players.IdentityIsNpc(playerId);
        }

        public static bool IsPlayer(long playerId)
        {
            return GetPlayerIdentity(playerId) != null;
        }

        public static bool HasIdentity(long playerId)
        {
            return MySession.Static.Players.HasIdentity(playerId);
        }

        public static List<MyIdentity> GetAllPlayerIdentities()
        {
            if (MySession.Static == null)
            {
                return new List<MyIdentity>();
            }
            var idents = MySession.Static.Players.GetAllIdentities().ToList();
            var npcs = MySession.Static.Players.GetNPCIdentities().ToList();
            return idents.Where(i => !npcs.Any(n => n == i.IdentityId)).OrderBy(i => i.DisplayName).ToList();
        }

        public static MyIdentity GetPlayerIdentity(long identityId)
        {
            return GetAllPlayerIdentities().Where(c => c.IdentityId == identityId).FirstOrDefault();
        }

        public static IMyPlayer GetPlayer(long identityId)
        {
            ulong steamId = MySession.Static.Players.TryGetSteamId(identityId);
            var player = MySession.Static.Players.GetPlayerById(new MyPlayer.PlayerId(steamId));
            if (player != null)
            {
                return player;
            }
            else return null;
        }

        public static List<IMyPlayer> GetAllPlayers()
        {
            if (MySession.Static == null)
            {
                return new List<IMyPlayer>();
            }

            var list = new List<IMyPlayer>();
            MyAPIGateway.Players.GetPlayers(list);
            return list;
        }

        public static bool IsAdmin(IMyPlayer player)
        {
            if (player != null && (player.PromoteLevel == MyPromoteLevel.Owner || player.PromoteLevel == MyPromoteLevel.Admin || player.PromoteLevel == MyPromoteLevel.Moderator))
            {
                return true;
            }
            return false;
        }

        public static long GetIdentityIdByName(string name)
        {
            var identity = MySession.Static.Players.GetAllIdentities().Where(c => c.DisplayName.Equals(name, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (identity != null)
            {
                return identity.IdentityId;
            }
            return 0;
        }
    }
}
