using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using System.Reflection;
using System.IO;

namespace InfinitePrawnJump_SN
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    public class BePinEx : BaseUnityPlugin
    {
        public static string modPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string assetsPath = Path.Combine(modPath, "Assets");
        public static AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(assetsPath, "exosuitbg"));

        private const string myGUID = "com.JackLog.PrawnJump";
        private const string pluginName = "Prawn Jump SN";
        private const string versionString = "1.0.0";

        private static readonly Harmony harmony = new Harmony(myGUID);

        public static ManualLogSource logger;

        private void Awake()
        {
            harmony.PatchAll();
            Logger.LogInfo(pluginName + " " + versionString + " " + "loaded.");
            logger = Logger;
        }
    }
}