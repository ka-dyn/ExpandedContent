using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Classes.ClassFeaturesDreadKnight {

    internal class PlagueBringer {

        public static void AddPlagueBringer() {
            var PlagueIcon = AssetLoader.LoadInternal("Skills", "Icon_Plague.png");
            var PlagueBringer = Helpers.CreateBlueprint<BlueprintFeature>("PlagueBringer", bp => {
                bp.SetName("Plague Bringer");
                bp.SetDescription("At 3rd level, the powers of darkness make the Dread Knight a beacon of corruption and disease. " +
                    "A Dread Knight does not take any damage or take any penalty from diseases. They can still contract " +
                    "diseases and spread them to others, but they are otherwise immune to their effects.");
                bp.m_Icon = PlagueIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<BuffDescriptorImmunity>(c => { c.Descriptor = SpellDescriptor.Disease; });
            });
        }
    }
}

