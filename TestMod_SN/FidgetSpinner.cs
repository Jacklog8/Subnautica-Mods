using SMLHelper.V2.Assets;
using SMLHelper.V2.Crafting;
using SMLHelper.V2.Utility;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using Sprite = Atlas.Sprite;
using TechData = SMLHelper.V2.Crafting.TechData;

namespace TestMod_SN
{
    internal class FidgetSpinner:Equipable
    {
        private static GameObject processedPrefab;

        public FidgetSpinner() : base("FidgetSpinner", "Fidget spinner", "\"Oh but AcE why would you add a fidget spinner to Subnautica isnt that really unnessecary.\" Says the nerd whom nobody likes. Well, you nerd, i simply do not care. infact there is a very slim chance that any human at all ever will read this so suck it you probably arent even real. And besides, i didnt ask for your opinion so frick off you fockin degenerate. Infact fidget spinner go brrrr!") {}
        public override TechGroup GroupForPDA => TechGroup.Personal;
        public override TechCategory CategoryForPDA => TechCategory.Equipment;
        public override QuickSlotType QuickSlotType => QuickSlotType.None;
        public override EquipmentType EquipmentType => EquipmentType.Hand;
        public override CraftTree.Type FabricatorType => CraftTree.Type.Fabricator;
        public override string[] StepsToFabricatorTab => new[] { "Personal", "Tools" };

        public override GameObject GetGameObject()
        {
            if (processedPrefab == null)
            {
                var prefab = QMod.assetBundle.LoadAsset<GameObject>("fidgetmesh.prefab");
                var gameObject = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);

                gameObject.AddComponent<PrefabIdentifier>().classId = this.ClassID; // makes the item recognizable by the game
                gameObject.AddComponent<TechTag>().type = this.TechType; // explicit scanning techtype. Optional
                gameObject.AddComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Medium; // to make the item save
                gameObject.AddComponent<Pickupable>();
                var renderers = gameObject.GetAllComponentsInChildren<Renderer>();
                renderers.ForEach(r => r.materials.ForEach(m => m.shader = Shader.Find("MarmosetUBER"))); // changes all materials of your object to MarmosetUBER shader
                gameObject.AddComponent<SkyApplier>().renderers = renderers; // to make the item recieve biome lighting

                processedPrefab = gameObject;
            }
            return processedPrefab;
        }

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>(new Ingredient[]
                    {
                        new Ingredient(TechType.Titanium, 2),
                    }
                )
            };
        }

        protected override Sprite GetItemSprite()
        {
            return ImageUtils.LoadSpriteFromFile(Path.Combine(QMod.assetsPath, "FidgetSpinner.png"));
        }
    }
}
