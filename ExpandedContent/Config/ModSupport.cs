using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager;


namespace ExpandedContent.Config {
    internal class ModSupport {

        protected static bool IsMysticalMayhemEnabled() { return IsModEnabled("MysticalMayhem"); }






        [HarmonyLib.HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch {
            static bool Initialized;

            [HarmonyLib.HarmonyAfter()]
            public static void Postfix() {
                if (Initialized) return;
                Initialized = true;
                if (IsMysticalMayhemEnabled()) {



                    var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
                    var ColorSpraySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("91da41b9793a4624797921f221db653c");
                    var RainbowPatternSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4b8265132f9c8174f87ce7fa6d0fe47b");
                    var PrismaticSpraySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("b22fd434bdb60fb4ba1068206402c4cf");
                    var ChainLightningSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("645558d63604747428d55f0dd3a4cb58");
                    var SearingLightSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("bf0accce250381a44b857d4af6c8e10d");
                    var HypnoticPatternAbility = Resources.GetModBlueprint<BlueprintAbility>("HypnoticPatternAbility");
                    var SunburstSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e96424f70ff884947b06f41a765b7658");
                    var BreakEnchantmentSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("7792da00c85b9e042a0fdfc2b66ec9a8");
                    var OwlsWisdomMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("9f5ada581af3db4419b54db77f44e430");
                    var OwlsWisdomSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f0455c9295b53904f9e02fc571dd2ce1");
                    var RemoveBlindnessSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c927a8b0cd3f5174f8c0b67cdbfde539");
                    var ConfusionSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("cf6c901fb7acc904e85c63b342e9c949");
                    var SoundBurstSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c3893092a333b93499fd0a21845aa265");
                    var ShoutSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f09453607e683784c8fca646eec49162");
                    var SongOfDiscordSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d38aaf487e29c3d43a3bffa4a4a55f8f");
                    var ShoutGreaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fd0d3840c48cafb44bb29e8eb74df204");
                    var BrilliantInspirationSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("a5c56f0f699daec44b7aedd8b273b08a");
                    var MeteorSwarmAbility = Resources.GetBlueprintReference<BlueprintAbilityReference>("d0cd103b15494866b0444c1a961bc40f");
                    var OracleHeavensSpells = Resources.GetBlueprint<BlueprintFeature>("53faeeb7938b43eb9d8355a1f696b870");
                    var OceansEchoHeavensSpells = Resources.GetBlueprint<BlueprintFeature>("1cde726627a245aea3b75362f5b7f245");
                    OracleHeavensSpells.RemoveComponents<AddKnownSpell>();
                    OceansEchoHeavensSpells.RemoveComponents<AddKnownSpell>();
                    OracleHeavensSpells.TemporaryContext(bp => {                        
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = ColorSpraySpell;
                            c.SpellLevel = 1;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = HypnoticPatternAbility.ToReference<BlueprintAbilityReference>();
                            c.SpellLevel = 2;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = SearingLightSpell;
                            c.SpellLevel = 3;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = RainbowPatternSpell;
                            c.SpellLevel = 4;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = BreakEnchantmentSpell;
                            c.SpellLevel = 5;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = ChainLightningSpell;
                            c.SpellLevel = 6;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = PrismaticSpraySpell;
                            c.SpellLevel = 7;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = SunburstSpell;
                            c.SpellLevel = 8;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = MeteorSwarmAbility;
                            c.SpellLevel = 9;
                        });
                    });                    
                    OceansEchoHeavensSpells.TemporaryContext(bp => {
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = ColorSpraySpell;
                            c.SpellLevel = 1;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = SoundBurstSpell;
                            c.SpellLevel = 2;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = SearingLightSpell;
                            c.SpellLevel = 3;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = ShoutSpell;
                            c.SpellLevel = 4;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = SongOfDiscordSpell;
                            c.SpellLevel = 5;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = ShoutGreaterSpell;
                            c.SpellLevel = 6;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = BrilliantInspirationSpell;
                            c.SpellLevel = 7;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = SunburstSpell;
                            c.SpellLevel = 8;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = MeteorSwarmAbility;
                            c.SpellLevel = 9;
                        });
                    });






                }                
            }
        }











































        protected static bool IsModEnabled(string modName) {
            return modEntries.Where(mod => mod.Info.Id.Equals(modName) && mod.Enabled && !mod.ErrorOnLoading).Any();
        }
    }
}
