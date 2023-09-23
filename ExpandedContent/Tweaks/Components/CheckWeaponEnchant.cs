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
using Kingmaker.Designers;

namespace ExpandedContent.Tweaks.Components {
    [AllowedOn(typeof(BlueprintAbility))]
    [ComponentName("Check if weapon has enchant.")]
    public class CheckWeaponEnchant : BlueprintComponent, IAbilityCasterRestriction {

        public bool IncludeStowedWeapons = false;
        public BlueprintItemEnchantmentReference m_Enchantment;


        public bool IsCasterRestrictionPassed(UnitEntityData caster) {
            if (caster.Body.PrimaryHand.HasWeapon || caster.Body.SecondaryHand.HasWeapon) { //Must have weapon in hand
                return caster.Body.PrimaryHand.Weapon.Blueprint.Enchantments.Contains(m_Enchantment) || caster.Body.SecondaryHand.Weapon.Blueprint.Enchantments.Contains(m_Enchantment); //Weapon in hand must have m_Enchantment
            }
            return false;
        }
        public string GetAbilityCasterRestrictionUIText() {            
            return $"You must have a weapon in hand with this enchant to transfer.";
        }
    }
}
