using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Classes.ClassFeaturesDreadKnight {
    internal class AuraOfEvil {

        public static void AddAuraOfEvil() {

            var EvilIcon = AssetLoader.LoadInternal("Skills", "Icon_AuraEvil.png");
            var UnholyNimbus = Resources.GetBlueprint<BlueprintBuff>("ec14c8e821c460b42bb925a2320ddf0c");
            var AuraOfEvilBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfEvilBuff", bp => {
                bp.SetName("Aura of Evil");
                bp.SetDescription("At 1st level, a Dread Knight radiates a profane aura that is easily detectable by those strongly " +
                    "attuned to a good alignment. This aura has no negative or positive effect.");
                bp.m_Icon = EvilIcon;
                bp.FxOnStart = UnholyNimbus.FxOnStart;
                bp.FxOnRemove = UnholyNimbus.FxOnRemove;
                bp.IsClassFeature = true;
              
            });

            var AuraOfEvilFeature = Helpers.CreateBlueprint<BlueprintFeature>("AuraOfEvilFeature", bp => {

                bp.SetName("Aura of Evil");
                bp.SetDescription("At 1st level, a Dread Knight radiates a profane aura that is easily detectable by those strongly " +
                    "attuned to a good alignment. This aura has no negative or positive effect.");
                bp.m_Icon = EvilIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AuraFeatureComponent>(c => {
                    c.m_Buff = AuraOfEvilBuff.ToReference<BlueprintBuffReference>();
                });

            });

        }
    }
}
