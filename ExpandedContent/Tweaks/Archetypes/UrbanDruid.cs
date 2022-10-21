using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.Utility;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class UrbanDruid {
        public static void AddUrbanDruid() {

            var DruidClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("610d836f3a3a9ed42a4349b62f002e96");


            var UrbanDruidArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("UrbanDruidArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"UrbanDruidArchetype.Name", "Urban Druid");
                bp.LocalizedDescription = Helpers.CreateString($"UrbanDruidArchetype.Description", "While many druids keep to the wilderness, some make their way within settlements, " +
                    "communing with the animals and vermin who live there and speaking for the nature that runs rampant in civilization’s very cradle.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"UrbanDruidArchetype.Description", "While many druids keep to the wilderness, some make their way within " +
                    "settlements, communing with the animals and vermin who live there and speaking for the nature that runs rampant in civilization’s very cradle.");

            });

        }
    }
}
