using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Mechanics;

namespace ExpandedContent.Tweaks.Components
{
    [AllowedOn(typeof(BlueprintUnitFact))]
    [ComponentName("Add extra drain on RuleDrainEnergy instance")] //I give this very low chance of working

    public class AddExtraDrainToEnergyDrainInstance : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleDrainEnergy>, IRulebookHandler<RuleDrainEnergy>, ISubscriber, IInitiatorRulebookSubscriber {
        
        public bool CheckSpellDescriptor = false;
        public SpellDescriptorWrapper SpellDescriptor;
        public bool SpellsOnly = false;
        public ContextDiceValue Value;

        public void OnEventAboutToTrigger(RuleDrainEnergy evt) {           

        }

        public void OnEventDidTrigger(RuleDrainEnergy evt) {

            MechanicsContext context = evt.Reason.Context;
            if (context?.SourceAbility == null || (!context.SpellDescriptor.HasAnyFlag(SpellDescriptor) && CheckSpellDescriptor) || (!context.SourceAbility.IsSpell && SpellsOnly) || context.SourceAbility.Type == AbilityType.Physical) {
                return;
            }

            if (evt.Result >= 1) {

                RuleDrainEnergy extraDrain = new RuleDrainEnergy(evt.Initiator, evt.Target, evt.Type, evt.m_Duration, new DiceFormula(Value.DiceCountValue.Calculate(base.Context), Value.DiceType), Value.BonusValue.Calculate(base.Context)) {
                    CriticalModifier = evt.CriticalModifier,
                    Empower = false,
                    Maximize = false,
                    ParentContext = base.Context,
                    SavingThrowType = Kingmaker.EntitySystem.Stats.SavingThrowType.Unknown
                };
                base.Context.TriggerRule(extraDrain);


            }



        }






    }
}
