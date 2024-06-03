using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpandedContent.Extensions;

namespace ExpandedContent.Utilities {
    public static class LazyLockUtility {

        public static void LazyLock(this BlueprintFeature feature) {
            BlueprintFeature LazyLockerFeature = Resources.GetModBlueprint<BlueprintFeature>("LazyLockerFeature");
            feature.AddComponent<PrerequisiteFeature>(c => {
                c.HideInUI = true;
                c.Group = Prerequisite.GroupType.Any;
                c.m_Feature = LazyLockerFeature.ToReference<BlueprintFeatureReference>();
            });
            feature.HideNotAvailibleInUI = true;
        }

    }
}
