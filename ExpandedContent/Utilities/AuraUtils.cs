using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;

namespace ExpandedContent.Utilities {
    internal class AuraUtils {

        public static AbilityAreaEffectRunAction CreateUnconditionalAuraEffect(BlueprintBuffReference buff) {
            return new AbilityAreaEffectRunAction() {
                UnitEnter = ActionFlow.DoSingle<ContextActionApplyBuff>(b => {
                    b.m_Buff = buff;
                    b.Permanent = true;
                    b.DurationValue = new ContextDurationValue();
                }),
                UnitExit = ActionFlow.DoSingle<ContextActionRemoveBuff>(b => {
                    b.m_Buff = buff;
                    b.RemoveRank = false;
                    b.ToCaster = false;
                }),
                UnitMove = ActionFlow.DoNothing(),
                Round = ActionFlow.DoNothing()
            };
        }
    }
}
