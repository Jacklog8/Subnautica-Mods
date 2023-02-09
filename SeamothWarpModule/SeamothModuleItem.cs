using SMLHelper.V2.Assets;
using UnityEngine;
using SMLHelper.V2.Crafting;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

namespace SeamothWarpModule_SN
{
    internal class SeamothModuleItem : Equipable
    {
        public static TechType ModuleTechtype;

        public SeamothModuleItem() : base("SeamothWarp", "Warp module", "Reuses Warper technology to allow the user to warp forward every once in a while.") 
        {
            OnFinishedPatching += () =>
            {
                ModuleTechtype = TechType;
            };
        }

        public override TechCategory CategoryForPDA => TechCategory.VehicleUpgrades;
        public override CraftTree.Type FabricatorType => CraftTree.Type.SeamothUpgrades;
        public override TechGroup GroupForPDA => TechGroup.VehicleUpgrades;
        public override TechType RequiredForUnlock => TechType.PrecursorLostRiverWarperParts;
        public override Vector2int SizeInInventory => new Vector2int(1, 1);
        public override QuickSlotType QuickSlotType => QuickSlotType.Selectable;
        public override float CraftingTime => 3;
        public override string[] StepsToFabricatorTab => new string[] { "SeamothModules" };
        public override EquipmentType EquipmentType => EquipmentType.SeamothModule;

        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,

                Ingredients = new List<Ingredient>(new Ingredient[]
                    {
                        new Ingredient(TechType.PrecursorIonCrystal, 3),
                        new Ingredient(TechType.TitaniumIngot, 1),
                        new Ingredient(TechType.PrecursorKey_Purple, 1),
                        new Ingredient(TechType.Kyanite, 2)
                    }
                )
            };
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            Atlas.Sprite sprite = new Atlas.Sprite(BepInEx.assetBundle.LoadAsset<Sprite>("Icon"), true);

            return sprite;

        }
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            yield return CraftData.InstantiateFromPrefabAsync(TechType.VehicleHullModule1, gameObject);
        }
    }
}
