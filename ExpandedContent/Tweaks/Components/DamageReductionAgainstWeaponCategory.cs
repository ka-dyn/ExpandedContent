using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Components {

    [ComponentName("Damage Reduction but smaller")]
    [AllowedOn(typeof(BlueprintUnitFact), false)]

    public class DamageReductionAgainstWeaponCategory : UnitFactComponentDelegate, ITargetRulebookHandler<RuleCalculateDamage>, IRulebookHandler<RuleCalculateDamage>, ISubscriber, ITargetRulebookSubscriber {
        public WeaponCategory[] Categories;
        public int Reduction;

        public void OnEventAboutToTrigger(RuleCalculateDamage evt) {
            if (evt.DamageBundle.Weapon == null || evt.DamageBundle.WeaponDamage == null || !Categories.Contains(evt.DamageBundle.Weapon.Blueprint.Category) || !(evt.Target == base.Owner)) {
                return;
            }

            foreach (BaseDamage item in evt.DamageBundle) {
                item.SetReductionBecauseResistance(Reduction, base.Fact);
            }
        }

        public void OnEventDidTrigger(RuleCalculateDamage evt) {
        }


    }
}
