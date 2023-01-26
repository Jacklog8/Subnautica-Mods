using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace InfintePrawnJump_SN
{
    public static class FixUI
    {
        [HarmonyPatch(typeof(uGUI_ExosuitHUD))]
        public static class uGUI_ExosuitHUD_Patch
        {
            [HarmonyPatch(nameof(uGUI_ExosuitHUD.Update))]
            [HarmonyPostfix]
            public static void Update_Postfix(uGUI_ExosuitHUD __instance)
            {
                GameObject Exosuit = __instance.gameObject.FindChild("Content").FindChild("Exosuit");
                GameObject Background = Exosuit.FindChild("Background");

                Exosuit.FindChild("ThrustBar").SetActive(false);
                Exosuit.FindChild("ThrustBarBorder").SetActive(false);
                Exosuit.transform.position = new Vector3(0.7809f, -0.3501f, 1);
                Background.GetComponent<Image>().sprite = InfinitePrawnJump_SN.BePinEx.assetBundle.LoadAsset<Sprite>("ExosuitBackground");
            }
        }
    }
}
