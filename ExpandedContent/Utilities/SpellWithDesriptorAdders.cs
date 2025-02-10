using ExpandedContent.Extensions;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using System.Linq;

namespace ExpandedContent.Utilities {
    internal class SpellWithDesriptorAdders {

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
                if (spell.SpellDescriptor.HasFlag(SpellDescriptor.Good) && !spell.SpellDescriptor.HasAnyFlag(SpellDescriptor.Chaos | SpellDescriptor.Law)) {
                    if (!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) {
                        NewLevel1Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in Level2Spells) {
                if (spell.SpellDescriptor.HasFlag(SpellDescriptor.Good) && !spell.SpellDescriptor.HasAnyFlag(SpellDescriptor.Chaos | SpellDescriptor.Law)) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) && 
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel2Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in Level3Spells) {
                if (spell.SpellDescriptor.HasFlag(SpellDescriptor.Good) && !spell.SpellDescriptor.HasAnyFlag(SpellDescriptor.Chaos | SpellDescriptor.Law)) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel3Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in Level4Spells) {
                if (spell.SpellDescriptor.HasFlag(SpellDescriptor.Good) && !spell.SpellDescriptor.HasAnyFlag(SpellDescriptor.Chaos | SpellDescriptor.Law)) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel4Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel4Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in Level5Spells) {
                if (spell.SpellDescriptor.HasFlag(SpellDescriptor.Good) && !spell.SpellDescriptor.HasAnyFlag(SpellDescriptor.Chaos | SpellDescriptor.Law)) {
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
                if (spell.SpellDescriptor.HasFlag(SpellDescriptor.Good) && !spell.SpellDescriptor.HasAnyFlag(SpellDescriptor.Chaos | SpellDescriptor.Law)) {
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

        public static void SkulkingHunterSpellAdder(BlueprintSpellList rangerspelllist, BlueprintSpellList druidspelllist, BlueprintSpellList wizardspelllist, BlueprintSpellList newspelllist) {
            var NewLevel0Spells = newspelllist.SpellsByLevel[0].m_Spells;
            var NewLevel1Spells = newspelllist.SpellsByLevel[1].m_Spells;
            var NewLevel2Spells = newspelllist.SpellsByLevel[2].m_Spells;
            var NewLevel3Spells = newspelllist.SpellsByLevel[3].m_Spells;
            var NewLevel4Spells = newspelllist.SpellsByLevel[4].m_Spells;
            var NewLevel5Spells = newspelllist.SpellsByLevel[5].m_Spells;
            var NewLevel6Spells = newspelllist.SpellsByLevel[6].m_Spells;
            var RangerLevel0Spells = rangerspelllist.SpellsByLevel[0].Spells;
            var RangerLevel1Spells = rangerspelllist.SpellsByLevel[1].Spells;
            var RangerLevel2Spells = rangerspelllist.SpellsByLevel[2].Spells;
            var RangerLevel3Spells = rangerspelllist.SpellsByLevel[3].Spells;
            var RangerLevel4Spells = rangerspelllist.SpellsByLevel[4].Spells;
            var RangerLevel5Spells = rangerspelllist.SpellsByLevel[5].Spells;
            var RangerLevel6Spells = rangerspelllist.SpellsByLevel[6].Spells;
            var DruidLevel0Spells = druidspelllist.SpellsByLevel[0].Spells;
            var DruidLevel1Spells = druidspelllist.SpellsByLevel[1].Spells;
            var DruidLevel2Spells = druidspelllist.SpellsByLevel[2].Spells;
            var DruidLevel3Spells = druidspelllist.SpellsByLevel[3].Spells;
            var DruidLevel4Spells = druidspelllist.SpellsByLevel[4].Spells;
            var DruidLevel5Spells = druidspelllist.SpellsByLevel[5].Spells;
            var DruidLevel6Spells = druidspelllist.SpellsByLevel[6].Spells;
            var WizardLevel0Spells = wizardspelllist.SpellsByLevel[0].Spells;
            var WizardLevel1Spells = wizardspelllist.SpellsByLevel[1].Spells;
            var WizardLevel2Spells = wizardspelllist.SpellsByLevel[2].Spells;
            var WizardLevel3Spells = wizardspelllist.SpellsByLevel[3].Spells;
            var WizardLevel4Spells = wizardspelllist.SpellsByLevel[4].Spells;
            var WizardLevel5Spells = wizardspelllist.SpellsByLevel[5].Spells;
            var WizardLevel6Spells = wizardspelllist.SpellsByLevel[6].Spells;
            foreach (var spell in RangerLevel0Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
                    if (!NewLevel0Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) {
                        NewLevel0Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in DruidLevel0Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
                    if (!NewLevel0Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) {
                        NewLevel0Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in WizardLevel0Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
                    if (!NewLevel0Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) {
                        NewLevel0Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in RangerLevel1Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
                    if (!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) {
                        NewLevel1Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in DruidLevel1Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
                    if (!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) {
                        NewLevel1Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in WizardLevel1Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
                    if (!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) {
                        NewLevel1Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in RangerLevel2Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel2Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in DruidLevel2Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel2Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in WizardLevel2Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel2Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in RangerLevel3Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel3Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in DruidLevel3Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel3Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in WizardLevel3Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel3Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in RangerLevel4Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel4Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel4Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in DruidLevel4Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel4Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel4Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in WizardLevel4Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel4Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel4Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in RangerLevel5Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel4Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel5Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel5Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in DruidLevel5Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel4Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel5Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel5Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in WizardLevel5Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
                    if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel4Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                        (!NewLevel5Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                        NewLevel5Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                    }
                }
            }
            foreach (var spell in RangerLevel6Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
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
            foreach (var spell in DruidLevel6Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
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
            foreach (var spell in WizardLevel6Spells) {
                if (spell.School == SpellSchool.Enchantment || spell.School == SpellSchool.Illusion || spell.School == SpellSchool.Transmutation) {
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

        public static void FaithfulParagonSpellAdder(BlueprintSpellList clericspelllist, BlueprintSpellList paladinspelllist, BlueprintSpellList newspelllist) {
            var NewLevel0Spells = newspelllist.SpellsByLevel[0].m_Spells;
            var NewLevel1Spells = newspelllist.SpellsByLevel[1].m_Spells;
            var NewLevel2Spells = newspelllist.SpellsByLevel[2].m_Spells;
            var NewLevel3Spells = newspelllist.SpellsByLevel[3].m_Spells;
            var NewLevel4Spells = newspelllist.SpellsByLevel[4].m_Spells;
            var NewLevel5Spells = newspelllist.SpellsByLevel[5].m_Spells;
            var NewLevel6Spells = newspelllist.SpellsByLevel[6].m_Spells;
            var ClericLevel0Spells = clericspelllist.SpellsByLevel[0].Spells;
            var ClericLevel1Spells = clericspelllist.SpellsByLevel[1].Spells;
            var ClericLevel2Spells = clericspelllist.SpellsByLevel[2].Spells;
            var ClericLevel3Spells = clericspelllist.SpellsByLevel[3].Spells;
            var ClericLevel4Spells = clericspelllist.SpellsByLevel[4].Spells;
            var ClericLevel5Spells = clericspelllist.SpellsByLevel[5].Spells;
            var ClericLevel6Spells = clericspelllist.SpellsByLevel[6].Spells;
            var PaladinLevel0Spells = paladinspelllist.SpellsByLevel[0].Spells;
            var PaladinLevel1Spells = paladinspelllist.SpellsByLevel[1].Spells;
            var PaladinLevel2Spells = paladinspelllist.SpellsByLevel[2].Spells;
            var PaladinLevel3Spells = paladinspelllist.SpellsByLevel[3].Spells;
            var PaladinLevel4Spells = paladinspelllist.SpellsByLevel[4].Spells;

            foreach (var spell in ClericLevel0Spells) {
                if (!NewLevel0Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) {
                    NewLevel0Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                }
            }
            foreach (var spell in PaladinLevel0Spells) {
                if (!NewLevel0Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) {
                    NewLevel0Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                }
            }
            foreach (var spell in ClericLevel1Spells) {
                if (!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) {
                    NewLevel1Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                }
            }
            foreach (var spell in PaladinLevel1Spells) {
                if (!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) {
                    NewLevel1Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                }
            }
            foreach (var spell in ClericLevel2Spells) {
                if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                    (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                    NewLevel2Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                }
            }
            foreach (var spell in PaladinLevel2Spells) {
                if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                    (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                    NewLevel2Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                }
            }
            foreach (var spell in ClericLevel3Spells) {
                if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                    (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                    (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                    NewLevel3Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                }
            }
            foreach (var spell in PaladinLevel3Spells) {
                if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                    (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                    (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                    NewLevel3Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                }
            }
            foreach (var spell in ClericLevel4Spells) {
                if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                    (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                    (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                    (!NewLevel4Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                    NewLevel4Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                }
            }
            foreach (var spell in PaladinLevel4Spells) {
                if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                    (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                    (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                    (!NewLevel4Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                    NewLevel4Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                }
            }
            foreach (var spell in ClericLevel5Spells) {
                if ((!NewLevel1Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                    (!NewLevel2Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                    (!NewLevel3Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                    (!NewLevel4Spells.Contains(spell.ToReference<BlueprintAbilityReference>())) &&
                    (!NewLevel5Spells.Contains(spell.ToReference<BlueprintAbilityReference>()))) {
                    NewLevel5Spells.Add(spell.ToReference<BlueprintAbilityReference>());
                }
            }
            foreach (var spell in ClericLevel6Spells) {
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
