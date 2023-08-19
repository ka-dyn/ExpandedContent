using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics;

namespace ExpandedContent.Tweaks.Components {

    [AllowMultipleComponents]
    [ComponentName("Saving throw bonus against fact from caster")]
    [AllowedOn(typeof(BlueprintUnitFact))]
    public class SavingThrowBonusAgainstCaster : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleSavingThrow>, IRulebookHandler<RuleSavingThrow>, ISubscriber, IInitiatorRulebookSubscriber {
        public ModifierDescriptor Descriptor;
        public ContextValue Value;
        public bool Reflex = true;
        public bool Fortitude = true;
        public bool Will = true;
        public void OnEventAboutToTrigger(RuleSavingThrow evt) {
            if (evt.Reason.Caster != Fact.MaybeContext?.MaybeCaster) {
                return;
            }
            int bonus = Value.Calculate(Fact.MaybeContext);            
            if (Reflex) {
                evt.AddTemporaryModifier(evt.Initiator.Stats.SaveReflex.AddModifier(bonus * Fact.GetRank(), Runtime, Descriptor));
            }
            if (Fortitude) {
                evt.AddTemporaryModifier(evt.Initiator.Stats.SaveFortitude.AddModifier(bonus * Fact.GetRank(), Runtime, Descriptor));
            }
            if (Will) {
                evt.AddTemporaryModifier(evt.Initiator.Stats.SaveWill.AddModifier(bonus * Fact.GetRank(), Runtime, Descriptor));
            }
        }
        public void OnEventDidTrigger(RuleSavingThrow evt) {
        }
    }
}
