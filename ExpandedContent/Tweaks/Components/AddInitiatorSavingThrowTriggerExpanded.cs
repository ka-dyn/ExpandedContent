using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Components {

    [ComponentName("Adding extra conditions on AddInitiatorSavingThrowTrigger")]
    [AllowedOn(typeof(BlueprintBuff))]

    public class AddInitiatorSavingThrowTriggerExpanded : UnitBuffComponentDelegate, IInitiatorRulebookHandler<RuleSavingThrow>, IRulebookHandler<RuleSavingThrow>, ISubscriber, IInitiatorRulebookSubscriber {
        
        public bool OnlyPass;
        public bool OnlyFail;
        public bool SpecificSave;
        public SavingThrowType ChooseSave = SavingThrowType.Fortitude;
        public bool SpecificDescriptor;
        public SpellDescriptorWrapper SpellDescriptor;
        public bool SpellsOnly = false;
        public bool FromExactSpellLevel = false;
        public int SpellLevel;
        public bool OnlyFromCaster = false;
        public bool OnlyFromFact = false;
        public BlueprintUnitFactReference m_CheckedFact;
        public BlueprintUnitFact CheckedFact => m_CheckedFact?.Get();

        public ActionList Action;

        public void OnEventAboutToTrigger(RuleSavingThrow evt) {
        }

        public void OnEventDidTrigger(RuleSavingThrow evt) {
            if (CheckConditions(evt) && base.Fact.MaybeContext != null) {
                using (base.Fact.MaybeContext?.GetDataScope(base.Owner)) {
                    (base.Fact as IFactContextOwner)?.RunActionInContext(Action, base.Owner);
                }
            }
        }

        public bool CheckConditions(RuleSavingThrow evt) {
            if (OnlyPass && !evt.IsPassed) {
                return false;
            }

            if (OnlyFail && evt.IsPassed) {
                return false;
            }

            if (SpecificSave && ChooseSave != evt.Type) {
                return false;
            }

            if (SpecificDescriptor && !evt.Reason.Context.SpellDescriptor.HasAnyFlag(SpellDescriptor)) {
                return false;
            }

            if (SpellsOnly && !evt.Reason.Context.SourceAbility.IsSpell) {
                return false;
            }
            if (FromExactSpellLevel && (evt.Reason.Context.SpellLevel != SpellLevel)) {
                return false;
            }
            if (OnlyFromCaster && (evt.Reason.Caster != Buff.Context.MaybeCaster)) {
                return false;
            }
            if (OnlyFromFact && !evt.Reason.Caster.HasFact(CheckedFact)) { 
                return false; 
            }

            return true;
        }
    }
}
