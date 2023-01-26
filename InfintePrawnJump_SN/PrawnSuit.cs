using HarmonyLib;
using UnityEngine.UI;

namespace InfintePrawnJump_SN
{
    public static class PrawnSuit
    {
        [HarmonyPatch(typeof(Exosuit))]
        public static class Exosuit_Patch
        {
            [HarmonyPatch(nameof(Exosuit.Update))]
            [HarmonyPostfix]
            public static void Update_Postfix(Exosuit __instance)
            {
                __instance.thrustConsumption = 0f;
            }
        }
    }
}
