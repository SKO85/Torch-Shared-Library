using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torch.SharedLibrary.Utils
{
    public static class NetworkUtils
    {
        /// <summary>
        /// Get Players IP address
        /// </summary>
        /// <param name="steamId">SteamId of the player</param>
        /// <returns></returns>
        public static IPAddress GetIPAddressOfClient(ulong steamId)
        {
            try
            {
                var state = new MyP2PSessionState();

                if (!MySteamServiceWrapper.Static.Peer2Peer.GetSessionState(steamId, ref state))
                    return null;
                return new IPAddress(BitConverter.GetBytes(state.RemoteIP).Reverse().ToArray());
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get Ping of Online Players
        /// </summary>
        /// <returns></returns>
        public static Dictionary<ulong, short> GetPings()
        {
            Dictionary<ulong, short> result = new Dictionary<ulong, short>();
            if (MyMultiplayer.Static != null && MyMultiplayer.Static.ReplicationLayer != null)
            {
                SerializableDictionary<ulong, short> pings;
                ((MyReplicationServer)MyMultiplayer.Static.ReplicationLayer).GetClientPings(out pings);
                result = pings.Dictionary;
            }
            return result;
        }
    }
}
