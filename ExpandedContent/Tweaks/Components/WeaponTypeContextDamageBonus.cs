using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Serialization;
using UnityEngine;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Enums;

namespace ExpandedContent.Tweaks.Components {
    [AllowedOn(typeof(BlueprintUnitFact), false)]
    [AllowMultipleComponents]
    [ComponentName("Context damage bonus on weapon type")]
    public class WeaponTypeContextDamageBonus : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateWeaponStats>, IRulebookHandler<RuleCalculateWeaponStats>, ISubscriber, IInitiatorRulebookSubscriber {


        public WeaponCategory[] Categories;
        public ContextValue Value;
        public bool ExceptForCategories;
        public ModifierDescriptor Descriptor = ModifierDescriptor.UntypedStackable;

        public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt) {
            bool flag = false;
            int bonus = Value.Calculate(Fact.MaybeContext);
            if (evt.Weapon != null) {
                WeaponCategory[] categories = Categories;
                foreach (WeaponCategory weaponCategory in categories) {
                    if (evt.Weapon.Blueprint.Category == weaponCategory) {
                        flag = true;
                    }
                }
            }
            if ((flag && !ExceptForCategories) || (!flag && ExceptForCategories)) {
                evt.AddDamageModifier(bonus * base.Fact.GetRank(), base.Fact);
            }
        }

        public void OnEventDidTrigger(RuleCalculateWeaponStats evt) {
        }
    }
}
