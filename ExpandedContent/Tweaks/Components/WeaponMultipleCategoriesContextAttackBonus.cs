using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Components {
    [ComponentName("Context attack bonus on multiple weapon types")]
    [AllowedOn(typeof(BlueprintUnitFact))]

    public class WeaponMultipleCategoriesContextAttackBonus : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateAttackBonusWithoutTarget>, IRulebookHandler<RuleCalculateAttackBonusWithoutTarget>, ISubscriber, IInitiatorRulebookSubscriber {

        public WeaponCategory[] Categories;
        public ContextValue Value;
        public bool ExceptForCategories;
        public ModifierDescriptor Descriptor = ModifierDescriptor.UntypedStackable;

        public void OnEventAboutToTrigger(RuleCalculateAttackBonusWithoutTarget evt) {
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
                evt.AddModifier(bonus * Fact.GetRank(), base.Fact, Descriptor);
            }
        }

        public void OnEventDidTrigger(RuleCalculateAttackBonusWithoutTarget evt) {
        }
    }
}
