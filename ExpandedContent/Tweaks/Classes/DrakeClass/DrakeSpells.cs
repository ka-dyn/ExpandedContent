using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Classes.DrakeClass {
    internal class DrakeSpells {
        public static void AddDrakeSpells() {

            var DrakeCompanionClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DrakeCompanionClass");

            var DrakeSpellsDailyTable = Helpers.CreateBlueprint<BlueprintSpellsTable>("DrakeSpellsDailyTable", bp => {
                bp.Levels = new SpellsLevelEntry[] {
                        SpellTools.CreateSpellLevelEntry(0),
                        SpellTools.CreateSpellLevelEntry(0,1),
                        SpellTools.CreateSpellLevelEntry(0,2),
                        SpellTools.CreateSpellLevelEntry(0,3),
                        SpellTools.CreateSpellLevelEntry(0,3,1),
                        SpellTools.CreateSpellLevelEntry(0,4,2),
                        SpellTools.CreateSpellLevelEntry(0,4,3),
                        SpellTools.CreateSpellLevelEntry(0,4,3,1),
                        SpellTools.CreateSpellLevelEntry(0,4,4,2),
                        SpellTools.CreateSpellLevelEntry(0,5,4,3),
                        SpellTools.CreateSpellLevelEntry(0,5,4,3,1),
                        SpellTools.CreateSpellLevelEntry(0,5,4,4,2),
                        SpellTools.CreateSpellLevelEntry(0,5,5,4,3),
                        SpellTools.CreateSpellLevelEntry(0,5,5,4,3,1),
                        SpellTools.CreateSpellLevelEntry(0,5,5,4,4,2),
                        SpellTools.CreateSpellLevelEntry(0,5,5,5,4,3),
                        SpellTools.CreateSpellLevelEntry(0,5,5,5,4,3,1),
                        SpellTools.CreateSpellLevelEntry(0,5,5,5,4,4,2),
                        SpellTools.CreateSpellLevelEntry(0,5,5,5,5,4,3),
                        SpellTools.CreateSpellLevelEntry(0,5,5,5,5,5,4,1),
                        SpellTools.CreateSpellLevelEntry(0,5,5,5,5,5,5,2)
                    };
            });
            var DrakeSpellsKnownTable = Helpers.CreateBlueprint<BlueprintSpellsTable>("DrakeSpellsKnownTable", bp => {
                bp.Levels = new SpellsLevelEntry[] {
                        SpellTools.CreateSpellLevelEntry(0),
                        SpellTools.CreateSpellLevelEntry(0,0),
                        SpellTools.CreateSpellLevelEntry(0,0),
                        SpellTools.CreateSpellLevelEntry(0,0),
                        SpellTools.CreateSpellLevelEntry(0,0,0),
                        SpellTools.CreateSpellLevelEntry(0,0,0),
                        SpellTools.CreateSpellLevelEntry(0,0,0),
                        SpellTools.CreateSpellLevelEntry(0,0,0,0),
                        SpellTools.CreateSpellLevelEntry(0,0,0,0),
                        SpellTools.CreateSpellLevelEntry(0,0,0,0),
                        SpellTools.CreateSpellLevelEntry(0,0,0,0,0),
                        SpellTools.CreateSpellLevelEntry(0,0,0,0,0),
                        SpellTools.CreateSpellLevelEntry(0,0,0,0,0),
                        SpellTools.CreateSpellLevelEntry(0,0,0,0,0,0),
                        SpellTools.CreateSpellLevelEntry(0,0,0,0,0,0),
                        SpellTools.CreateSpellLevelEntry(0,0,0,0,0,0),
                        SpellTools.CreateSpellLevelEntry(0,0,0,0,0,0,0),
                        SpellTools.CreateSpellLevelEntry(0,0,0,0,0,0,0),
                        SpellTools.CreateSpellLevelEntry(0,0,0,0,0,0,0),
                        SpellTools.CreateSpellLevelEntry(0,0,0,0,0,0,0,0),
                        SpellTools.CreateSpellLevelEntry(0,0,0,0,0,0,0,0)
                    };
            });
            var DrakeEmptySpellList = Helpers.CreateBlueprint<BlueprintSpellList>("DrakeEmptySpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[] {
                    new SpellLevelList(0){ },
                    new SpellLevelList(1){ },
                    new SpellLevelList(2){ },
                    new SpellLevelList(3){ },
                    new SpellLevelList(4){ },
                    new SpellLevelList(5){ },
                    new SpellLevelList(6){ },
                    new SpellLevelList(7){ },
                    new SpellLevelList(8){ },
                    new SpellLevelList(9){ },

                };
            });
            var DrakeSpellbook = Helpers.CreateBlueprint<BlueprintSpellbook>("DrakeSpellbook", bp => {
                bp.Name = Helpers.CreateString($"DrakeSpellbook.Name", "Drake");
                bp.m_SpellsPerDay = DrakeSpellsDailyTable.ToReference<BlueprintSpellsTableReference>();
                bp.m_SpellSlots = null;
                bp.m_SpellsKnown = DrakeSpellsKnownTable.ToReference<BlueprintSpellsTableReference>();
                bp.m_SpellList = DrakeEmptySpellList.ToReference<BlueprintSpellListReference>();
                bp.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                bp.CastingAttribute = StatType.Charisma;
                bp.Spontaneous = true;
                bp.AllSpellsKnown = false;
                bp.CantripsType = CantripsType.Cantrips;
            });
            DrakeCompanionClass.m_Spellbook = DrakeSpellbook.ToReference<BlueprintSpellbookReference>();

            //spellbank
            var AcidArrow = Resources.GetBlueprint<BlueprintAbility>("9a46dfd390f943647ab4395fc997936d");
            var AcidFog = Resources.GetBlueprint<BlueprintAbility>("dbf99b00cd35d0a4491c6cc9e771b487");
            var Aid = Resources.GetBlueprint<BlueprintAbility>("03a9630394d10164a9410882d31572f0");
            var BalefulPolymorph = Resources.GetBlueprint<BlueprintAbility>("3105d6e9febdc3f41a08d2b7dda1fe74");
            var Banishment = Resources.GetBlueprint<BlueprintAbility>("d361391f645db984bbf58907711a146a");
            var BearsEndurance = Resources.GetBlueprint<BlueprintAbility>("a900628aea19aa74aad0ece0e65d091a");
            var BearsEnduranceMass = Resources.GetBlueprint<BlueprintAbility>("f6bcea6db14f0814d99b54856e918b92");
            var BestowGraceOfTheChampionCast = Resources.GetBlueprint<BlueprintAbility>("199d585bff173c74b86387856919242c");
            var Bless = Resources.GetBlueprint<BlueprintAbility>("90e59f4a4ada87243b7b3535a06d0638");
            var Blink = Resources.GetBlueprint<BlueprintAbility>("045351f1421ee3f449a9143db701d192");
            var Blur = Resources.GetBlueprint<BlueprintAbility>("14ec7a4e52e90fa47a4c8d63c69fd5c1");
            var BreakEnchantment = Resources.GetBlueprint<BlueprintAbility>("7792da00c85b9e042a0fdfc2b66ec9a8");
            var BreathOfLifeCast = Resources.GetBlueprint<BlueprintAbility>("d5847cad0b0e54c4d82d6c59a3cda6b0");
            var BrilliantInspiration = Resources.GetBlueprint<BlueprintAbility>("a5c56f0f699daec44b7aedd8b273b08a");
            var BullsStrength = Resources.GetBlueprint<BlueprintAbility>("4c3d08935262b6544ae97599b3a9556d");
            var CausticEruption = Resources.GetBlueprint<BlueprintAbility>("8c29e953190cc67429dc9c701b16b7c2");
            var ChainLightning = Resources.GetBlueprint<BlueprintAbility>("645558d63604747428d55f0dd3a4cb58");
            var CircleOfDeath = Resources.GetBlueprint<BlueprintAbility>("a89dcbbab8f40e44e920cc60636097cf");
            var CloakOfDreams = Resources.GetBlueprint<BlueprintAbility>("7f71a70d822af94458dc1a235507e972");
            var ConeOfCold = Resources.GetBlueprint<BlueprintAbility>("e7c530f8137630f4d9d7ee1aa7b1edc0");
            var ConfusionSpell = Resources.GetBlueprint<BlueprintAbility>("cf6c901fb7acc904e85c63b342e9c949");
            var CureModerateWounds = Resources.GetBlueprint<BlueprintAbility>("1c1ebf5370939a9418da93176cc44cd9");
            var CureSeriousWounds = Resources.GetBlueprint<BlueprintAbility>("6e81a6679a0889a429dec9cedcf3729c");
            var DimensionDoor = Resources.GetBlueprint<BlueprintAbility>("5bdc37e4acfa209408334326076a43bc");
            var Disintegrate = Resources.GetBlueprint<BlueprintAbility>("4aa7942c3e62a164387a73184bca3fc1");
            var Dismissal = Resources.GetBlueprint<BlueprintAbility>("95f7cdcec94e293489a85afdf5af1fd7");
            var DismissAreaEffect = Resources.GetBlueprint<BlueprintAbility>("97a23111df7547fd8f6417f9ba9b9775");
            var DispelMagic = Resources.GetBlueprint<BlueprintAbility>("92681f181b507b34ea87018e8f7a528a");
            var DispelMagicGreater = Resources.GetBlueprint<BlueprintAbility>("f0f761b808dc4b149b08eaf44b99f633");
            var Displacement = Resources.GetBlueprint<BlueprintAbility>("903092f6488f9ce45a80943923576ab3");
            var DivineFavor = Resources.GetBlueprint<BlueprintAbility>("9d5d2d3ffdd73c648af3eb3e585b1113");
            var DominatePerson = Resources.GetBlueprint<BlueprintAbility>("d7cbd2004ce66a042aeab2e95a3c5c61");
            var Enervation = Resources.GetBlueprint<BlueprintAbility>("f34fb78eaaec141469079af124bcfa0f");
            var Entangle = Resources.GetBlueprint<BlueprintAbility>("0fd00984a2c0e0a429cf1a911b4ec5ca");
            var ExpeditousRetreat = Resources.GetBlueprint<BlueprintAbility>("4f8181e7a7f1d904fbaea64220e83379");
            var FalseLife = Resources.GetBlueprint<BlueprintAbility>("7a5b5bf845779a941a67251539545762");
            var Fear = Resources.GetBlueprint<BlueprintAbility>("d2aeac47450c76347aebbc02e4f463e0");
            var Feeblemind = Resources.GetBlueprint<BlueprintAbility>("444eed6e26f773a40ab6e4d160c67faa");
            var FingerOfDeath = Resources.GetBlueprint<BlueprintAbility>("6f1dcf6cfa92d1948a740195707c0dbe");
            var Fireball = Resources.GetBlueprint<BlueprintAbility>("2d81362af43aeac4387a3d4fced489c3");
            var FireStorm = Resources.GetBlueprint<BlueprintAbility>("e3d0dfe1c8527934294f241e0ae96a8d");
            var FlameStrike = Resources.GetBlueprint<BlueprintAbility>("f9910c76efc34af41b6e43d5d8752f0f");
            var FreedomOfMovement = Resources.GetBlueprint<BlueprintAbility>("4c349361d720e844e846ad8c19959b1e");
            var Glitterdust = Resources.GetBlueprint<BlueprintAbility>("ce7dad2b25acf85429b6c9550787b2d9");
            var Grease = Resources.GetBlueprint<BlueprintAbility>("95851f6e85fe87d4190675db0419d112");
            var Haste = Resources.GetBlueprint<BlueprintAbility>("486eaff58293f6441a5c2759c4872f98");
            var Heal = Resources.GetBlueprint<BlueprintAbility>("ff8f1534f66559c478448723e16b6624");
            var Heroism = Resources.GetBlueprint<BlueprintAbility>("5ab0d42fb68c9e34abae4921822b9d63");
            var HideousLaughter = Resources.GetBlueprint<BlueprintAbility>("fd4d9fd7f87575d47aafe2a64a6e2d8d");
            var HoldMonster = Resources.GetBlueprint<BlueprintAbility>("41e8a952da7a5c247b3ec1c2dbb73018");
            var HoldPerson = Resources.GetBlueprint<BlueprintAbility>("c7104f7526c4c524f91474614054547e");
            var HoldPersonMass = Resources.GetBlueprint<BlueprintAbility>("defbbeaef79eda64abc645036228a31b");
            var HolyWord = Resources.GetBlueprint<BlueprintAbility>("4737294a66c91b844842caee8cf505c8");
            var Hypnotism = Resources.GetBlueprint<BlueprintAbility>("88367310478c10b47903463c5d0152b0");
            var IceBody = Resources.GetBlueprint<BlueprintAbility>("89778dc261fe6094bb2445cb389842d2");
            var IceStorm = Resources.GetBlueprint<BlueprintAbility>("fcb028205a71ee64d98175ff39a0abf9");
            var Insanity = Resources.GetBlueprint<BlueprintAbility>("2b044152b3620c841badb090e01ed9de");
            var Invisibility = Resources.GetBlueprint<BlueprintAbility>("89940cde01689fb46946b2f8cd7b66b7");
            var InvisibilityGreater = Resources.GetBlueprint<BlueprintAbility>("ecaa0def35b38f949bd1976a6c9539e0");
            var LifeBubble = Resources.GetBlueprint<BlueprintAbility>("265582bc494c4b12b5860b508a2f89a2");
            var LightingBolt = Resources.GetBlueprint<BlueprintAbility>("d2cff9243a7ee804cb6d5be47af30c73");
            var MageArmor = Resources.GetBlueprint<BlueprintAbility>("9e1ad5d6f87d19e4d8883d63a6e35568");
            var MageShield = Resources.GetBlueprint<BlueprintAbility>("ef768022b0785eb43a18969903c537c4");
            var MagicMissile = Resources.GetBlueprint<BlueprintAbility>("4ac47ddb9fa1eaf43a1b6809980cfbd2");
            var MindFog = Resources.GetBlueprint<BlueprintAbility>("eabf94e4edc6e714cabd96aa69f8b207");
            var MirrorImage = Resources.GetBlueprint<BlueprintAbility>("3e4ab69ada402d145a5e0ad3ad4b8564");
            var PhantasmalKiller = Resources.GetBlueprint<BlueprintAbility>("6717dbaef00c0eb4897a1c908a75dfe5");
            var PlagueStorm = Resources.GetBlueprint<BlueprintAbility>("82a5b848c05e3f342b893dedb1f9b446");
            var PoisonBreath = Resources.GetBlueprint<BlueprintAbility>("b5be90707c17a9643b90d90b7c4096e2");
            var PolymorphBase = Resources.GetBlueprint<BlueprintAbility>("93d9d74dac46b9b458d4d2ea7f4b1911");
            var PowerWordBlind = Resources.GetBlueprint<BlueprintAbility>("261e1788bfc5ac1419eec68b1d485dbc");
            var Prayer = Resources.GetBlueprint<BlueprintAbility>("faabd2cc67efa4646ac58c7bb3e40fcc");
            var PredictionOfFailure = Resources.GetBlueprint<BlueprintAbility>("0e67fa8f011662c43934d486acc50253");
            var PrimalRegression = Resources.GetBlueprint<BlueprintAbility>("07d577a74441a3a44890e3006efcf604");
            var PrismaticSpray = Resources.GetBlueprint<BlueprintAbility>("b22fd434bdb60fb4ba1068206402c4cf");
            var ProtectionFromEvil = Resources.GetBlueprint<BlueprintAbility>("eee384c813b6d74498d1b9cc720d61f4");
            var RainbowPattern = Resources.GetBlueprint<BlueprintAbility>("4b8265132f9c8174f87ce7fa6d0fe47b");
            var RemoveBlindness = Resources.GetBlueprint<BlueprintAbility>("c927a8b0cd3f5174f8c0b67cdbfde539");
            var ResistEnergy = Resources.GetBlueprint<BlueprintAbility>("21ffef7791ce73f468b6fca4d9371e8b");
            var Restoration = Resources.GetBlueprint<BlueprintAbility>("f2115ac1148256b4ba20788f7e966830");
            var RestorationLesser = Resources.GetBlueprint<BlueprintAbility>("e84fc922ccf952943b5240293669b171");
            var Resurrection = Resources.GetBlueprint<BlueprintAbility>("80a1a388ee938aa4e90d427ce9a7a3e9");
            var ScorchingRay = Resources.GetBlueprint<BlueprintAbility>("cdb106d53c65bbc4086183d54c3b97c7");
            var SeeInvisibility = Resources.GetBlueprint<BlueprintAbility>("30e5dc243f937fc4b95d2f8f4e1b7ff3");
            var SeeInvisibilityCommunal = Resources.GetBlueprint<BlueprintAbility>("1a045f845778dc54db1c2be33a8c3c0a");
            var Serenity = Resources.GetBlueprint<BlueprintAbility>("30e5dc243f937fc4b95d2f8f4e1b7ff3");
            var ShadowEvocation = Resources.GetBlueprint<BlueprintAbility>("237427308e48c3341b3d532b9d3a001f");
            var ShieldOfFaith = Resources.GetBlueprint<BlueprintAbility>("183d5bb91dea3a1489a6db6c9cb64445");
            var ShoutGreater = Resources.GetBlueprint<BlueprintAbility>("fd0d3840c48cafb44bb29e8eb74df204");
            var Sirocco = Resources.GetBlueprint<BlueprintAbility>("093ed1d67a539ad4c939d9d05cfe192c");
            var Slow = Resources.GetBlueprint<BlueprintAbility>("f492622e473d34747806bdb39356eb89");
            var Snowball = Resources.GetBlueprint<BlueprintAbility>("9f10909f0be1f5141bf1c102041f93d9");
            var Stoneskin = Resources.GetBlueprint<BlueprintAbility>("c66e86905f7606c4eaa5c774f0357b2b");
            var SummonMonsterISingle = Resources.GetBlueprint<BlueprintAbility>("8fd74eddd9b6c224693d9ab241f25e84");
            var SummonMonsterVBase = Resources.GetBlueprint<BlueprintAbility>("630c8b85d9f07a64f917d79cb5905741");
            var SymbolOfRevelation = Resources.GetBlueprint<BlueprintAbility>("48a555180a109e545a6e367b1bd3f535");
            var TarPool = Resources.GetBlueprint<BlueprintAbility>("7d700cdf260d36e48bb7af3a8ca5031f");
            var ThoughtSense = Resources.GetBlueprint<BlueprintAbility>("8fb1a1670b6e1f84b89ea846f589b627");
            var TrueSeeing = Resources.GetBlueprint<BlueprintAbility>("b3da3fbee6a751d4197e446c7e852bcb");
            var TrueSeeingCommunal = Resources.GetBlueprint<BlueprintAbility>("fa08cb49ade3eee42b5fd42bd33cb407");
            var TrueStrike = Resources.GetBlueprint<BlueprintAbility>("2c38da66e5a599347ac95b3294acbe00");
            var VampiricTouch = Resources.GetBlueprint<BlueprintAbility>("6cbb040023868574b992677885390f92");
            var WavesOfFatigue = Resources.GetBlueprint<BlueprintAbility>("8878d0c46dfbd564e9d5756349d5e439");
            var Web = Resources.GetBlueprint<BlueprintAbility>("134cb6d492269aa4f8662700ef57449f");

            var DrakeBloodGreenSpelllist = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBloodGreenSpelllist", bp => {
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DismissAreaEffect.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 0;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MagicMissile.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MageShield.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SummonMonsterISingle.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Entangle.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ResistEnergy.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MirrorImage.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SeeInvisibility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = AcidArrow.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DispelMagic.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Displacement.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Fireball.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Haste.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DimensionDoor.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = IceStorm.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Stoneskin.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Fear.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BalefulPolymorph.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PolymorphBase.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SummonMonsterVBase.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = AcidFog.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Disintegrate.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueSeeing.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PrismaticSpray.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = CausticEruption.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
            });
            var DrakeBloodSilverSpelllist = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBloodSilverSpelllist", bp => {
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DismissAreaEffect.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 0;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Bless.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MageShield.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DivineFavor.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ProtectionFromEvil.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ResistEnergy.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Web.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Invisibility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = CureModerateWounds.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DispelMagic.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = CureSeriousWounds.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HoldPerson.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Heroism.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DimensionDoor.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = IceStorm.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = FreedomOfMovement.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Restoration.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BreakEnchantment.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = FlameStrike.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BreathOfLifeCast.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Banishment.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DispelMagicGreater.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Heal.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HolyWord.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BestowGraceOfTheChampionCast.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
            });
            var DrakeBloodBlackSpelllist = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBloodBlackSpelllist", bp => {
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DismissAreaEffect.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 0;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MagicMissile.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MageArmor.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueStrike.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MageShield.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Blur.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Glitterdust.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Invisibility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ResistEnergy.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DispelMagic.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Slow.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HoldPerson.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Heroism.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DimensionDoor.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Fear.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PhantasmalKiller.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Enervation.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ConeOfCold.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DominatePerson.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = WavesOfFatigue.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = AcidFog.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Disintegrate.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueSeeing.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = FingerOfDeath.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PowerWordBlind.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
            });
            var DrakeBloodBlueSpelllist = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBloodBlueSpelllist", bp => {
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DismissAreaEffect.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 0;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MagicMissile.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MageArmor.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueStrike.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MageShield.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = FalseLife.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Glitterdust.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Invisibility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ResistEnergy.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DispelMagic.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Displacement.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Haste.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = VampiricTouch.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DimensionDoor.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Enervation.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Stoneskin.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Fear.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ChainLightning.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Dismissal.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = LifeBubble.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DispelMagicGreater.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HoldPersonMass.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueSeeing.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Insanity.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PlagueStorm.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
            });
            var DrakeBloodBrassSpelllist = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBloodBrassSpelllist", bp => {
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DismissAreaEffect.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 0;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Hypnotism.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ProtectionFromEvil.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MagicMissile.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MageShield.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SeeInvisibility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ResistEnergy.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BearsEndurance.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BullsStrength.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Haste.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Displacement.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HoldPerson.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Heroism.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ConfusionSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PolymorphBase.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Stoneskin.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Fear.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DominatePerson.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShadowEvocation.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ThoughtSense.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DispelMagicGreater.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Sirocco.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BearsEnduranceMass.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HoldPersonMass.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PowerWordBlind.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
            });
            var DrakeBloodRedSpelllist = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBloodRedSpelllist", bp => {
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DismissAreaEffect.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 0;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Grease.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueStrike.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MagicMissile.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MageShield.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SeeInvisibility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ResistEnergy.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ScorchingRay.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BullsStrength.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Haste.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Displacement.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Fireball.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Slow.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = InvisibilityGreater.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Enervation.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Stoneskin.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Fear.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PolymorphBase.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BreakEnchantment.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Heal.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DispelMagicGreater.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = CircleOfDeath.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TarPool.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HoldPersonMass.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = FireStorm.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
            });
            var DrakeBloodWhiteSpelllist = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBloodWhiteSpelllist", bp => {
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DismissAreaEffect.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 0;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Grease.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Snowball.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueStrike.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MageShield.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SeeInvisibility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ResistEnergy.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Invisibility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Blur.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DispelMagic.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Displacement.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = LightingBolt.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Slow.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Blink.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DimensionDoor.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Stoneskin.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Fear.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Feeblemind.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HoldMonster.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MindFog.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ChainLightning.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PoisonBreath.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PrimalRegression.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = IceBody.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutGreater.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
            });
            var DrakeBloodGoldSpelllist = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBloodGoldSpelllist", bp => {
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DismissAreaEffect.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 0;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DivineFavor.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MageArmor.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MageShield.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShieldOfFaith.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Aid.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = CureModerateWounds.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = RestorationLesser.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ResistEnergy.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DispelMagic.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Haste.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = RemoveBlindness.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Prayer.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Restoration.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SymbolOfRevelation.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Stoneskin.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = LifeBubble.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueSeeing.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = FlameStrike.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BreakEnchantment.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Heal.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DispelMagicGreater.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Serenity.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Resurrection.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HoldPersonMass.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
            });
            var DrakeBloodBronzeSpelllist = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBloodBronzeSpelllist", bp => {
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DismissAreaEffect.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 0;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MageArmor.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MageShield.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueStrike.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ProtectionFromEvil.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Blur.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Invisibility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MirrorImage.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Web.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DispelMagic.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Heroism.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Slow.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SeeInvisibilityCommunal.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DimensionDoor.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = IceStorm.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Stoneskin.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SymbolOfRevelation.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Dismissal.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MindFog.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ThoughtSense.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DispelMagicGreater.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueSeeing.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ChainLightning.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueSeeingCommunal.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PredictionOfFailure.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
            });
            var DrakeBloodCopperSpelllist = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBloodCopperSpelllist", bp => {
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DismissAreaEffect.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 0;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HideousLaughter.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Grease.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MageShield.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ExpeditousRetreat.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Glitterdust.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SeeInvisibility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Invisibility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ResistEnergy.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DispelMagic.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Haste.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HoldPerson.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Displacement.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ConfusionSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = RainbowPattern.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Stoneskin.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Fear.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MindFog.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PolymorphBase.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BalefulPolymorph.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = AcidFog.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DispelMagicGreater.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = CloakOfDreams.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PrismaticSpray.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HoldPersonMass.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
            });

        }
    }
}
