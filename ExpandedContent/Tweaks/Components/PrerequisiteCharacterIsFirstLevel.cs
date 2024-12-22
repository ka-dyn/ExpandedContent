using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Root.Strings;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Class.LevelUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Components {
    [ComponentName("Feature must be taken on first level")]
    [AllowedOn(typeof(BlueprintFeature))]

    public class PrerequisiteCharacterIsFirstLevel : Prerequisite {

        public override bool CheckInternal(FeatureSelectionState selectionState, UnitDescriptor unit, LevelUpState state) {
            return unit.Progression.CharacterLevel <= 1;
        }

        public override string GetUITextInternal(UnitDescriptor unit) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{UIStrings.Instance.Tooltips.CharacterLevel}: {1}");
            if (unit != null) {
                stringBuilder.Append("\n");
                stringBuilder.Append(string.Format(UIStrings.Instance.Tooltips.CurrentValue, unit.Progression.CharacterLevel));
            }

            return stringBuilder.ToString();
        }
    }
}
