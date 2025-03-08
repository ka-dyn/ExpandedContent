using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Components;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.EventConditionActionSystem.Conditions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UI.GenericSlot;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System.Collections.Generic;
using TabletopTweaks.Core.NewComponents;

namespace ExpandedContent.Tweaks.Mysteries {
    internal class ApocalypseMystery {
        public static void AddApocalypseMystery() {

            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var OracleRevelationSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("60008a10ad7ad6543b1f63016741a5d2");
            var ApocalypseMysteryIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleApocalypseMystery.png");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");

            //Spelllist
            var DoomSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fbdd8c455ac4cde4a9a3e18c84af9485");//?1
            var SummonNaturesAllyIISpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("298148133cdc3fd42889b99c82711986");
            var ExplosiveRunesSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("ff652882623d43f184eb6abef741e20e");
            var IceStormSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fcb028205a71ee64d98175ff39a0abf9");
            var TidalSurgeSpell = Resources.GetModBlueprint<BlueprintAbility>("50bdf209dede4ce1acc722d5fdfcf53c");
            var CircleOfDeathSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("a89dcbbab8f40e44e920cc60636097cf");
            var IncendiaryCloudAbility = Resources.GetModBlueprint<BlueprintAbility>("IncendiaryCloudAbility");
            var MomentOfPrescienceAbility = Resources.GetModBlueprint<BlueprintAbility>("MomentOfPrescienceAbility");
            var MeteorSwarmSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("5e36df08c71748f7936bce310181fb71");
            var OwlsWisdomMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("9f5ada581af3db4419b54db77f44e430");
            var OwlsWisdomSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f0455c9295b53904f9e02fc571dd2ce1");
            var RemoveBlindnessSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c927a8b0cd3f5174f8c0b67cdbfde539");
            var ConfusionSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("cf6c901fb7acc904e85c63b342e9c949");
            var MindBlankSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("df2a0ba6b6dcecf429cbb80a56fee5cf");
            var MindBlankMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("87a29febd010993419f2a4a9bee11cfc");
            var TrueSeeingSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4cf3d0fae3239ec478f51e86f49161cb");
            var TrueSeeingMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fa08cb49ade3eee42b5fd42bd33cb407");
            var SoundBurstSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c3893092a333b93499fd0a21845aa265");
            var ShoutSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f09453607e683784c8fca646eec49162");
            var SongOfDiscordSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d38aaf487e29c3d43a3bffa4a4a55f8f");
            var ShoutGreaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fd0d3840c48cafb44bb29e8eb74df204");
            var BrilliantInspirationSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("a5c56f0f699daec44b7aedd8b273b08a");
            var OracleApocalypseSpells = Helpers.CreateBlueprint<BlueprintFeature>("OracleApocalypseSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DoomSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SummonNaturesAllyIISpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ExplosiveRunesSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = IceStormSpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TidalSurgeSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = CircleOfDeathSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = IncendiaryCloudAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MomentOfPrescienceAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MeteorSwarmSpell;
                    c.SpellLevel = 9;
                });
            });
            var EnlightnedPhilosopherApocalypseSpells = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherApocalypseSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DoomSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = OwlsWisdomSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = RemoveBlindnessSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ConfusionSpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueSeeingSpell;
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = OwlsWisdomMassSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueSeeingMassSpell;
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MindBlankSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MindBlankMassSpell;
                    c.SpellLevel = 9;
                });
            });
            var OceansEchoApocalypseSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoApocalypseSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DoomSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ExplosiveRunesSpell;
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
                    c.m_Spell = MomentOfPrescienceAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MeteorSwarmSpell;
                    c.SpellLevel = 9;
                });
            });
            #region Final Revelation TESTING
            var BestowCurseFeebleBodyBuff = Resources.GetBlueprint<BlueprintBuff>("c092750ba895e014cb24a25e2e8274a7");
            var BestowCurseWeaknessBuff = Resources.GetBlueprint<BlueprintBuff>("de92c96c86cb2cd4c8eb8e2881b84d99");
            var BestowCurseIdiocyBuff = Resources.GetBlueprint<BlueprintBuff>("7fbb7799e8684434e80487cef9cc7f09");
            var BestowCurseDeteriorationBuff = Resources.GetBlueprint<BlueprintBuff>("caae9592917719a41b601b678a8e6ddf");
            var WeaponFocusGreater = Resources.GetBlueprint<BlueprintParametrizedFeature>("09c9e82965fb4334b984a1e9df3bd088");
            var ImprovedCritical = Resources.GetBlueprint<BlueprintParametrizedFeature>("f4201c85a991369408740c6888362e20");

            var OracleApocalypseLazyCombatBuff = Helpers.CreateBuff("OracleApocalypseLazyCombatBuff", bp => {
                bp.SetName("Not in combat");
                bp.SetDescription("");
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;

            });

            var OracleApocalypseFinalFeebleBodyBuff = Helpers.CreateBuff("OracleApocalypseFinalFeebleBodyBuff", bp => {
                bp.SetName("Critical Curse - Feeble Body");
                bp.SetDescription("During this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability." +
                    "\r\nCurse of Feeble Body — The subject suffers a –6 {g|Encyclopedia:Penalty}penalty{/g} to {g|Encyclopedia:Constitution}Constitution{/g} score.");
                bp.m_Icon = WaterfallStyleIcon;
                bp.AddComponent<CombatStateTrigger>(c => {
                    c.CombatStartActions = Helpers.CreateActionList();
                    c.CombatEndActions = Helpers.CreateActionList( new ContextActionRemoveSelf() );
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = true;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Ranged;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = true;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(                        
                        new ContextActionApplyBuff() {
                            m_Buff = BestowCurseFeebleBodyBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue(),
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = true,
                        },
                        new ContextActionRemoveSelf()
                        );
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var OracleApocalypseFinalWeaknessBuff = Helpers.CreateBuff("OracleApocalypseFinalWeaknessBuff", bp => {
                bp.SetName("Critical Curse - Weakness");
                bp.SetDescription("During this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability." +
                    "\r\nCurse of Weakness — The subject suffers a –6 penalty to {g|Encyclopedia:Strength}Strength{/g} and {g|Encyclopedia:Dexterity}Dexterity{/g} scores.");
                bp.m_Icon = WaterfallStyleIcon;
                bp.AddComponent<CombatStateTrigger>(c => {
                    c.CombatStartActions = Helpers.CreateActionList();
                    c.CombatEndActions = Helpers.CreateActionList(new ContextActionRemoveSelf());
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = true;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Ranged;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = true;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BestowCurseWeaknessBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue(),
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = true,
                        },
                        new ContextActionRemoveSelf()
                        );
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var OracleApocalypseFinalIdiocyBuff = Helpers.CreateBuff("OracleApocalypseFinalIdiocyBuff", bp => {
                bp.SetName("Critical Curse - Idiocy");
                bp.SetDescription("During this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability." +
                    "\r\nCurse of Idiocy — The subject suffers a –6 penalty to {g|Encyclopedia:Intelligence}Intelligence{/g}, {g|Encyclopedia:Wisdom}Wisdom{/g}, " +
                    "and {g|Encyclopedia:Charisma}Charisma{/g} scores.");
                bp.m_Icon = WaterfallStyleIcon;
                bp.AddComponent<CombatStateTrigger>(c => {
                    c.CombatStartActions = Helpers.CreateActionList();
                    c.CombatEndActions = Helpers.CreateActionList(new ContextActionRemoveSelf());
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = true;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Ranged;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = true;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BestowCurseIdiocyBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue(),
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = true,
                        },
                        new ContextActionRemoveSelf()
                        );
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var OracleApocalypseFinalDeteriorationBuff = Helpers.CreateBuff("OracleApocalypseFinalDeteriorationBuff", bp => {
                bp.SetName("Critical Curse - Deterioration");
                bp.SetDescription("During this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability." +
                    "\r\nCurse of Idiocy — The subject suffers a –6 penalty to {g|Encyclopedia:Intelligence}Intelligence{/g}, {g|Encyclopedia:Wisdom}Wisdom{/g}, " +
                    "and {g|Encyclopedia:Charisma}Charisma{/g} scores.");
                bp.m_Icon = WaterfallStyleIcon;
                bp.AddComponent<CombatStateTrigger>(c => {
                    c.CombatStartActions = Helpers.CreateActionList();
                    c.CombatEndActions = Helpers.CreateActionList(new ContextActionRemoveSelf());
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = true;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Ranged;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = true;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BestowCurseDeteriorationBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue(),
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = true,
                        },
                        new ContextActionRemoveSelf()
                        );
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });

            var OracleApocalypseFinalCriticalCurseAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleApocalypseFinalCriticalCurseAbility", bp => {
                bp.SetName("Critical Curse");
                bp.SetDescription("While in combat you may expend a swift action; during this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability." +
                    "\nBestow Curse" +
                    "\nYou place a curse on the subject. Choose one of the following:" +
                    "\r\nCurse of Feeble Body — The subject suffers a –6 {g|Encyclopedia:Penalty}penalty{/g} to {g|Encyclopedia:Constitution}Constitution{/g} score." +
                    "\r\nCurse of Weakness — The subject suffers a –6 penalty to {g|Encyclopedia:Strength}Strength{/g} and {g|Encyclopedia:Dexterity}Dexterity{/g} scores." +
                    "\r\nCurse of Idiocy — The subject suffers a –6 penalty to {g|Encyclopedia:Intelligence}Intelligence{/g}, {g|Encyclopedia:Wisdom}Wisdom{/g}, " +
                    "and {g|Encyclopedia:Charisma}Charisma{/g} scores." +
                    "\r\nCurse of Deterioration — The subject suffers a –4 penalty on {g|Encyclopedia:Attack}attack rolls{/g}, {g|Encyclopedia:Saving_Throw}saves{/g}, " +
                    "{g|Encyclopedia:Ability_Scores}ability checks{/g}, and {g|Encyclopedia:Skills}skill{/g} {g|Encyclopedia:Check}checks{/g}.");
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { OracleApocalypseLazyCombatBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = false;
                });
                bp.m_Icon = WaveStyleIcon;
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleApocalypseFinalFeebleBodyAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleApocalypseFinalFeebleBodyAbility", bp => {
                bp.SetName("Critical Curse - Feeble Body");
                bp.SetDescription("While in combat you may expend a swift action; during this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability." +
                    "\r\nCurse of Feeble Body — The subject suffers a –6 {g|Encyclopedia:Penalty}penalty{/g} to {g|Encyclopedia:Constitution}Constitution{/g} score.");
                bp.m_Icon = WaveStyleIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleApocalypseFinalFeebleBodyBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalWeaknessBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalIdiocyBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalDeteriorationBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { OracleApocalypseLazyCombatBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.m_Parent = OracleApocalypseFinalCriticalCurseAbility.ToReference<BlueprintAbilityReference>();
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleApocalypseFinalWeaknessAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleApocalypseFinalWeaknessAbility", bp => {
                bp.SetName("Critical Curse - Weakness");
                bp.SetDescription("While in combat you may expend a swift action; during this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability." +
                    "\r\nCurse of Weakness — The subject suffers a –6 penalty to {g|Encyclopedia:Strength}Strength{/g} and {g|Encyclopedia:Dexterity}Dexterity{/g} scores.");
                bp.m_Icon = WaveStyleIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleApocalypseFinalWeaknessBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalFeebleBodyBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalIdiocyBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalDeteriorationBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { OracleApocalypseLazyCombatBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.m_Parent = OracleApocalypseFinalCriticalCurseAbility.ToReference<BlueprintAbilityReference>();
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleApocalypseFinalIdiocyAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleApocalypseFinalIdiocyAbility", bp => {
                bp.SetName("Critical Curse - Idiocy");
                bp.SetDescription("While in combat you may expend a swift action; during this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability." +
                    "\r\nCurse of Idiocy — The subject suffers a –6 penalty to {g|Encyclopedia:Intelligence}Intelligence{/g}, {g|Encyclopedia:Wisdom}Wisdom{/g}, " +
                    "and {g|Encyclopedia:Charisma}Charisma{/g} scores.");
                bp.m_Icon = WaveStyleIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleApocalypseFinalIdiocyBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalFeebleBodyBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalWeaknessBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalDeteriorationBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { OracleApocalypseLazyCombatBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.m_Parent = OracleApocalypseFinalCriticalCurseAbility.ToReference<BlueprintAbilityReference>();
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleApocalypseFinalDeteriorationAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleApocalypseFinalDeteriorationAbility", bp => {
                bp.SetName("Critical Curse - Deterioration");
                bp.SetDescription("While in combat you may expend a swift action; during this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability." +
                    "\r\nCurse of Idiocy — The subject suffers a –6 penalty to {g|Encyclopedia:Intelligence}Intelligence{/g}, {g|Encyclopedia:Wisdom}Wisdom{/g}, " +
                    "and {g|Encyclopedia:Charisma}Charisma{/g} scores.");
                bp.m_Icon = WaveStyleIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleApocalypseFinalDeteriorationBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalFeebleBodyBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalWeaknessBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalIdiocyBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { OracleApocalypseLazyCombatBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.m_Parent = OracleApocalypseFinalCriticalCurseAbility.ToReference<BlueprintAbilityReference>();
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            OracleApocalypseFinalCriticalCurseAbility.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    OracleApocalypseFinalFeebleBodyAbility.ToReference<BlueprintAbilityReference>(),
                    OracleApocalypseFinalWeaknessAbility.ToReference<BlueprintAbilityReference>(),
                    OracleApocalypseFinalIdiocyAbility.ToReference<BlueprintAbilityReference>(),
                    OracleApocalypseFinalDeteriorationAbility.ToReference<BlueprintAbilityReference>()
                };
            });

            var OracleApocalypseFinalRevelation = Helpers.CreateBlueprint<BlueprintFeature>("OracleApocalypseFinalRevelation", bp => {
                bp.SetName("Final Revelation");
                bp.SetDescription("Upon reaching 20th level, you become a herald of the apocalypse and wield the awesome power to fulfill such prophecy. " +
                    "Anytime you successfully cast a spell or use an ability that bestows 1 or more negative levels, you then bestow an 1d4 additional negative levels." +
                    "\nAdditionally, while in combat you may expend a swift action; during this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability.");
                bp.AddComponent<AddExtraDrainToEnergyDrainInstance>(c => {
                    c.CheckSpellDescriptor = false;
                    c.SpellDescriptor = SpellDescriptor.None;
                    c.SpellsOnly = false;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.D4,
                        DiceCountValue = 1,
                        BonusValue = 0
                    };
                });
                bp.AddComponent<CombatStateTrigger>(c => {
                    c.CombatStartActions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleApocalypseLazyCombatBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                    c.CombatEndActions = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            ToCaster = true,
                            m_Buff = OracleApocalypseLazyCombatBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });                
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleApocalypseFinalFeebleBodyAbility.ToReference<BlueprintUnitFactReference>(),
                        OracleApocalypseFinalIdiocyAbility.ToReference<BlueprintUnitFactReference>(),
                        OracleApocalypseFinalWeaknessAbility.ToReference<BlueprintUnitFactReference>(),
                        OracleApocalypseFinalDeteriorationAbility.ToReference<BlueprintUnitFactReference>(),
                        OracleApocalypseFinalCriticalCurseAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.IsClassFeature = true;
            });
            #endregion
            //Main Mystery Feature
            var OracleApocalypseMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleApocalypseMysteryFeature", bp => {
                bp.m_Icon = ApocalypseMysteryIcon;
                bp.SetName("Apocalypse");
                bp.SetDescription("An oracle with the apocalypse mystery adds {g|Encyclopedia:Lore_Nature}Lore (nature){/g}, {g|Encyclopedia:Persuasion}Persuasion{/g} " +
                    "and {g|Encyclopedia:Stealth}Stealth{/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.Level = 20;
                    c.m_Feature = OracleApocalypseFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleApocalypseSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillStealth;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //EnlightnedPhilosopherMystery
            var EnlightnedPhilosopherApocalypseMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherApocalypseMysteryFeature", bp => {
                bp.m_Icon = ApocalypseMysteryIcon;
                bp.SetName("Apocalypse");
                bp.SetDescription("An oracle with the apocalypse mystery adds {g|Encyclopedia:Lore_Nature}Lore (nature){/g}, {g|Encyclopedia:Persuasion}Persuasion{/g} " +
                    "and {g|Encyclopedia:Stealth}Stealth{/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = EnlightnedPhilosopherApocalypseSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillStealth;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.EnlightenedPhilosopherMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //DivineHerbalistMystery
            var DivineHerbalistApocalypseMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("DivineHerbalistApocalypseMysteryFeature", bp => {
                bp.m_Icon = ApocalypseMysteryIcon;
                bp.SetName("Apocalypse");
                bp.SetDescription("Gain access to the spells and revelations of the apocalypse mystery. \nDue to the divine herbalist archetype the class skills gained from this archetype" +
                    "are replaced by the master herbalist feature and the brew potions feat.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleApocalypseFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleApocalypseSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.DivineHerbalistMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //Ocean's Echo
            var OceansEchoApocalypseMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoApocalypseMysteryFeature", bp => {
                bp.m_Icon = ApocalypseMysteryIcon;
                bp.SetName("Apocalypse");
                bp.SetDescription("Gain access to the spells and revelations of the apocalypse mystery. \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleApocalypseFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoApocalypseSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.None };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            #region Defy Elements 

            #endregion
            #region Destructive Roots 

            #endregion
            #region Doomsayer 


            #endregion
            #region Dust to Dust 

            #endregion

            #region Erosion Touch and Near Death

            #endregion
            #region Pass the Torch 

            #endregion
            #region Power of the Fallen ?????????????? 

            #endregion
            #region Spell Blast  

            #endregion

            MysteryTools.RegisterMystery(OracleApocalypseMysteryFeature);
            MysteryTools.RegisterSecondMystery(OracleApocalypseMysteryFeature);
            MysteryTools.RegisterEnlightendPhilosopherMystery(EnlightnedPhilosopherApocalypseMysteryFeature);
            MysteryTools.RegisterSecondEnlightendPhilosopherMystery(EnlightnedPhilosopherApocalypseMysteryFeature);
            MysteryTools.RegisterHerbalistMystery(DivineHerbalistApocalypseMysteryFeature);
            MysteryTools.RegisterSecondHerbalistMystery(DivineHerbalistApocalypseMysteryFeature);
            MysteryTools.RegisterOceansEchoMystery(OceansEchoApocalypseMysteryFeature);
            MysteryTools.RegisterSecondOceansEchoMystery(OceansEchoApocalypseMysteryFeature);
            MysteryTools.RegisterHermitMystery(OracleApocalypseMysteryFeature);
            MysteryTools.RegisterSecondHermitMystery(OracleApocalypseMysteryFeature);
            MysteryTools.RegisterMysteryGiftSelection(OracleApocalypseMysteryFeature);
        }
    }
}
