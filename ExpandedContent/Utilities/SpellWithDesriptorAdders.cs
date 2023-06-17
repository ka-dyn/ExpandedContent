using ExpandedContent.Extensions;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpandedContent.Utilities {
    internal class SpellWithDesriptorAdders {
        //Test thing to add spells with descriptor from one spellbook to another
        public static void RavenerHunterSpellAdder(BlueprintSpellList spelllist, BlueprintSpellList newspelllist) {
            var Level1Spells = spelllist.SpellsByLevel[1].Spells.SelectMany(spellandsubspell => spellandsubspell.AbilityAndVariants());
            var Level2Spells = spelllist.SpellsByLevel[2].Spells.SelectMany(spellandsubspell => spellandsubspell.AbilityAndVariants());
            var Level3Spells = spelllist.SpellsByLevel[3].Spells.SelectMany(spellandsubspell => spellandsubspell.AbilityAndVariants());
            var Level4Spells = spelllist.SpellsByLevel[4].Spells.SelectMany(spellandsubspell => spellandsubspell.AbilityAndVariants());
            var Level5Spells = spelllist.SpellsByLevel[5].Spells.SelectMany(spellandsubspell => spellandsubspell.AbilityAndVariants());
            var Level6Spells = spelllist.SpellsByLevel[6].Spells.SelectMany(spellandsubspell => spellandsubspell.AbilityAndVariants());
            var NewLevel1Spells = newspelllist.SpellsByLevel[1].m_Spells;
            var NewLevel2Spells = newspelllist.SpellsByLevel[2].m_Spells;
            var NewLevel3Spells = newspelllist.SpellsByLevel[3].m_Spells;
            var NewLevel4Spells = newspelllist.SpellsByLevel[4].m_Spells;
            var NewLevel5Spells = newspelllist.SpellsByLevel[5].m_Spells;
            var NewLevel6Spells = newspelllist.SpellsByLevel[6].m_Spells;
            foreach (var spell in Level1Spells) {
                if ((spell.SpellDescriptor & (SpellDescriptor.Good)) != 0) {
                    if (!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) {
                        NewLevel1Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in Level2Spells) {
                if ((spell.SpellDescriptor & (SpellDescriptor.Good)) != 0) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) && 
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel2Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in Level3Spells) {
                if ((spell.SpellDescriptor & (SpellDescriptor.Good)) != 0) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel3Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in Level4Spells) {
                if ((spell.SpellDescriptor & (SpellDescriptor.Good)) != 0) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel4Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel4Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in Level5Spells) {
                if ((spell.SpellDescriptor & (SpellDescriptor.Good)) != 0) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel4Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel5Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel5Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in Level6Spells) {
                if ((spell.SpellDescriptor & (SpellDescriptor.Good)) != 0) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel4Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel5Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel6Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel6Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
        }
    }
}
