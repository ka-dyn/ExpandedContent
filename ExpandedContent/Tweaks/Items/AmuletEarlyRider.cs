using BlueprintCore.Blueprints.Configurators.AI;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints.Loot;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Items {
    internal class AmuletEarlyRider {
        public static void AddAmuletEarlyRider() {

            var ReducePerson = Resources.GetBlueprint<BlueprintAbility>("4e0e9aba6447d514f88eff1464cc4763");
            var RiverFoxPendantIcon = AssetLoader.LoadInternal("Items", "Icon_RiverFoxsPendant.png");

            var MetalShardItem = Resources.GetBlueprintReference<BlueprintItemReference>("e6820e62423d4c81a2ba20d236251b67");
            var Jeweler_Chapter3VendorTable = Resources.GetBlueprint<BlueprintSharedVendorTable>("9f959fcff8d929042b1be6311d209580");
            var DLC3_VendorTable_Equipment = Resources.GetBlueprint<BlueprintSharedVendorTable>("195579adaa20483ca3aad66bb2b06f8f");

            var AmuletEarlyRider = Helpers.CreateBlueprint<BlueprintItemEquipmentNeck>("AmuletEarlyRider", bp => {
                bp.SetName("Early Rider");
                bp.SetDescription("This amulet allows the wearer to cast Reduce Person as a 6th level wizard once pey day.");
                bp.SetFlavorText("A amulet common to those who raise animals for riding, allowing them to start training early.");
                bp.m_Icon = RiverFoxPendantIcon;
                bp.m_Cost = 600;
                bp.m_Weight = 0.1f;
                bp.m_IsNotable = false;
                bp.m_ForceStackable = false;
                bp.m_Destructible = true;
                bp.m_ShardItem = MetalShardItem;
                bp.m_MiscellaneousType = BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = "AmuletPut";
                bp.m_InventoryTakeSound = "AmuletTake";
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = new TrashLootType[0];
                bp.CR = 10;
                bp.m_Ability = ReducePerson.ToReference<BlueprintAbilityReference>();
                bp.m_ActivatableAbility = null;
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = true;
                bp.CasterLevel = 6;
                bp.SpellLevel = 1;
                bp.DC = 12;
                bp.IsNonRemovable = false;
                bp.m_EquipmentEntity = null;
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.m_Enchantments = new BlueprintEquipmentEnchantmentReference[0];
                bp.m_InventoryEquipSound = "AmuletPut";
            });

            Jeweler_Chapter3VendorTable.AddComponent<LootItemsPackFixed>(c => {
                c.m_Item = VenderTools.CreateLootItem(AmuletEarlyRider);
                c.m_Count = 6;
            });
            DLC3_VendorTable_Equipment.AddComponent<LootItemsPackFixed>(c => {
                c.m_Item = VenderTools.CreateLootItem(AmuletEarlyRider);
                c.m_Count = 6;
            });

        }
    }
}
