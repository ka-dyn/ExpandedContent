using System;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Mechanics;

namespace ExpandedContent.Tweaks.Components {
    //Test
    [ComponentName("Attack roll bonus against all units but the caster")]
    [AllowedOn(typeof(BlueprintBuff))]

    public class AttackBonusAgainstNotCaster : UnitBuffComponentDelegate, IInitiatorRulebookHandler<RuleCalculateAttackBonus>, IRulebookHandler<RuleCalculateAttackBonus>, ISubscriber, IInitiatorRulebookSubscriber {
        public ModifierDescriptor Descriptor;
        public ContextValue Value;
        public void OnEventAboutToTrigger(RuleCalculateAttackBonus evt) {
            if (base.Buff.Context.MaybeCaster != null && evt.Target != base.Buff.Context.MaybeCaster) {
                evt.AddModifier(this.Value.Calculate(base.Buff.Context), base.Fact, this.Descriptor);
            }
        }
        public void OnEventDidTrigger(RuleCalculateAttackBonus evt) {
        }
    }
}
