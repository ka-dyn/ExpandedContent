using System;
using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Enums;
using Kingmaker.UI.Models.Log;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.Base;

namespace ExpandedContent.Tweaks.Components {
    [AllowedOn(typeof(BlueprintAbility))]
    [ComponentName("Need weapon type in offhand to use ability")]
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
