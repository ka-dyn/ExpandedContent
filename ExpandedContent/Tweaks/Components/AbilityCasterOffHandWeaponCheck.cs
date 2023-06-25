using System;
using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UI.Models.Log;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Mechanics;

namespace ExpandedContent.Tweaks.Components {
    [AllowedOn(typeof(BlueprintAbility))]
    [TypeId("Need weapon type in offhand to use ability")]
    public class AbilityCasterOffHandWeaponCheck : BlueprintComponent, IAbilityCasterRestriction {
        public WeaponCategory[] Category;

        public bool IsCasterRestrictionPassed(UnitEntityData caster) {
            if (caster.Body.SecondaryHand.HasWeapon) {
                return Category.Contains(caster.Body.SecondaryHand.Weapon.Blueprint.Type.Category);
            }

            return false;
        }

        public string GetAbilityCasterRestrictionUIText() {
            string categories = string.Join(", ", Category.Select(LocalizedTexts.Instance.WeaponCategories.GetText));
            return LocalizedTexts.Instance.Reasons.SpecificWeaponRequired.ToString(delegate {
                GameLogContext.Text = categories;
            });
        }
    }
}
