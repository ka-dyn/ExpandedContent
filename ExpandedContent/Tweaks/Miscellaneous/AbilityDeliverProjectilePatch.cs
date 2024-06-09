using HarmonyLib;
using Kingmaker.Items;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Miscellaneous {
    internal class AbilityDeliverProjectilePatch {


        [HarmonyPatch(typeof(AbilityDeliverProjectile), "RangedHandOfTheApprentice", new Type[] {typeof(bool) })]


    }
}
