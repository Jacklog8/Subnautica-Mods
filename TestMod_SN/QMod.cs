using System.Reflection;
using HarmonyLib;
using QModManager.API.ModLoading;
using Logger = QModManager.Utility.Logger;
using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;
using SMLHelper.V2.Handlers;
using System;
using UnityStandardAssets;
using System.IO;
using UnityEngine;

namespace TestMod_SN
{
    [QModCore]
    public static class QMod
    {

        public static string modPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string assetsPath = Path.Combine(modPath, "Assets");
        internal static Config config { get; } = OptionsPanelHandler.Main.RegisterModOptions<Config>();
        internal static AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(assetsPath, "fidgetspinner"));

        [QModPatch]
        public static void Patch()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var modName = ($"AcE1337_{assembly.GetName().Name}");
            Logger.Log(Logger.Level.Info, $"Patching {modName}");
            Harmony harmony = new Harmony(modName);
            harmony.PatchAll(assembly);
            Logger.Log(Logger.Level.Info, "Patched successfully!");
            if (assetBundle.Contains("fidgetmesh.prefab"))
                Logger.Log(Logger.Level.Info, "Found fidgetspinner.prefab!");
            else
                Logger.Log(Logger.Level.Info, "Could not find fidgetspinner.prefab");

            var fidgetspinner = new FidgetSpinner();
            fidgetspinner.Patch();
        }

        [Menu("Knife Damage")]
        public class Config : ConfigFile
        {
            [Slider("Knife damage modifier", Format = "{0:F2}", Min = 0f, Max = 10f, DefaultValue = 1f, Step = 0.05f)]
            public float KnifeModifier = 1f;

            [Toggle("Remove Reaper leviathan")]
            public bool RemoveReaper = false;
        }
    }
}