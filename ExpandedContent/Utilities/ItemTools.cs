using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Components;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Enums;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System.Collections.Generic;
using UnityEngine;
using ExpandedContent.Extensions;



namespace ExpandedContent.Utilities {
    public static class ItemTools {

        //Taken from TTT-Core        
        public enum PotionColor : int {
            Blue,
            Cyan,
            Green,
            Red,
            Yellow,
        }       

        private static Sprite Form01_Blue_Simple = AssetLoader.LoadInternal("Potions", "Form01_Blue_Simple.png");
        private static Sprite Form03_Blue_Simple = AssetLoader.LoadInternal("Potions", "Form03_Blue_Simple.png");
        private static Sprite Form04_Blue_Simple = AssetLoader.LoadInternal("Potions", "Form04_Blue_Simple.png");
        private static Sprite Form05_Blue_Simple = AssetLoader.LoadInternal("Potions", "Form05_Blue_Simple.png");
        private static Sprite Form06_Blue_Simple = AssetLoader.LoadInternal("Potions", "Form06_Blue_Simple.png");

        private static Sprite Form01_Cyan_Simple = AssetLoader.LoadInternal("Potions", "Form01_Cyan_Simple.png");
        private static Sprite Form03_Cyan_Simple = AssetLoader.LoadInternal("Potions", "Form03_Cyan_Simple.png");
        private static Sprite Form04_Cyan_Simple = AssetLoader.LoadInternal("Potions", "Form04_Cyan_Simple.png");
        private static Sprite Form05_Cyan_Simple = AssetLoader.LoadInternal("Potions", "Form05_Cyan_Simple.png");
        private static Sprite Form06_Cyan_Simple = AssetLoader.LoadInternal("Potions", "Form06_Cyan_Simple.png");

        private static Sprite Form01_Green_Simple = AssetLoader.LoadInternal("Potions", "Form01_Green_Simple.png");
        private static Sprite Form03_Green_Simple = AssetLoader.LoadInternal("Potions", "Form03_Green_Simple.png");
        private static Sprite Form04_Green_Simple = AssetLoader.LoadInternal("Potions", "Form04_Green_Simple.png");
        private static Sprite Form05_Green_Simple = AssetLoader.LoadInternal("Potions", "Form05_Green_Simple.png");
        private static Sprite Form06_Green_Simple = AssetLoader.LoadInternal("Potions", "Form06_Green_Simple.png");

        private static Sprite Form01_Red_Simple = AssetLoader.LoadInternal("Potions", "Form01_Red_Simple.png");
        private static Sprite Form03_Red_Simple = AssetLoader.LoadInternal("Potions", "Form03_Red_Simple.png");
        private static Sprite Form04_Red_Simple = AssetLoader.LoadInternal("Potions", "Form04_Red_Simple.png");
        private static Sprite Form05_Red_Simple = AssetLoader.LoadInternal("Potions", "Form05_Red_Simple.png");
        private static Sprite Form06_Red_Simple = AssetLoader.LoadInternal("Potions", "Form06_Red_Simple.png");

        private static Sprite Form01_Yellow_Simple = AssetLoader.LoadInternal("Potions", "Form01_Yellow_Simple.png");
        private static Sprite Form03_Yellow_Simple = AssetLoader.LoadInternal("Potions", "Form03_Yellow_Simple.png");
        private static Sprite Form04_Yellow_Simple = AssetLoader.LoadInternal("Potions", "Form04_Yellow_Simple.png");
        private static Sprite Form05_Yellow_Simple = AssetLoader.LoadInternal("Potions", "Form05_Yellow_Simple.png");
        private static Sprite Form06_Yellow_Simple = AssetLoader.LoadInternal("Potions", "Form06_Yellow_Simple.png");
        
