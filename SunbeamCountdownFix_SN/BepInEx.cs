using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using System.Reflection;
using System.IO;

namespace SunbeamCountdownFix_SN
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    public class BepInEx : BaseUnityPlugin
    {
        private const string myGUID = "com.JackLog.Sunbeamtimer";
        private const string pluginName = "Sunbeam Timer SN";
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