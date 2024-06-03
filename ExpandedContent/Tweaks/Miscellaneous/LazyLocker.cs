using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Miscellaneous {
    internal class LazyLocker {
        public static void AddLazyLocker() {
            var LazyLockerFeature = Helpers.CreateBlueprint<BlueprintFeature>("LazyLockerFeature", bp => {
                bp.SetName("LazyLockerFeature");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
        }
    }
}
