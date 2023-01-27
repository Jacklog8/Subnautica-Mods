using HarmonyLib;
using UnityEngine;

namespace SunbeamCountdownFix_SN
{
    internal class TimerFix
    {
        [HarmonyPatch(typeof(uGUI_SunbeamCountdown))]
        public class uGUI_SunbeamCountdown_Patch
        {
            [HarmonyPatch(nameof(uGUI_SunbeamCountdown.Start))]
            [HarmonyPostfix]
            public static void uGUI_SunbeamCountdown_Start_Patch(uGUI_SunbeamCountdown __instance)
            {
                __instance.gameObject.transform.localPosition = new Vector3(-1496.492f, 0, 0);
            }
        }
    }
}
