using HarmonyLib;
using Logger = QModManager.Utility.Logger;
using UnityEngine;

namespace TestMod_SN
{
    class TestMod
    {
        [HarmonyPatch(typeof(PlayerTool))]
        [HarmonyPatch("Awake")]
        internal class PatchPlayerTool
        {
            [HarmonyPostfix]
            public static void Postfix(PlayerTool __instance)
            {
                if (__instance.GetType() == typeof(Knife))
                {
                    Knife knife = __instance as Knife;

                    float knifeDamage = knife.damage;
                    float newKnifeDamage = knifeDamage * QMod.config.KnifeModifier;
                    knife.damage = newKnifeDamage;

                    Logger.Log(Logger.Level.Debug, $"Knife damage was: {knifeDamage}," + $" is now; {newKnifeDamage}");
                }
            }
        }

        [HarmonyPatch(typeof(ReaperLeviathan))]
        [HarmonyPatch("Update")]
        internal class PatchReaperLeviathan
        {
            [HarmonyPostfix]
            public static void Postfix(ReaperLeviathan __instance)
            {
                if(QMod.config.RemoveReaper)
                GameObject.Destroy(__instance.gameObject);
            }
        }
    }
}
