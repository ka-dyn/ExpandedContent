using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Utilities;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kadynsWOTRMods.Tweaks.Classes.ClassFeatures
{
    internal class OathbreakersDirection
    {


        public static void AddOathbreakersDirection()
        {
            var OathbreakersDirectionBuff = Helpers.CreateCopy<BlueprintBuff>(Resources.GetBlueprint<BlueprintBuff>("76dabd40a1c1c644c86ce30e41ad5cab"));
            var OathbreakersDirectionAlliesBuff
            var OathbreakersDirectionAbility








            var OathbreakersDirection = Helpers.CreateCopy<BlueprintFeature>(Resources.GetBlueprint<BlueprintFeature>("485fa71b077a0304f86915a7718678ad"));
            OathbreakersDirection.SetName("Oathbreakers Direction");
            OathbreakersDirection.SetDescription("At 1st level, the Oathbreaker can, as a move action, indicate an enemy in combat and rally her allies to " +
                "focus on that target. The Oahtbreaker and her allies gain a +1 bonus on weapon attack and damage rolls against the target. " +
                "This ability applies only to allies who can see or hear the Oathbreaker and who are within 30 feet of the freebooter at the time she " +
                "activates this ability. At 5th level and every 5 levels thereafter (10th, 15th, and 20th level), the bonus increases by 1. The Oathbreaker's Direction " +
                "lasts until the target dies or the freebooter selects a new target.");

            
        }
    }
}
