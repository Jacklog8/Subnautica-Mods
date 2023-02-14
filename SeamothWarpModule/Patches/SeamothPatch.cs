using HarmonyLib;
using UnityEngine;
using System.Collections;
using SMLHelper.V2.FMod;

namespace SeamothWarpModule_SN
{
    internal class SeamothPatch
    {
        private static readonly FMODAsset teleportSound = BepInEx.GetFmodAsset("event:/creature/warper/portal_open");
        public static Material mat = null;
        static GameObject gameObject = null;
        public static bool moduleSelected = false;

        [HarmonyPatch(typeof(SeaMoth))]
        public static class SeaMoth_Patch
        {
            [HarmonyPatch(nameof(SeaMoth.OnUpgradeModuleUse))]
            [HarmonyPostfix]
            public static void SeaMoth_OnUpgradeModuleUse(SeaMoth __instance, TechType techType, int slotID)
            {
                if (techType == SeamothModuleItem.ModuleTechtype)
                {
                    Warp(__instance, slotID, 20);
                }
            }

            [HarmonyPatch(nameof(SeaMoth.OnUpgradeModuleToggle))]
            [HarmonyPrefix]
            public static void SeaMoth_OnUpgradeModuleToggle(SeaMoth __instance, int slotID, bool active)
            {
                WarpPointMono.Kill();

                if (active)
                {
                    if (__instance.GetSlotBinding(slotID) == SeamothModuleItem.ModuleTechtype)
                    {
                        GameObject prefab = BepInEx.assetBundle.LoadAsset<GameObject>("WarpPoint.prefab");
                        gameObject = MonoBehaviour.Instantiate<GameObject>(new GameObject(), __instance.transform.position, __instance.transform.rotation);
                        gameObject.name = "WarpPoint";

                        gameObject.EnsureComponent<WarpPointMono>();
                    }
                }
            }

            [HarmonyPatch(nameof(SeaMoth.Awake))]
            [HarmonyPostfix]
            public static void SeaMoth_Awake(SeaMoth __instance)
            {
                __instance.StartCoroutine(setMat());
            }

            [HarmonyPatch(nameof(SeaMoth.Update))]
            [HarmonyPostfix]
            public static void SeaMoth_Update(SeaMoth __instance)
            {
                if(gameObject != null)
                    gameObject.GetComponent<WarpPointMono>().seamoth = __instance.transform;

                if (__instance.GetSlotBinding(__instance.activeSlot) == SeamothModuleItem.ModuleTechtype)
                    moduleSelected = true;
                else moduleSelected = false;
            }
        }

        [HarmonyPatch(typeof(Vehicle))]
        public static class Vehicle_Patch
        {
            [HarmonyPatch(nameof(Vehicle.SlotNext))]
            [HarmonyPrefix]
            public static bool Vehicle_SlotNext()
            {
                if (moduleSelected)
                    return false;
                else return true;
            }

            [HarmonyPatch(nameof(Vehicle.SlotPrevious))]
            [HarmonyPrefix]
            public static bool Vehicle_SlotPrevious()
            {
                if (moduleSelected)
                    return false;
                else return true;
            }
        }

        public static void Warp(SeaMoth seaMoth, int slot, float coolDown)
        {
            seaMoth.quickSlotCooldown[slot] = coolDown;

            seaMoth.transform.position = WarpPointMono.tpTransform.position;
            seaMoth.transform.rotation = WarpPointMono.tpTransform.rotation;
            seaMoth.GetComponent<Rigidbody>().velocity *= 3;

            Utils.PlayFMODAsset(teleportSound, seaMoth.transform);
        }

        public static IEnumerator setMat()
        {
            var task = new TaskResult<Material>();
            yield return BepInEx.GetHoloMaterial(task);
            mat = task.Get();
        }
    }
}