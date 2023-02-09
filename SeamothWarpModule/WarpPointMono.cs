using UnityEngine;

namespace SeamothWarpModule_SN
{
    internal class WarpPointMono : MonoBehaviour
    {
        public static Transform tpTransform;
        Vector3 centerOffset = new Vector3(0, 10, 0);
        static MeshRenderer renderer = null;
        public Transform seamoth;
        static GameObject model = null;

        void Update()
        {
            RaycastHit raycast;
            if (Physics.Raycast(seamoth.position + seamoth.forward, seamoth.forward, out raycast, BepInEx.Distance, -1, QueryTriggerInteraction.Ignore))
                tpTransform.position = Vector3.Lerp(tpTransform.position, raycast.point - seamoth.forward * 2, 0.1f);
            else tpTransform.position = Vector3.Lerp(tpTransform.position, seamoth.position + seamoth.forward * BepInEx.Distance, 0.1f);
            tpTransform.rotation = Quaternion.Slerp(tpTransform.rotation, seamoth.rotation, 0.04f);

            transform.position = tpTransform.position;
            transform.rotation = tpTransform.rotation;

            model.transform.localScale = new Vector3(82.69877616f, 82.69877616f, 82.69877616f);
            model.transform.localPosition = Vector3.zero;

            renderer.material.SetVector(ShaderPropertyID._MapCenterWorldPos, transform.position + centerOffset);
            renderer.material.SetFloat(ShaderPropertyID._FadeRadius, 100);
            renderer.material.SetFloat(ShaderPropertyID._NoiseStr, 4);
            renderer.material.SetColor(ShaderPropertyID._Color, new Color(0.1f, 0.5f, 0.5f, 1));
        }

        void Start()
        {
            model = Instantiate<GameObject>(GameObject.Find("Submersible_SeaMoth_geo"), transform);
            Mesh mesh = model.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            Destroy(model.GetComponent<SkinnedMeshRenderer>());
            renderer = model.EnsureComponent<MeshRenderer>();
            MeshFilter filter = model.EnsureComponent<MeshFilter>();
            filter.mesh = mesh;
            renderer.material = SeamothPatch.mat;

            tpTransform = transform;
        }

        public static void Kill()
        {
            if (FindObjectOfType<WarpPointMono>())
                Destroy(FindObjectOfType<WarpPointMono>().gameObject);
        }
    }
}
