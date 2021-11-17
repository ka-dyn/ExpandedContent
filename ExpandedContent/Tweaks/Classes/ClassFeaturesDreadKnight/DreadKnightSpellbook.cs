using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Classes.ClassFeaturesDreadKnight {
    internal class DreadKnightSpellbook {







        public static void AddDreadKnightSpellbook() {


            var DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
            var BloodragerSpellLevels = Resources.GetBlueprint<BlueprintSpellsTable>("d9e9437865e83344b864ef49ffa53013");
            var BloodragerClass = Resources.GetBlueprint<BlueprintCharacterClass>("d77e67a814d686842802c9cfd8ef8499");
            var PaladinSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("bce4989b070ce924b986bf346f59e885");

            var WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");

            


          
            var DreadKnightSpellLevels = Helpers.CreateBlueprint<BlueprintSpellsTable>("DreadKnightSpellLevels", bp => {
                bp.Levels = BloodragerSpellLevels.Levels.Select(level => SpellTools.CreateSpellLevelEntry(level.Count)).ToArray();
            });

            var DreadKnightSpellbook = Helpers.CreateBlueprint<BlueprintSpellbook>("DreadKnightSpellbook", bp => {
                bp.Name = Helpers.CreateString("$DreadKnightSpellbook.Name", "Dread Knight");
                bp.CastingAttribute = BloodragerClass.Spellbook.CastingAttribute;
                bp.AllSpellsKnown = BloodragerClass.Spellbook.AllSpellsKnown;
                bp.CantripsType = BloodragerClass.Spellbook.CantripsType;
                bp.HasSpecialSpellList = BloodragerClass.Spellbook.HasSpecialSpellList;
                bp.SpecialSpellListName = BloodragerClass.Spellbook.SpecialSpellListName;
                bp.m_SpellsPerDay = DreadKnightSpellLevels.ToReference<BlueprintSpellsTableReference>();
                bp.m_SpellsKnown = BloodragerClass.Spellbook.m_SpellsKnown;
                bp.m_SpellSlots = BloodragerClass.Spellbook.m_SpellSlots;
                bp.m_SpellList = WarpriestClass.Spellbook.m_SpellList;
                bp.m_MythicSpellList = BloodragerClass.Spellbook.m_MythicSpellList;
                bp.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                bp.m_Overrides = BloodragerClass.Spellbook.m_Overrides;
                bp.IsArcane = false;
                
                SpellTools.Spellbook.AllSpellbooks.Add(bp);
            });

        }
    }
}
