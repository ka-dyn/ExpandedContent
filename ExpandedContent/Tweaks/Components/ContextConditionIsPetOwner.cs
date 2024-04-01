using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Mechanics.Conditions;

namespace ExpandedContent.Tweaks.Components {
    public class ContextConditionIsPetOwner : ContextCondition {

        public override string GetConditionCaption() {
            return "Check if target is this pets owner";
        }
        public override bool CheckCondition() {

            UnitEntityData pet = base.Context.MaybeCaster;
            UnitEntityData target = base.Target.Unit;
            if (pet.Master != target) {
                return false;
            }
            return true;
        }
    }
}
