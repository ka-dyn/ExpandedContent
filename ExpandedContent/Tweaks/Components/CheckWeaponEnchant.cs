using System;
using System.Linq;
using Kingmaker.Items;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Enums;
using Kingmaker.UI.Models.Log;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.Utility;
using static Kingmaker.EntitySystem.EntityDataBase;
using Kingmaker.Blueprints.Items.Weapons;

namespace ExpandedContent.Tweaks.Components {
    [AllowedOn(typeof(BlueprintAbility))]
    [ComponentName("Check if weapon has enchant.")]
    public class CheckWeaponEnchant : BlueprintComponent, IAbilityCasterRestriction {

        public bool IncludeStowedWeapons = false;
        public BlueprintItemEnchantmentReference m_Enchantment;
        public bool IsCasterRestrictionPassed(UnitEntityData caster) {
            if (!IncludeStowedWeapons) {
                if (caster.Body.PrimaryHand.HasWeapon || caster.Body.SecondaryHand.HasWeapon) { //Must have weapon in hand
                    return caster.Body.PrimaryHand.Weapon.Blueprint.Enchantments.Contains(m_Enchantment) || caster.Body.SecondaryHand.Weapon.Blueprint.Enchantments.Contains(m_Enchantment); //Weapon in hand must have m_Enchantment
                }
                return false;
            }
            return caster.Body.m_HandsEquipmentSets.Any(HandsEquipmentSet => //Weapon in any equipment set must have m_Enchantment
                HandsEquipmentSet.PrimaryHand.Weapon.Blueprint.Enchantments.Contains(m_Enchantment) || 
                HandsEquipmentSet.SecondaryHand.Weapon.Blueprint.Enchantments.Contains(m_Enchantment)
                );            
        }
        public string GetAbilityCasterRestrictionUIText() {
            if (IncludeStowedWeapons) {
                return $"You must have a weapon on your person with this enchant to transfer.";
            }
            return $"You must have a weapon in hand with this enchant to transfer.";
        }
    }
}
