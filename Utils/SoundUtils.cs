using Sandbox.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Torch.SharedLibrary.Utils
{
    public static class SoundUtils
    {
        public static void SendTo(long playerId, VRage.Audio.MyGuiSounds sound = VRage.Audio.MyGuiSounds.HudGPSNotification3)
        {
            MyVisualScriptLogicProvider.PlayHudSound(sound, playerId);
        }

        public static void SendToAll(VRage.Audio.MyGuiSounds sound = VRage.Audio.MyGuiSounds.HudGPSNotification3)
        {
            MyVisualScriptLogicProvider.PlayHudSound(sound, 0);
        }
    }
}
