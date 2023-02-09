using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using SMLHelper.V2.Options.Attributes;
using System.Reflection;
using System.IO;
using SMLHelper.V2.Json;
using SMLHelper.V2.Handlers;
using System.Collections;

namespace SeamothWarpModule_SN
{
    [BepInPlugin(myGUID, pluginName, versionString)]
    public class BepInEx : BaseUnityPlugin
    {
        public static AssetBundle assetBundle = AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "warpassets"));

        private const string myGUID = "com.JackLog.Warp.Module";
        private const string pluginName = "Seamoth Warp Module SN";
        private const string versionString = "1.0.0";

        private static readonly Harmony harmony = new Harmony(myGUID);

        public static ManualLogSource logger;

        public static float Distance = 30;

        private void Awake()
        {
            harmony.PatchAll();
            Logger.LogInfo(pluginName + " " + versionString + " " + "loaded.");
            logger = Logger;

            SeamothModuleItem item = new SeamothModuleItem();
            item.Patch();
        }

        public static FMODAsset GetFmodAsset(string audioPath)
        {
            FMODAsset asset = ScriptableObject.CreateInstance<FMODAsset>();
            asset.path = audioPath;
            return asset;
        }

        public static IEnumerator GetHoloMaterial(IOut<Material> @out)
        {
            UWE.IPrefabRequest task = UWE.PrefabDatabase.GetPrefabAsync("4e372190-a687-49bf-a89a-d134c949546e");
            yield return task;
            task.TryGetPrefab(out GameObject pf);
            GameObject go;
            go = Instantiate<GameObject>(pf, Vector3.zero, Quaternion.identity, true);

            yield return new WaitForSecondsRealtime(5);
            MeshRenderer[] renderers = go.FindChild("hologram").FindChild("worlddisplay").FindChild("wireframeworld").FindChild("MiniWorld").GetAllComponentsInChildren<MeshRenderer>();

            @out.Set(renderers[0].material);
        }

        /*public static IEnumerator GetMaterialFromClassIDAsync(string classId, IOut<Material> material, int rendererIndex = 0)
        {
            material = null;

            UWE.IPrefabRequest task = UWE.PrefabDatabase.GetPrefabAsync(classId);
            yield return task;
            task.TryGetPrefab(out GameObject forceFieldPrefab);

            MeshRenderer[] meshRenderers = forceFieldPrefab.GetAllComponentsInChildren<MeshRenderer>();

            if (meshRenderers != null)
                Debug.LogWarning("Loacted mesh renderers.");

            material.Set(meshRenderers[rendererIndex].material);
        } */
    }
}