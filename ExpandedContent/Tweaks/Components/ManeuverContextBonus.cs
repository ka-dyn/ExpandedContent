using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Enums;
using Kingmaker.Utility;


namespace ExpandedContent.Tweaks.Components {
    [AllowedOn(typeof(BlueprintUnitFact), false)]
    [AllowMultipleComponents]
    [ComponentName("Maneuver bonus with context input")]
    public class ManeuverContextBonus : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateCMB>, IRulebookHandler<RuleCalculateCMB>, ISubscriber, IInitiatorRulebookSubscriber {
        public CombatManeuver Type;        
        public ContextValue Value;
        public ModifierDescriptor Descriptor = ModifierDescriptor.UntypedStackable;

        public void OnEventAboutToTrigger(RuleCalculateCMB evt) {

            if (evt.Type == Type) {
                int bonus = Value.Calculate(Fact.MaybeContext);
                evt.AddModifier(bonus, base.Fact, Descriptor);
            }
        }

        public void OnEventDidTrigger(RuleCalculateCMB evt) {
        }
    }
}
