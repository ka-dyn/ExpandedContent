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
            var RiverFoxPendantItem = Resources.GetBlueprint<BlueprintItemEquipmentNeck>("169fd99b68c1dd04692c2368a7b957b7");
            var Jeweler_Chapter3VendorTable = Resources.GetBlueprint<BlueprintSharedVendorTable>("9f959fcff8d929042b1be6311d209580");
            var DLC3_Tier1VendorTableMagic = Resources.GetBlueprint<BlueprintSharedVendorTable>("fac2be1b911745a588b5afcc19c0e184");
            var DLC3_Tier2VendorTableMagic = Resources.GetBlueprint<BlueprintSharedVendorTable>("6176737df85b41fea25e13b67fe557b8"); 

            var AmuletEarlyRider = Helpers.CreateBlueprint<BlueprintItemEquipmentNeck>("AmuletEarlyRider", bp => {
                bp.SetName("Early Rider");
                bp.SetDescription("This amulet allows the wearer to cast Reduce Person as a 6thlevel wizard once pey day.");
                bp.SetFlavorText("A amulet common to those who raise animals for riding, allowing them to start training early.");
                bp.m_Icon = RiverFoxPendantItem.Icon;
                bp.m_Cost = 600;
                bp.m_Weight = 0.1f;
                bp.m_IsNotable = false;
                bp.m_ForceStackable = false;
                bp.m_Destructible = true;
                bp.m_ShardItem = RiverFoxPendantItem.m_ShardItem;
                bp.m_MiscellaneousType = BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = RiverFoxPendantItem.InventoryPutSound;
                bp.m_InventoryTakeSound = RiverFoxPendantItem.InventoryTakeSound;
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
                bp.m_InventoryEquipSound = RiverFoxPendantItem.m_InventoryEquipSound;
            });

            Jeweler_Chapter3VendorTable.AddComponent<LootItemsPackFixed>(c => {
                c.m_Item = new LootItem() {
                    m_Type = LootItemType.Item,
                    m_Item = AmuletEarlyRider.ToReference<BlueprintItemReference>(),
                    m_Loot = new BlueprintUnitLootReference()
                };
                c.m_Count = 6;
            });
            DLC3_Tier1VendorTableMagic.AddComponent<LootItemsPackFixed>(c => {
                c.m_Item = new LootItem() {
                    m_Type = LootItemType.Item,
                    m_Item = AmuletEarlyRider.ToReference<BlueprintItemReference>(),
                    m_Loot = new BlueprintUnitLootReference()
                };
                c.m_Count = 6;
            });
            DLC3_Tier2VendorTableMagic.AddComponent<LootItemsPackFixed>(c => {
                c.m_Item = new LootItem() {
                    m_Type = LootItemType.Item,
                    m_Item = AmuletEarlyRider.ToReference<BlueprintItemReference>(),
                    m_Loot = new BlueprintUnitLootReference()
                };
                c.m_Count = 6;
            });

        }
    }
}
