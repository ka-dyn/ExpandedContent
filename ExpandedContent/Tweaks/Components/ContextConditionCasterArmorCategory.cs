using Kingmaker;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Blueprints.Items.Armors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Components {
    public class ContextConditionCasterArmorCategory : ContextCondition {

        public ArmorProficiencyGroup[] armorCategory;
        public override string GetConditionCaption() {
            return "";
        }

        public override bool CheckCondition() {
            UnitEntityData maybeCaster = base.Context.MaybeCaster;
            if (maybeCaster == null) {
                PFLog.Default.Error("Target unit is missing");
                return false;
            }

            return armorCategory.Contains(maybeCaster.Body.Armor.Armor.ArmorType());
        }





    }
}
