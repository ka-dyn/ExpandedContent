using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Classes.ClassFeaturesOathbreaker {
    internal class OathbreakerDefensiveStance {



        

        public static void AddDefensiveStance() {
            var DreadfulIcon = AssetLoader.LoadInternal("Skills", "Icon_DreadfulCalm.png");
            var DefensiveStanceFeature = Resources.GetBlueprint<BlueprintFeature>("2a6a2f8e492ab174eb3f01acf5b7c90a");
            var DreadfulCalm = Helpers.CreateBlueprint<BlueprintFeature>("DreadfulCalm", bp => {
                bp.SetName("Dreadful Calm");
                bp.SetDescription("At 4th level, an Oathbreaker can enter a dreadfully calm rage against those who have harmed her or her allies. The Oathbreaker " +
                    "gains the Defensive Stance ability as per the Stalwart Defender.");
                bp.m_DescriptionShort = Helpers.CreateString("$DreadfulCalm.DescriptionShort", "At 4th level, an Oathbreaker can enter a dreadfully calm rage against those who have harmed her or her allies. The Oathbreaker " +
                    "gains the Defensive Stance ability as per the Stalwart Defender.");
                bp.IsClassFeature = true;
                bp.Ranks = 1;
                bp.m_Icon = DreadfulIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DefensiveStanceFeature.ToReference<BlueprintUnitFactReference>() };
                });
            });

            var OathbreakerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("OathbreakerClass");
            var IncreasedDamageReductionDefensivePower = Resources.GetBlueprint<BlueprintFeature>("d10496e92d0799a40bb3930b8f4fda0d");
            IncreasedDamageReductionDefensivePower.AddComponent<PrerequisiteClassLevel>(c => {c.m_CharacterClass = OathbreakerClass.ToReference<BlueprintCharacterClassReference>(); c.Level = 1; });
            var FearlessDefenseDefensivePower = Resources.GetBlueprint<BlueprintFeature>("2c13bd43a7ed4844b9f4dcc919fd74f8");
            FearlessDefenseDefensivePower.AddComponent<PrerequisiteClassLevel>(c => { c.m_CharacterClass = OathbreakerClass.ToReference<BlueprintCharacterClassReference>(); c.Level = 1; });
        }
    }
}












            