        public static BlueprintItemEquipmentUsable CreateScroll(string name, Sprite icon, BlueprintAbility spell, int spellLevel, int casterLevel) {
            var scrollItemPrefab = new PrefabLink() {
                AssetId = "d711efe72d029364a9ad378d5f0955c0"
            };
            var Scroll = Helpers.CreateBlueprint<BlueprintItemEquipmentUsable>(name, bp => {
                bp.m_InventoryEquipSound = "ScrollPut";
                bp.m_BeltItemPrefab = scrollItemPrefab;
                bp.m_Enchantments = new BlueprintEquipmentEnchantmentReference[0];
                bp.Type = UsableItemType.Scroll;
                bp.m_Ability = spell.ToReference<BlueprintAbilityReference>();
                bp.m_ActivatableAbility = new BlueprintActivatableAbilityReference();
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.CasterLevel = casterLevel;
                bp.SpellLevel = spellLevel;
                bp.DC = 10 + spellLevel + (spellLevel / 2);
                bp.m_DisplayNameText = Helpers.CreateString($"{bp.name}.Name", "");
                bp.m_DescriptionText = Helpers.CreateString($"{bp.name}.Description", "");
                bp.m_FlavorText = Helpers.CreateString($"{bp.name}.Flavor", "");
                bp.m_NonIdentifiedNameText = Helpers.CreateString($"{bp.name}.Unidentified_Name", "");
                bp.m_NonIdentifiedDescriptionText = Helpers.CreateString($"{bp.name}.Unidentified_Description", "");
                bp.m_Icon = icon;
                bp.m_Cost = GetScrollCost(spellLevel);
                bp.m_Weight = 0.2f;
                bp.m_Destructible = true;
                bp.m_ShardItem = Resources.GetBlueprintReference<BlueprintItemReference>("08133117418642fb9d1d2adba9785f43"); //PaperShardItem
                bp.m_InventoryPutSound = "ScrollPut";
                bp.m_InventoryTakeSound = "ScrollTake";
                bp.TrashLootTypes = new TrashLootType[] { TrashLootType.Scrolls_RE };
                bp.m_Overrides = new List<string>();
                bp.AddComponent<CopyScroll>(c => {
                    c.m_CustomSpell = spell.ToReference<BlueprintAbilityReference>();
                });
            });
            AddScrollToCraftRoot(Scroll);
            return Scroll;
        }
        public static BlueprintItemEquipmentUsable CreatePotion(string name, PotionColor color, BlueprintAbility spell, int spellLevel, int casterLevel) {
            var Potion = Helpers.CreateBlueprint<BlueprintItemEquipmentUsable>(name, bp => {
                bp.m_InventoryEquipSound = "ScrollPut";
                bp.m_BeltItemPrefab = GetPotionPrefab(color);
                bp.m_Enchantments = new BlueprintEquipmentEnchantmentReference[0];
                bp.Type = UsableItemType.Potion;
                bp.m_Ability = spell.ToReference<BlueprintAbilityReference>();
                bp.m_ActivatableAbility = new BlueprintActivatableAbilityReference();
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.CasterLevel = casterLevel;
                bp.SpellLevel = spellLevel;
                bp.DC = 10 + spellLevel + (spellLevel / 2);
                bp.m_DisplayNameText = Helpers.CreateString($"{bp.name}.Name", "");
                bp.m_DescriptionText = Helpers.CreateString($"{bp.name}.Description", "");
                bp.m_FlavorText = Helpers.CreateString($"{bp.name}.Flavor", "");
                bp.m_NonIdentifiedNameText = Helpers.CreateString($"{bp.name}.Unidentified_Name", "");
                bp.m_NonIdentifiedDescriptionText = Helpers.CreateString($"{bp.name}.Unidentified_Description", "");
                bp.m_Icon = GetPotionIcon(color, spellLevel);
                bp.m_Cost = GetPotionCost(spellLevel);
                bp.m_Weight = 0.5f;
                bp.m_Destructible = true;
                bp.m_ShardItem = Resources.GetBlueprintReference<BlueprintItemReference>("2b2107f98002425bb1309d31ff531f37"); //GlassShardItem
                bp.m_InventoryPutSound = "BottlePut";
                bp.m_InventoryTakeSound = "BottleTake";
                bp.TrashLootTypes = new TrashLootType[] { TrashLootType.Potions };
                bp.m_Overrides = new List<string>();
            });
            AddPotionToCraftRoot(Potion);
            return Potion;
        }
        private static int GetScrollCost(int spellLevel) {
            return spellLevel switch {
                0 => 13,
                1 => 25,
                2 => 150,
                3 => 375,
                4 => 700,
                5 => 1125,
                6 => 1650,
                7 => 2275,
                8 => 3000,
                9 => 3825,
                10 => 5000,
                _ => 0
            };
        }
        private static void AddScrollToCraftRoot(BlueprintItemEquipmentUsable scroll) {
            if (scroll.Type != UsableItemType.Scroll) { return; }

            Game.Instance.BlueprintRoot.CraftRoot.m_ScrollsItems.Add(scroll.ToReference<BlueprintItemEquipmentUsableReference>());
        }
        private static PrefabLink GetPotionPrefab(PotionColor color) {
            var AssetID = color switch {
                PotionColor.Blue => "7b2a2ed1f3284224c804038a713c391f",
                PotionColor.Cyan => "e805c0e867b583b4f8c24b2b045b5be3",
                PotionColor.Green => "51097fd1d322c0d41b33dac27da51bf4",
                PotionColor.Red => "8de60d0edae1a1a47ba9fee1e1d97e32",
                PotionColor.Yellow => "9b57d6e56c83fc14d9580c6f766fbe20",
                _ => "9b57d6e56c83fc14d9580c6f766fbe20"
            };
            return new PrefabLink() {
                AssetId = AssetID
            };
        }
        private static Sprite GetPotionIcon(PotionColor color, int spellLevel) {
            return color switch {
                PotionColor.Blue => spellLevel switch {
                    1 => Form01_Blue_Simple,
                    2 => Form03_Blue_Simple,
                    3 => Form04_Blue_Simple,
                    4 => Form05_Blue_Simple,
                    5 => Form06_Blue_Simple,
                    _ => Form06_Blue_Simple
                },
                PotionColor.Cyan => spellLevel switch {
                    1 => Form01_Cyan_Simple,
                    2 => Form03_Cyan_Simple,
                    3 => Form04_Cyan_Simple,
                    4 => Form05_Cyan_Simple,
                    5 => Form06_Cyan_Simple,
                    _ => Form06_Cyan_Simple
                },
                PotionColor.Green => spellLevel switch {
                    1 => Form01_Green_Simple,
                    2 => Form03_Green_Simple,
                    3 => Form04_Green_Simple,
                    4 => Form05_Green_Simple,
                    5 => Form06_Green_Simple,
                    _ => Form06_Green_Simple
                },
                PotionColor.Red => spellLevel switch {
                    1 => Form01_Red_Simple,
                    2 => Form03_Red_Simple,
                    3 => Form04_Red_Simple,
                    4 => Form05_Red_Simple,
                    5 => Form06_Red_Simple,
                    _ => Form06_Red_Simple
                },
                PotionColor.Yellow => spellLevel switch {
                    1 => Form01_Yellow_Simple,
                    2 => Form03_Yellow_Simple,
                    3 => Form04_Yellow_Simple,
                    4 => Form05_Yellow_Simple,
                    5 => Form06_Yellow_Simple,
                    _ => Form06_Yellow_Simple
                },
                _ => Form01_Blue_Simple
            };
        }
        private static int GetPotionCost(int spellLevel) {
            return spellLevel switch {
                0 => 25,
                1 => 50,
                2 => 300,
                3 => 750,
                4 => 1400,
                5 => 2250,
                6 => 3300,
                _ => 25
            };
        }
        private static void AddPotionToCraftRoot(BlueprintItemEquipmentUsable potion) {
            if (potion.Type != UsableItemType.Potion) { return; }

            Game.Instance.BlueprintRoot.CraftRoot.m_PotionsItems.Add(potion.ToReference<BlueprintItemEquipmentUsableReference>());
        }
    }
}
