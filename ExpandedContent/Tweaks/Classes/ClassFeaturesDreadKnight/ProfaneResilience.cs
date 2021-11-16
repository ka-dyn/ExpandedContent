using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Classes.ClassFeaturesDreadKnight {
    internal class ProfaneResilience {

        public static void AddProfaneResilience() {
            var ProfaneResIcon = AssetLoader.LoadInternal("Skills", "Icon_UnholyRes.png");
            var ProfaneResilience = Helpers.CreateBlueprint<BlueprintFeature>("ProfaneResilience", bp => {
                bp.SetName("Profane Resilience");
                bp.SetDescription("At 2nd level, a Dread Knight gains a bonus equal to his Charisma bonus (if any) on all saving throws.");
                bp.m_Icon = ProfaneResIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<DerivativeStatBonus>(c => {
                    c.BaseStat = Kingmaker.EntitySystem.Stats.StatType.Charisma;
                    c.DerivativeStat = Kingmaker.EntitySystem.Stats.StatType.SaveFortitude;
                });
                bp.AddComponent<DerivativeStatBonus>(c => {
                    c.BaseStat = Kingmaker.EntitySystem.Stats.StatType.Charisma;
                    c.DerivativeStat = Kingmaker.EntitySystem.Stats.StatType.SaveWill;
                });
                bp.AddComponent<DerivativeStatBonus>(c => {
                    c.BaseStat = Kingmaker.EntitySystem.Stats.StatType.Charisma;
                    c.DerivativeStat = Kingmaker.EntitySystem.Stats.StatType.SaveReflex;
                });
            });
        }
    }
}
