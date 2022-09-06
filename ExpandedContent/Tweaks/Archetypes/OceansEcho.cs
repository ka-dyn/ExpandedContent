using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Kingmaker.UnitLogic.ActivatableAbilities.ActivatableAbilityResourceLogic;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class OceansEcho {
        public static void AddOceansEcho() {

            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var OracleRevelationSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("60008a10ad7ad6543b1f63016741a5d2");
            var OracleMysterySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("5531b975dcdf0e24c98f1ff7e017e741");
            var BardMovePerformance = Resources.GetBlueprint<BlueprintFeature>("36931765983e96d4bb07ce7844cd897e");
            var BardSwiftPerformance = Resources.GetBlueprint<BlueprintFeature>("fd4ec50bc895a614194df6b9232004b9");
            var BardicPerformanceResource = Resources.GetBlueprint<BlueprintAbilityResource>("e190ba276831b5c4fa28737e5e49e6a6");
            var BardicPerformanceResourcePrerequisite = Resources.GetBlueprint<BlueprintFeature>("019ada4530c41274a885dfaa0fbf6218");
            var OceansEchoArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("OceansEchoArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"OceansEchoArchetype.Name", "Ocean's Echo");
                bp.LocalizedDescription = Helpers.CreateString($"OceansEchoArchetype.Description", "Although many merfolk claim deep connections to both art and the natural " +
                    "world, a rare few merfolk can manipulate the forces of nature and weave them into song. An ocean’s echo is a merfolk gifted with the powers of an oracle " +
                    "and a singing voice that evokes the legendary tales of merfolk virtuosos.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"OceansEchoArchetype.Description", "Although many merfolk claim deep connections to both art and the natural " +
                    "world, a rare few merfolk can manipulate the forces of nature and weave them into song. An ocean’s echo is a merfolk gifted with the powers of an oracle " +
                    "and a singing voice that evokes the legendary tales of merfolk virtuosos.");

            });
            //Song stuff
            var InspiringSongFeature = Helpers.CreateBlueprint<BlueprintFeature>("InspiringSongFeature", bp => {
                bp.SetName("Inspiring Song");
                bp.SetDescription("The voice of an ocean’s echo provides inspiration to allies. Allowing her to inspire courage at 1st level, inspire competence at 3rd level, and inspire " +
                    "heroics at 15th level, as a bard of the ocean’s echo’s level. It is usable a total number of rounds per day equal to her level + her Charisma modifier (minimum 1). " +
                    "\nThis ability replaces the revelations gained at 1st, 3rd, and 15th level.");
                bp.AddComponent<AddAbilityResources>(c => {
                    c.UseThisAsResource = false;
                    c.m_Resource = BardicPerformanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Amount = 0;
                    c.RestoreAmount = true;
                    c.RestoreOnLevelUp = false;
                });
                bp.AddComponent<IncreaseResourcesByClass>(c => {
                    c.m_Resource = BardicPerformanceResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Stat = StatType.Charisma;
                    c.BaseValue = 0;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { BardicPerformanceResourcePrerequisite.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
            });
            var InspireCourageBuff = Resources.GetBlueprint<BlueprintBuff>("b4027a834204042409248889cc8abf67");
            var InspireCourageEffectBuff = Resources.GetBlueprint<BlueprintBuff>("6d6d9e06b76f5204a8b7856c78607d5d").GetComponent<ContextRankConfig>();
            InspireCourageEffectBuff.m_AdditionalArchetypes.AppendToArray(OceansEchoArchetype.ToReference<BlueprintArchetypeReference>());
            InspireCourageEffectBuff.m_Class.AppendToArray(OracleClass.ToReference<BlueprintCharacterClassReference>());
            var OceansEchoInspireCourageToggleAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OceansEchoInspireCourageToggleAbility", bp => {
                bp.SetName("Inspiring Song - Courage");
                bp.SetDescription("A 1st level ocean's echo can use her performance to inspire courage in her allies (including himself), bolstering them against fear and improving their " +
                    "combat abilities. To be affected, an ally must be able to perceive the ocean's echo's performance. An affected ally receives a +1 morale {g|Encyclopedia:Bonus}bonus{/g} " +
                    "on {g|Encyclopedia:Saving_Throw}saving throws{/g} against charm and fear effects and a +1 competence bonus on {g|Encyclopedia:Attack}attack{/g} and weapon " +
                    "{g|Encyclopedia:Damage}damage rolls{/g}. At 5th level, and every six ocean's echo levels thereafter, this bonus increases by +1, to a maximum of +4 at 17th level.");
                bp.m_Icon = InspireCourageBuff.m_Icon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ResourceSpendType.NewRound;
                    c.m_RequiredResource = BardicPerformanceResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.m_Buff = InspireCourageBuff.ToReference<BlueprintBuffReference>();
                bp.Group = ActivatableAbilityGroup.BardicPerformance;
                bp.IsOnByDefault = false;
                bp.DeactivateIfCombatEnded = true;
                bp.DeactivateAfterFirstRound = false;
                bp.DeactivateImmediately = false;
                bp.IsTargeted = false;
                bp.DeactivateIfOwnerDisabled = true;
                bp.DeactivateIfOwnerUnconscious = false;
                bp.OnlyInCombat = false;
                bp.DoNotTurnOffOnRest = false;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = CommandType.Standard;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var OceansEchoInspireCourageFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoInspireCourageFeature", bp => {
                bp.SetName("Inspiring Song - Courage");
                bp.SetDescription("A 1st level ocean's echo can use her performance to inspire courage in her allies (including himself), bolstering them against fear and improving their " +
                    "combat abilities. To be affected, an ally must be able to perceive the ocean's echo's performance. An affected ally receives a +1 morale {g|Encyclopedia:Bonus}bonus{/g} " +
                    "on {g|Encyclopedia:Saving_Throw}saving throws{/g} against charm and fear effects and a +1 competence bonus on {g|Encyclopedia:Attack}attack{/g} and weapon " +
                    "{g|Encyclopedia:Damage}damage rolls{/g}. At 5th level, and every six ocean's echo levels thereafter, this bonus increases by +1, to a maximum of +4 at 17th level.");
                bp.m_Icon = InspireCourageBuff.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OceansEchoInspireCourageToggleAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var InspireCompetenceBuff = Resources.GetBlueprint<BlueprintBuff>("f58e8500ebc8594499bd804b0277cdd8");
            var InspireCompetenceEffectBuff = Resources.GetBlueprint<BlueprintBuff>("1fa5f733fa1d77743bf54f5f3da5a6b1").GetComponent<ContextRankConfig>();
            InspireCompetenceEffectBuff.m_AdditionalArchetypes.AppendToArray(OceansEchoArchetype.ToReference<BlueprintArchetypeReference>());
            InspireCompetenceEffectBuff.m_Class.AppendToArray(OracleClass.ToReference<BlueprintCharacterClassReference>());
            var OceansEchoInspireCompetenceToggleAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OceansEchoInspireCompetenceToggleAbility", bp => {
                bp.SetName("Inspiring Song - Competence");
                bp.SetDescription("A ocean's echo of 3rd level or higher can use her performance to help allies succeed at a task. They must be within 30 feet and able to see and hear the " +
                    "ocean's echo. They get a +2 competence {g|Encyclopedia:Bonus}bonus{/g} on all {g|Encyclopedia:Skills}skill checks{/g} as long as they continue to hear the ocean's echo's " +
                    "performance. This bonus increases by +1 for every four levels the ocean's echo has attained beyond 3rd (+3 at 7th, +4 at 11th, +5 at 15th, and +6 at 19th).");
                bp.m_Icon = InspireCompetenceBuff.m_Icon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ResourceSpendType.NewRound;
                    c.m_RequiredResource = BardicPerformanceResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.m_Buff = InspireCompetenceBuff.ToReference<BlueprintBuffReference>();
                bp.Group = ActivatableAbilityGroup.BardicPerformance;
                bp.IsOnByDefault = false;
                bp.DeactivateIfCombatEnded = true;
                bp.DeactivateAfterFirstRound = false;
                bp.DeactivateImmediately = false;
                bp.IsTargeted = false;
                bp.DeactivateIfOwnerDisabled = true;
                bp.DeactivateIfOwnerUnconscious = false;
                bp.OnlyInCombat = false;
                bp.DoNotTurnOffOnRest = false;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = CommandType.Standard;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var OceansEchoInspireCompetenceFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoInspireCompetenceFeature", bp => {
                bp.SetName("Inspiring Song - Competence");
                bp.SetDescription("A ocean's echo of 3rd level or higher can use her performance to help allies succeed at a task. They must be within 30 feet and able to see and hear the " +
                    "ocean's echo. They get a +2 competence {g|Encyclopedia:Bonus}bonus{/g} on all {g|Encyclopedia:Skills}skill checks{/g} as long as they continue to hear the ocean's echo's " +
                    "performance. This bonus increases by +1 for every four levels the ocean's echo has attained beyond 3rd (+3 at 7th, +4 at 11th, +5 at 15th, and +6 at 19th).");
                bp.m_Icon = InspireCompetenceBuff.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OceansEchoInspireCompetenceToggleAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var InspireHeroicsBuff = Resources.GetBlueprint<BlueprintBuff>("ab81563882fcf3a41bc657e0c6677ea2");
            var OceansEchoInspireHeroicsToggleAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OceansEchoInspireHeroicsToggleAbility", bp => {
                bp.SetName("Inspiring Song - Heroics");
                bp.SetDescription("An ocean's echo of 15th level or higher can inspire tremendous heroism in all allies within 30 feet. Inspired creatures gain a +4 morale " +
                    "{g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Saving_Throw}saving throws{/g} and a +4 dodge bonus to {g|Encyclopedia:Armor_Class}AC{/g}. The " +
                    "effect lasts for as long as the targets are able to witness the performance.");
                bp.m_Icon = InspireHeroicsBuff.m_Icon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ResourceSpendType.NewRound;
                    c.m_RequiredResource = BardicPerformanceResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.m_Buff = InspireHeroicsBuff.ToReference<BlueprintBuffReference>();
                bp.Group = ActivatableAbilityGroup.BardicPerformance;
                bp.IsOnByDefault = false;
                bp.DeactivateIfCombatEnded = true;
                bp.DeactivateAfterFirstRound = false;
                bp.DeactivateImmediately = false;
                bp.IsTargeted = false;
                bp.DeactivateIfOwnerDisabled = true;
                bp.DeactivateIfOwnerUnconscious = false;
                bp.OnlyInCombat = false;
                bp.DoNotTurnOffOnRest = false;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = CommandType.Standard;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var OceansEchoInspireHeroicsFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoInspireHeroicsFeature", bp => {
                bp.SetName("Inspiring Song - Heroics");
                bp.SetDescription("An ocean's echo of 15th level or higher can inspire tremendous heroism in all allies within 30 feet. Inspired creatures gain a +4 morale " +
                    "{g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Saving_Throw}saving throws{/g} and a +4 dodge bonus to {g|Encyclopedia:Armor_Class}AC{/g}. The " +
                    "effect lasts for as long as the targets are able to witness the performance.");
                bp.m_Icon = InspireHeroicsBuff.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OceansEchoInspireHeroicsToggleAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            //Mystery stuff
            var SoundBurstSpell = Resources.GetBlueprint<BlueprintAbility>("c3893092a333b93499fd0a21845aa265"); //LVL4
            var ShoutSpell = Resources.GetBlueprint<BlueprintAbility>("f09453607e683784c8fca646eec49162"); //LVL8
            var SongOfDiscordSpell = Resources.GetBlueprint<BlueprintAbility>("d38aaf487e29c3d43a3bffa4a4a55f8f"); //LVL10
            var ShoutGreaterSpell = Resources.GetBlueprint<BlueprintAbility>("fd0d3840c48cafb44bb29e8eb74df204"); //LVL12
            var BrilliantInspirationSpell = Resources.GetBlueprint<BlueprintAbility>("a5c56f0f699daec44b7aedd8b273b08a"); //LVL14
            var UnbreakableHeartSpell = Resources.GetBlueprint<BlueprintAbility>("dd38f33c56ad00a4da386c1afaa49967");
            var HeroismSpell = Resources.GetBlueprint<BlueprintAbility>("5ab0d42fb68c9e34abae4921822b9d63");
            var PredictionOfFailureSpell = Resources.GetBlueprint<BlueprintAbility>("0e67fa8f011662c43934d486acc50253");
            var ForesightSpell = Resources.GetBlueprint<BlueprintAbility>("1f01a098d737ec6419aedc4e7ad61fdd");
            var EnlargePersonSpell = Resources.GetBlueprint<BlueprintAbility>("c60969e7f264e6d4b84a1499fdcf9039");
            var MagicalVestmentSpell = Resources.GetBlueprint<BlueprintAbility>("2d4263d80f5136b4296d6eb43a221d7d");
            var RiftOfRuinSpell = Resources.GetBlueprint<BlueprintAbility>("dd3dacafcf40a0145a5824c838e2698d");
            var OverwhelmingPresenceSpell = Resources.GetBlueprint<BlueprintAbility>("41cf93453b027b94886901dbfc680cb9");
            var CauseFearSpell = Resources.GetBlueprint<BlueprintAbility>("bd81a3931aa285a4f9844585b5d97e51");
            var AnimateDeadSpell = Resources.GetBlueprint<BlueprintAbility>("4b76d32feb089ad4499c3a1ce8e1ac27");
            var HorridWiltingSpell = Resources.GetBlueprint<BlueprintAbility>("08323922485f7e246acb3d2276515526");
            var WailOfBansheeSpell = Resources.GetBlueprint<BlueprintAbility>("b24583190f36a8442b212e45226c54fc");
            var BurningHandsSpell = Resources.GetBlueprint<BlueprintAbility>("4783c3709a74a794dbe7c8e7e0b1b038");
            var FireballSpell = Resources.GetBlueprint<BlueprintAbility>("2d81362af43aeac4387a3d4fced489c3");
            var SummonElementalElderFireSpell = Resources.GetBlueprint<BlueprintAbility>("e4926aa766a1cc048835237b3a97597d");
            var FieryBodySpell = Resources.GetBlueprint<BlueprintAbility>("08ccad78cac525040919d51963f9ac39");
            var RemoveSicknessSpell = Resources.GetBlueprint<BlueprintAbility>("f6f95242abdfac346befd6f4f6222140");
            var NeutralizePoisonSpell = Resources.GetBlueprint<BlueprintAbility>("e7240516af4241b42b2cd819929ea9da");
            var HealMassSpell = Resources.GetBlueprint<BlueprintAbility>("867524328b54f25488d371214eea0d90");
            var HeroicInvocationSpell = Resources.GetBlueprint<BlueprintAbility>("43740dab07286fe4aa00a6ee104ce7c1");
            var FeatherStepSpell = Resources.GetBlueprint<BlueprintAbility>("f3c0b267dd17a2a45a40805e31fe3cd1");
            var PoisonCastSpell = Resources.GetBlueprint<BlueprintAbility>("2a6eda8ef30379142a4b75448fb214a3");
            var AnimalShapesSpell = Resources.GetBlueprint<BlueprintAbility>("cf689244b2c7e904eb85f26fd6e81552");
            var ShapechangeSpell = Resources.GetBlueprint<BlueprintAbility>("22b9044aa229815429d57d0a30e4b739");
            var CorrosiveTouchSpell = Resources.GetBlueprint<BlueprintAbility>("95810d2829895724f950c8c4086056e7");
            var ProtectionFromAcidSpell = Resources.GetBlueprint<BlueprintAbility>("3d77ee3fc4913c44b9df7c5bbcdc4906");
            var SummonElementalElderEarthSpell = Resources.GetBlueprint<BlueprintAbility>("65254c7a2cf18944287207e1de3e44e8");
            var ClashingRocksSpell = Resources.GetBlueprint<BlueprintAbility>("01300baad090d634cb1a1b2defe068d6");
            var SnowballSpell = Resources.GetBlueprint<BlueprintAbility>("9f10909f0be1f5141bf1c102041f93d9");
            var ProtectionFromColdSpell = Resources.GetBlueprint<BlueprintAbility>("021d39c8e0eec384ba69140f4875e166");
            var SeaMantleSpell = Resources.GetBlueprint<BlueprintAbility>("7ef49f184922063499b8f1346fb7f521");
            var TsunamiSpell = Resources.GetBlueprint<BlueprintAbility>("d8144161e352ca846a73cf90e85bf9ac");
            var LightningBoltSpell = Resources.GetBlueprint<BlueprintAbility>("d2cff9243a7ee804cb6d5be47af30c73");
            var StormboltsSpell = Resources.GetBlueprint<BlueprintAbility>("7cfbefe0931257344b2cb7ddc4cdff6f");
            var WindOfVengeanceSpell = Resources.GetBlueprint<BlueprintAbility>("5d8f1da2fdc0b9242af9f326f9e507be");
            var OceansEchoAncestorsSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoAncestorsSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = UnbreakableHeartSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HeroismSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SongOfDiscordSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutGreaterSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BrilliantInspirationSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PredictionOfFailureSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ForesightSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 9;
                });
            });
            var OceansEchoBattleSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoBattleSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = EnlargePersonSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MagicalVestmentSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SongOfDiscordSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutGreaterSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BrilliantInspirationSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = RiftOfRuinSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 9;
                });
            });
            var OceansEchoBonesSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoBonesSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = CauseFearSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = AnimateDeadSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SongOfDiscordSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutGreaterSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BrilliantInspirationSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HorridWiltingSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = WailOfBansheeSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 9;
                });
            });
            var OceansEchoFlameSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoFlameSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BurningHandsSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = FireballSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SongOfDiscordSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutGreaterSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BrilliantInspirationSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SummonElementalElderFireSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = FieryBodySpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 9;
                });
            });
            var OceansEchoLifeSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoLifeSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = RemoveSicknessSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = NeutralizePoisonSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SongOfDiscordSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutGreaterSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BrilliantInspirationSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HealMassSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HeroicInvocationSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 9;
                });
            });
            var OceansEchoNatureSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoNatureSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = FeatherStepSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PoisonCastSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SongOfDiscordSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutGreaterSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BrilliantInspirationSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = AnimalShapesSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShapechangeSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 9;
                });
            });
            var OceansEchoStoneSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoStoneSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = CorrosiveTouchSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ProtectionFromAcidSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SongOfDiscordSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutGreaterSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BrilliantInspirationSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SummonElementalElderEarthSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ClashingRocksSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 9;
                });
            });
            var OceansEchoWavesSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoWavesSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SnowballSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ProtectionFromColdSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SongOfDiscordSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutGreaterSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BrilliantInspirationSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SeaMantleSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TsunamiSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 9;
                });
            });
            var OceansEchoWindSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoWindSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = FeatherStepSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = LightningBoltSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SongOfDiscordSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutGreaterSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BrilliantInspirationSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = StormboltsSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = WindOfVengeanceSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 9;
                });
            });
            var OracleAncestorFinalRevelation = Resources.GetBlueprint<BlueprintFeature>("933b4d571a31b9a43aa424eac0a22068");
            var OracleBattleFinalRevelation = Resources.GetBlueprint<BlueprintFeature>("9caa8060ec83c0842b127c318571c6d1");
            var OracleBonesFinalRevelation = Resources.GetBlueprint<BlueprintFeature>("d20e184293942774eac71acb48ad7f26");
            var OracleFlameFinalRevelation = Resources.GetBlueprint<BlueprintFeature>("2b61f2c215ec8cb46ab28c4d08ac2cd5");
            var OracleLifeFinalRevelation = Resources.GetBlueprint<BlueprintFeature>("ee23b52c6a06c0b48a09a7a23071aa52");
            var OracleNatureFinalRevelation = Resources.GetBlueprint<BlueprintFeature>("e46981dfc05f3924784942b8684194ec");
            var OracleStoneFinalRevelation = Resources.GetBlueprint<BlueprintFeature>("46d347b49df9ac143b32fa9ef1926ee7");
            var OracleWavesFinalRevelation = Resources.GetBlueprint<BlueprintFeature>("0d5d8144b1e98f74882be1e0756edda6");
            var OracleWindFinalRevelation = Resources.GetBlueprint<BlueprintFeature>("169f0e005a5cc9143b0009359fa1453f");
            var OracleAncestorsMysteryFeature = Resources.GetBlueprint<BlueprintFeature>("9936554e9372dab4a874c1dd165fb6f8");
            var OracleBattleMysteryFeature = Resources.GetBlueprint<BlueprintFeature>("6b8e676943cb91648b21b7ca72fbb833");
            var OracleBonesMysteryFeature = Resources.GetBlueprint<BlueprintFeature>("067b175d3df0d1a408efd7eee2b36b9b");
            var OracleFlameMysteryFeature = Resources.GetBlueprint<BlueprintFeature>("3b68909df737cd4458509d7f3a9c3706");
            var OracleLifeMysteryFeature = Resources.GetBlueprint<BlueprintFeature>("a5458a1c316d1f24e8d9770f4fc179fa");
            var OracleNatureMysteryFeature = Resources.GetBlueprint<BlueprintFeature>("69579bfe28e15b942af0722021d8725c");
            var OracleStoneMysteryFeature = Resources.GetBlueprint<BlueprintFeature>("7bb4bb3e7fd26f34e8ca035a27e03e85");
            var OracleWavesMysteryFeature = Resources.GetBlueprint<BlueprintFeature>("e2f8bd7c45dfb954c8c42b168505c783");
            var OracleWindMysteryFeature = Resources.GetBlueprint<BlueprintFeature>("7c1fdd831af747b47bb2cce0051f309b");
            var OceansEchoAncestorsMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoAncestorsMysteryFeature", bp => {
                bp.SetName("Ancestors");
                bp.SetDescription("Gain access to the spells and revelations of the ancestors mystery \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.m_Icon = OracleAncestorsMysteryFeature.m_Icon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleAncestorFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoAncestorsSpells.ToReference<BlueprintFeatureReference>();
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
            var OceansEchoBattleMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoBattleMysteryFeature", bp => {
                bp.SetName("Battle");
                bp.SetDescription("Gain access to the spells and revelations of the battle mystery \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.m_Icon = OracleBattleMysteryFeature.m_Icon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleBattleFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoBattleSpells.ToReference<BlueprintFeatureReference>();
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
            var OceansEchoBonesMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoBonesMysteryFeature", bp => {
                bp.SetName("Bones");
                bp.SetDescription("Gain access to the spells and revelations of the bones mystery \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.m_Icon = OracleBonesMysteryFeature.m_Icon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleBonesFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoBonesSpells.ToReference<BlueprintFeatureReference>();
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
            var OceansEchoFlameMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoFlameMysteryFeature", bp => {
                bp.SetName("Flame");
                bp.SetDescription("Gain access to the spells and revelations of the flame mystery \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.m_Icon = OracleFlameMysteryFeature.m_Icon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleFlameFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoFlameSpells.ToReference<BlueprintFeatureReference>();
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
            var OceansEchoLifeMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoLifeMysteryFeature", bp => {
                bp.SetName("Life");
                bp.SetDescription("Gain access to the spells and revelations of the life mystery \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.m_Icon = OracleLifeMysteryFeature.m_Icon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleLifeFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoLifeSpells.ToReference<BlueprintFeatureReference>();
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
            var OceansEchoNatureMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoNatureMysteryFeature", bp => {
                bp.SetName("Nature");
                bp.SetDescription("Gain access to the spells and revelations of the nature mystery \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.m_Icon = OracleNatureMysteryFeature.m_Icon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleNatureFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoNatureSpells.ToReference<BlueprintFeatureReference>();
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
            var OceansEchoStoneMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoStoneMysteryFeature", bp => {
                bp.SetName("Stone");
                bp.SetDescription("Gain access to the spells and revelations of the stone mystery \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.m_Icon = OracleStoneMysteryFeature.m_Icon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleStoneFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoStoneSpells.ToReference<BlueprintFeatureReference>();
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
            var OceansEchoWavesMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoWavesMysteryFeature", bp => {
                bp.SetName("Waves");
                bp.SetDescription("Gain access to the spells and revelations of the waves mystery \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.m_Icon = OracleWavesMysteryFeature.m_Icon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleWavesFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoWavesSpells.ToReference<BlueprintFeatureReference>();
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
            var OceansEchoWindMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoWindMysteryFeature", bp => {
                bp.SetName("Wind");
                bp.SetDescription("Gain access to the spells and revelations of the wind mystery \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.m_Icon = OracleWindMysteryFeature.m_Icon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleWindFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoWindSpells.ToReference<BlueprintFeatureReference>();
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
            var OceansEchoMysterySelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("OceansEchoMysterySelection", bp => {
                bp.SetName("Mystery");
                bp.SetDescription("Each oracle draws upon a divine mystery to grant her {g|Encyclopedia:Spell}spells{/g} and powers. This mystery also grants additional class " +
                    "{g|Encyclopedia:Skills}skills{/g} and other {g|Encyclopedia:Special_Abilities}special abilities{/g}. This mystery can represent a devotion to one ideal, " +
                    "prayers to deities that support the concept, or a natural calling to champion a cause. Regardless of its source, the mystery manifests in a number of ways as " +
                    "the oracle gains levels. An oracle must pick one mystery upon taking her first level of oracle. Once made, this choice cannot be changed.\nAt 2nd level, and " +
                    "every two levels thereafter, an oracle learns an additional spell derived from her mystery. \nA ocean's echo adds {g|Encyclopedia:Persuasion}Persuasion{/g}, " +
                    "{g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g} to her list of class skills. These replace the additional " +
                    "class skills from her mystery.");
                bp.m_Icon = OracleMysterySelection.m_Icon;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Group = FeatureGroup.None;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    OceansEchoAncestorsMysteryFeature.ToReference<BlueprintFeatureReference>(),
                    OceansEchoBattleMysteryFeature.ToReference<BlueprintFeatureReference>(),
                    OceansEchoBonesMysteryFeature.ToReference<BlueprintFeatureReference>(),
                    OceansEchoFlameMysteryFeature.ToReference<BlueprintFeatureReference>(),
                    OceansEchoLifeMysteryFeature.ToReference<BlueprintFeatureReference>(),
                    OceansEchoNatureMysteryFeature.ToReference<BlueprintFeatureReference>(),
                    OceansEchoStoneMysteryFeature.ToReference<BlueprintFeatureReference>(),
                    OceansEchoWavesMysteryFeature.ToReference<BlueprintFeatureReference>(),
                    OceansEchoWindMysteryFeature.ToReference<BlueprintFeatureReference>()
                };
            });
            var SecondMysteryOceansEcho = Helpers.CreateBlueprint<BlueprintFeatureSelection>("SecondMysteryOceansEcho", bp => {
                bp.SetName("Second Mystery (Ocean's Echo)");
                bp.SetDescription("You opened your mind and soul to learn the secrets of a second mystery.\nBenefit: You gain a second Oracle Mystery. \nA ocean's echo adds " +
                    "{g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g} to her " +
                    "list of class skills. These replace the additional class skills from her mystery.");
                bp.m_Icon = OracleMysterySelection.m_Icon;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Group = FeatureGroup.None;
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = OceansEchoMysterySelection.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    OceansEchoAncestorsMysteryFeature.ToReference<BlueprintFeatureReference>(),
                    OceansEchoBattleMysteryFeature.ToReference<BlueprintFeatureReference>(),
                    OceansEchoBonesMysteryFeature.ToReference<BlueprintFeatureReference>(),
                    OceansEchoFlameMysteryFeature.ToReference<BlueprintFeatureReference>(),
                    OceansEchoLifeMysteryFeature.ToReference<BlueprintFeatureReference>(),
                    OceansEchoNatureMysteryFeature.ToReference<BlueprintFeatureReference>(),
                    OceansEchoStoneMysteryFeature.ToReference<BlueprintFeatureReference>(),
                    OceansEchoWavesMysteryFeature.ToReference<BlueprintFeatureReference>(),
                    OceansEchoWindMysteryFeature.ToReference<BlueprintFeatureReference>()
                };
                bp.Mode = SelectionMode.OnlyNew;
            });
            SecondMysteryOceansEcho.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.CheckInProgression = false;
                c.HideInUI = false;
                c.m_Feature = SecondMysteryOceansEcho.ToReference<BlueprintFeatureReference>();
            });
            FeatTools.AddAsMythicAbility(SecondMysteryOceansEcho);

            //Revelations
            var AcidSkin = Resources.GetBlueprint<BlueprintFeature>("bc1cbe328e1cd214698e6da7fbd9400f").GetComponent<PrerequisiteFeaturesFromList>();
            AcidSkin.m_Features = AcidSkin.m_Features.AppendToArray(OceansEchoStoneMysteryFeature.ToReference<BlueprintFeatureReference>());
            var AirBarrier = Resources.GetBlueprint<BlueprintFeature>("7e9743e4be4aad9438a58988cfe521bb").GetComponent<PrerequisiteFeaturesFromList>();
            AirBarrier.m_Features = AirBarrier.m_Features.AppendToArray(OceansEchoWindMysteryFeature.ToReference<BlueprintFeatureReference>());
            var ArmorOfBones = Resources.GetBlueprint<BlueprintFeature>("b07a138d653a16146bc12d90637d5dd1").GetComponent<PrerequisiteFeaturesFromList>();
            ArmorOfBones.m_Features = ArmorOfBones.m_Features.AppendToArray(OceansEchoBonesMysteryFeature.ToReference<BlueprintFeatureReference>());
            var Battlecry = Resources.GetBlueprint<BlueprintFeature>("9a67248ce68bd5f47a36d240ff8196e4").GetComponent<PrerequisiteFeaturesFromList>();
            Battlecry.m_Features = Battlecry.m_Features.AppendToArray(OceansEchoBattleMysteryFeature.ToReference<BlueprintFeatureReference>());
            var BattlefieldClarity = Resources.GetBlueprint<BlueprintFeature>("c0c2b21d83dd2514c98ae8d3684ad981").GetComponent<PrerequisiteFeaturesFromList>();
            BattlefieldClarity.m_Features = BattlefieldClarity.m_Features.AppendToArray(OceansEchoBattleMysteryFeature.ToReference<BlueprintFeatureReference>());
            var Blizzard = Resources.GetBlueprint<BlueprintFeature>("d518226e0f83aaf40aed6466d0ab3fb0").GetComponent<PrerequisiteFeaturesFromList>();
            Blizzard.m_Features = Blizzard.m_Features.AppendToArray(OceansEchoWavesMysteryFeature.ToReference<BlueprintFeatureReference>());
            var BloodOfHeros = Resources.GetBlueprint<BlueprintFeature>("1b9eebdfa2ad9d44eb60446d465b0cfd").GetComponent<PrerequisiteFeaturesFromList>();
            BloodOfHeros.m_Features = BloodOfHeros.m_Features.AppendToArray(OceansEchoAncestorsMysteryFeature.ToReference<BlueprintFeatureReference>());
            var BurningMagic = Resources.GetBlueprint<BlueprintFeature>("125294de0a922c34db4cd58ca7a200ac").GetComponent<PrerequisiteFeaturesFromList>();
            BurningMagic.m_Features = BurningMagic.m_Features.AppendToArray(OceansEchoFlameMysteryFeature.ToReference<BlueprintFeatureReference>());
            var Channel = Resources.GetBlueprint<BlueprintFeature>("ade57ae9bbe55c142a012c2453b3088c").GetComponent<PrerequisiteFeaturesFromList>();
            Channel.m_Features = Channel.m_Features.AppendToArray(OceansEchoLifeMysteryFeature.ToReference<BlueprintFeatureReference>());
            var CinderDance = Resources.GetBlueprint<BlueprintFeature>("6e67eae3081853544b191943f5ed4534").GetComponent<PrerequisiteFeaturesFromList>();
            CinderDance.m_Features = CinderDance.m_Features.AppendToArray(OceansEchoFlameMysteryFeature.ToReference<BlueprintFeatureReference>());
            var ClobberingStrike = Resources.GetBlueprint<BlueprintFeature>("b830f7ed7a68fa349a08cccc35975bdd").GetComponent<PrerequisiteFeaturesFromList>();
            ClobberingStrike.m_Features = ClobberingStrike.m_Features.AppendToArray(OceansEchoStoneMysteryFeature.ToReference<BlueprintFeatureReference>());
            var CombatHealer = Resources.GetBlueprint<BlueprintFeature>("db1d9829383e78841a6f1145411a54b4").GetComponent<PrerequisiteFeaturesFromList>();
            CombatHealer.m_Features = CombatHealer.m_Features.AppendToArray(OceansEchoBattleMysteryFeature.ToReference<BlueprintFeatureReference>());
            CombatHealer.m_Features = CombatHealer.m_Features.AppendToArray(OceansEchoLifeMysteryFeature.ToReference<BlueprintFeatureReference>());
            var DeathsTouch = Resources.GetBlueprint<BlueprintFeature>("1a25afc333021a64cbe9f23ef8c5ebd8").GetComponent<PrerequisiteFeaturesFromList>();
            DeathsTouch.m_Features = DeathsTouch.m_Features.AppendToArray(OceansEchoBonesMysteryFeature.ToReference<BlueprintFeatureReference>());
            var EnhancedCures = Resources.GetBlueprint<BlueprintFeature>("973a22b02c793ca49b48652e3d70ae80").GetComponent<PrerequisiteFeaturesFromList>();
            EnhancedCures.m_Features = EnhancedCures.m_Features.AppendToArray(OceansEchoLifeMysteryFeature.ToReference<BlueprintFeatureReference>());
            var ErosionTouch = Resources.GetBlueprint<BlueprintFeature>("b459fee5bc4b33449bb883b0ac5a01d8").GetComponent<PrerequisiteFeaturesFromList>();
            ErosionTouch.m_Features = ErosionTouch.m_Features.AppendToArray(OceansEchoNatureMysteryFeature.ToReference<BlueprintFeatureReference>());
            var FireBreath = Resources.GetBlueprint<BlueprintFeature>("5b664b58c911bc944b1572fccf0e7f5f").GetComponent<PrerequisiteFeaturesFromList>();
            FireBreath.m_Features = FireBreath.m_Features.AppendToArray(OceansEchoFlameMysteryFeature.ToReference<BlueprintFeatureReference>());
            var Firestorm = Resources.GetBlueprint<BlueprintFeature>("3fdc528f56566984fbbe0baaef0027a2").GetComponent<PrerequisiteFeaturesFromList>();
            Firestorm.m_Features = Firestorm.m_Features.AppendToArray(OceansEchoFlameMysteryFeature.ToReference<BlueprintFeatureReference>());
            var FluidNature = Resources.GetBlueprint<BlueprintFeature>("e248a57686c53be4cb49b082057850f3").GetComponent<PrerequisiteFeaturesFromList>();
            FluidNature.m_Features = FluidNature.m_Features.AppendToArray(OceansEchoWavesMysteryFeature.ToReference<BlueprintFeatureReference>());
            var FreezingSpells = Resources.GetBlueprint<BlueprintFeature>("bc2f6769fe042834db7120e3c8a50b47").GetComponent<PrerequisiteFeaturesFromList>();
            FreezingSpells.m_Features = FreezingSpells.m_Features.AppendToArray(OceansEchoWavesMysteryFeature.ToReference<BlueprintFeatureReference>());
            var FriendToAnimals = Resources.GetBlueprint<BlueprintFeature>("9a56368c28795544fbeb43fe70e1a40d").GetComponent<PrerequisiteFeaturesFromList>();
            FriendToAnimals.m_Features = FriendToAnimals.m_Features.AppendToArray(OceansEchoNatureMysteryFeature.ToReference<BlueprintFeatureReference>());
            var HeatAura = Resources.GetBlueprint<BlueprintFeature>("44dbe8e30c013544d85976671009d79d").GetComponent<PrerequisiteFeaturesFromList>();
            HeatAura.m_Features = HeatAura.m_Features.AppendToArray(OceansEchoFlameMysteryFeature.ToReference<BlueprintFeatureReference>());
            var IceArmor = Resources.GetBlueprint<BlueprintFeature>("a1cd9835c6699534ca124fab239fdf1c").GetComponent<PrerequisiteFeaturesFromList>();
            IceArmor.m_Features = IceArmor.m_Features.AppendToArray(OceansEchoWavesMysteryFeature.ToReference<BlueprintFeatureReference>());
            var IceSkin = Resources.GetBlueprint<BlueprintFeature>("cdeba08f8137cb141a9aa2f6fe55f99c").GetComponent<PrerequisiteFeaturesFromList>();
            IceSkin.m_Features = IceSkin.m_Features.AppendToArray(OceansEchoWavesMysteryFeature.ToReference<BlueprintFeatureReference>());
            var Invisibility = Resources.GetBlueprint<BlueprintFeature>("1f72e7cdcc07d994bac4fb65fc341971").GetComponent<PrerequisiteFeaturesFromList>();
            Invisibility.m_Features = Invisibility.m_Features.AppendToArray(OceansEchoWindMysteryFeature.ToReference<BlueprintFeatureReference>());
            var IronSkin = Resources.GetBlueprint<BlueprintFeature>("7b3f171fe4577474ead600758543cbef").GetComponent<PrerequisiteFeaturesFromList>();
            IronSkin.m_Features = IronSkin.m_Features.AppendToArray(OceansEchoBattleMysteryFeature.ToReference<BlueprintFeatureReference>());
            var LifeLeach = Resources.GetBlueprint<BlueprintFeature>("5efbf89a8bf2ab34a883810fd0fcc216").GetComponent<PrerequisiteFeaturesFromList>();
            LifeLeach.m_Features = LifeLeach.m_Features.AppendToArray(OceansEchoNatureMysteryFeature.ToReference<BlueprintFeatureReference>());
            var LifeLink = Resources.GetBlueprint<BlueprintFeature>("ef97c9bcc1c54ea7993ef8b2489c908a").GetComponent<PrerequisiteFeaturesFromList>();
            LifeLink.m_Features = LifeLink.m_Features.AppendToArray(OceansEchoLifeMysteryFeature.ToReference<BlueprintFeatureReference>());
            var Lifesense = Resources.GetBlueprint<BlueprintFeature>("17e537c174c7f0f4c9422c5ab5e3c2b8").GetComponent<PrerequisiteFeaturesFromList>();
            Lifesense.m_Features = Lifesense.m_Features.AppendToArray(OceansEchoLifeMysteryFeature.ToReference<BlueprintFeatureReference>());
            var LightningBreath = Resources.GetBlueprint<BlueprintFeature>("b7b19ce8776f81547ad3bfb6ac859045").GetComponent<PrerequisiteFeaturesFromList>();
            LightningBreath.m_Features = LightningBreath.m_Features.AppendToArray(OceansEchoWindMysteryFeature.ToReference<BlueprintFeatureReference>());
            var MightyPebble = Resources.GetBlueprint<BlueprintFeature>("b29d13f63f371664e9ae25bd65e5c463").GetComponent<PrerequisiteFeaturesFromList>();
            MightyPebble.m_Features = MightyPebble.m_Features.AppendToArray(OceansEchoStoneMysteryFeature.ToReference<BlueprintFeatureReference>());
            var MoltenSkin = Resources.GetBlueprint<BlueprintFeature>("5fd4d9ba38a9f8745b39fb8baee58337").GetComponent<PrerequisiteFeaturesFromList>();
            MoltenSkin.m_Features = MoltenSkin.m_Features.AppendToArray(OceansEchoFlameMysteryFeature.ToReference<BlueprintFeatureReference>());
            var NatureWhispers = Resources.GetBlueprint<BlueprintFeature>("3d2cd23869f0d98458169b88738f3c32").GetComponent<PrerequisiteFeaturesFromList>();
            NatureWhispers.m_Features = NatureWhispers.m_Features.AppendToArray(OceansEchoNatureMysteryFeature.ToReference<BlueprintFeatureReference>());
            var NearDeath = Resources.GetBlueprint<BlueprintFeature>("96649fb9694c1164caf7b836898685aa").GetComponent<PrerequisiteFeaturesFromList>();
            NearDeath.m_Features = NearDeath.m_Features.AppendToArray(OceansEchoBonesMysteryFeature.ToReference<BlueprintFeatureReference>());
            var PhantomTouch = Resources.GetBlueprint<BlueprintFeature>("60a1be62a0edf274580a1cdc5250293e").GetComponent<PrerequisiteFeaturesFromList>();
            PhantomTouch.m_Features = PhantomTouch.m_Features.AppendToArray(OceansEchoAncestorsMysteryFeature.ToReference<BlueprintFeatureReference>());
            var PunitiveTransformation = Resources.GetBlueprint<BlueprintFeature>("504a1b83335abcb4eb1ba1227a1b9e06").GetComponent<PrerequisiteFeaturesFromList>();
            PunitiveTransformation.m_Features = PunitiveTransformation.m_Features.AppendToArray(OceansEchoWavesMysteryFeature.ToReference<BlueprintFeatureReference>());
            var ResistLife = Resources.GetBlueprint<BlueprintFeature>("f7f37cff49bfc9b49a45d9f4fbb8aec3").GetComponent<PrerequisiteFeaturesFromList>();
            ResistLife.m_Features = ResistLife.m_Features.AppendToArray(OceansEchoBonesMysteryFeature.ToReference<BlueprintFeatureReference>());
            var SacredCouncil = Resources.GetBlueprint<BlueprintFeature>("a8ae26103124a1c409279392b8f56238").GetComponent<PrerequisiteFeaturesFromList>();
            SacredCouncil.m_Features = SacredCouncil.m_Features.AppendToArray(OceansEchoAncestorsMysteryFeature.ToReference<BlueprintFeatureReference>());
            var SafeCuring = Resources.GetBlueprint<BlueprintFeature>("3fa75c1a809882a4697db75daf8803e3").GetComponent<PrerequisiteFeaturesFromList>();
            SafeCuring.m_Features = SafeCuring.m_Features.AppendToArray(OceansEchoLifeMysteryFeature.ToReference<BlueprintFeatureReference>());
            var ShardExplosion = Resources.GetBlueprint<BlueprintFeature>("650b9b5efa60c5d4ea25faad3d346fcf").GetComponent<PrerequisiteFeaturesFromList>();
            ShardExplosion.m_Features = ShardExplosion.m_Features.AppendToArray(OceansEchoStoneMysteryFeature.ToReference<BlueprintFeatureReference>());
            var SkillAtArms = Resources.GetBlueprint<BlueprintFeature>("ba63899cf42ba8a459742078a526e7ec").GetComponent<PrerequisiteFeaturesFromList>();
            SkillAtArms.m_Features = SkillAtArms.m_Features.AppendToArray(OceansEchoBattleMysteryFeature.ToReference<BlueprintFeatureReference>());
            var SoulSiphon = Resources.GetBlueprint<BlueprintFeature>("226c053a75fd7c34cab1b493f5847787").GetComponent<PrerequisiteFeaturesFromList>();
            SoulSiphon.m_Features = SoulSiphon.m_Features.AppendToArray(OceansEchoBonesMysteryFeature.ToReference<BlueprintFeatureReference>());
            var SparkSkin = Resources.GetBlueprint<BlueprintFeature>("279637adc9719db4c93352f71366eba8").GetComponent<PrerequisiteFeaturesFromList>();
            SparkSkin.m_Features = SparkSkin.m_Features.AppendToArray(OceansEchoWindMysteryFeature.ToReference<BlueprintFeatureReference>());
            var SpiritBoost = Resources.GetBlueprint<BlueprintFeature>("8cf1bc6fe4d14304392496ff66023271").GetComponent<PrerequisiteFeaturesFromList>();
            SpiritBoost.m_Features = SpiritBoost.m_Features.AppendToArray(OceansEchoLifeMysteryFeature.ToReference<BlueprintFeatureReference>());
            var SpiritOfNature = Resources.GetBlueprint<BlueprintFeature>("a5ab3559cb7921a4a8c288110be248ec").GetComponent<PrerequisiteFeaturesFromList>();
            SpiritOfNature.m_Features = SpiritOfNature.m_Features.AppendToArray(OceansEchoNatureMysteryFeature.ToReference<BlueprintFeatureReference>());
            var SpiritOfTheWarrior = Resources.GetBlueprint<BlueprintFeature>("b63f198db73edbc4b84d863cb681f5bc").GetComponent<PrerequisiteFeaturesFromList>();
            SpiritOfTheWarrior.m_Features = SpiritOfTheWarrior.m_Features.AppendToArray(OceansEchoAncestorsMysteryFeature.ToReference<BlueprintFeatureReference>());
            var SpiritShield = Resources.GetBlueprint<BlueprintFeature>("899d6aa944ea9654fa829edcaedcf073").GetComponent<PrerequisiteFeaturesFromList>();
            SpiritShield.m_Features = SpiritShield.m_Features.AppendToArray(OceansEchoAncestorsMysteryFeature.ToReference<BlueprintFeatureReference>());
            var SpiritWalk = Resources.GetBlueprint<BlueprintFeature>("87bdea524911cfa4dacfe7243abe8b51").GetComponent<PrerequisiteFeaturesFromList>();
            SpiritWalk.m_Features = SpiritWalk.m_Features.AppendToArray(OceansEchoBonesMysteryFeature.ToReference<BlueprintFeatureReference>());
            var StoneStability = Resources.GetBlueprint<BlueprintFeature>("4244aa481ad9a604db8442c02712c1e0").GetComponent<PrerequisiteFeaturesFromList>();
            StoneStability.m_Features = StoneStability.m_Features.AppendToArray(OceansEchoStoneMysteryFeature.ToReference<BlueprintFeatureReference>());
            var StormOfSouls = Resources.GetBlueprint<BlueprintFeature>("0edd5395810cf3441a093ca49efb858f").GetComponent<PrerequisiteFeaturesFromList>();
            StormOfSouls.m_Features = StormOfSouls.m_Features.AppendToArray(OceansEchoAncestorsMysteryFeature.ToReference<BlueprintFeatureReference>());
            var Thunderburst = Resources.GetBlueprint<BlueprintFeature>("55e42ed24a0806140bccdcea79459f6a").GetComponent<PrerequisiteFeaturesFromList>();
            Thunderburst.m_Features = Thunderburst.m_Features.AppendToArray(OceansEchoWindMysteryFeature.ToReference<BlueprintFeatureReference>());
            var TouchOfAcid = Resources.GetBlueprint<BlueprintFeature>("9333c3106d4a6c24d8f22279194aeeff").GetComponent<PrerequisiteFeaturesFromList>();
            TouchOfAcid.m_Features = TouchOfAcid.m_Features.AppendToArray(OceansEchoStoneMysteryFeature.ToReference<BlueprintFeatureReference>());
            var TouchOfElectricity = Resources.GetBlueprint<BlueprintFeature>("44df0246a8d254e4fb41305135ae2692").GetComponent<PrerequisiteFeaturesFromList>();
            TouchOfElectricity.m_Features = TouchOfElectricity.m_Features.AppendToArray(OceansEchoWindMysteryFeature.ToReference<BlueprintFeatureReference>());
            var TouchOfFlame = Resources.GetBlueprint<BlueprintFeature>("7551135fc3779744396b229b38dca152").GetComponent<PrerequisiteFeaturesFromList>();
            TouchOfFlame.m_Features = TouchOfFlame.m_Features.AppendToArray(OceansEchoFlameMysteryFeature.ToReference<BlueprintFeatureReference>());
            var VortexSpells = Resources.GetBlueprint<BlueprintFeature>("cd9791a958181544a8b71346ec0722db").GetComponent<PrerequisiteFeaturesFromList>();
            VortexSpells.m_Features = VortexSpells.m_Features.AppendToArray(OceansEchoWindMysteryFeature.ToReference<BlueprintFeatureReference>());
            var WarSight = Resources.GetBlueprint<BlueprintFeature>("84f5169d964185741b97e95a1f1f2a79").GetComponent<PrerequisiteFeaturesFromList>();
            WarSight.m_Features = WarSight.m_Features.AppendToArray(OceansEchoBattleMysteryFeature.ToReference<BlueprintFeatureReference>());
            var WintryTouch = Resources.GetBlueprint<BlueprintFeature>("a63e315828427a54492724e06c0bd969").GetComponent<PrerequisiteFeaturesFromList>();
            WintryTouch.m_Features = WintryTouch.m_Features.AppendToArray(OceansEchoWavesMysteryFeature.ToReference<BlueprintFeatureReference>());
            var FormOfFlame = Resources.GetBlueprint<BlueprintProgression>("9c8997f740d5a634fb19ac32e0517180").GetComponent<PrerequisiteFeaturesFromList>();
            FormOfFlame.m_Features = FormOfFlame.m_Features.AppendToArray(OceansEchoFlameMysteryFeature.ToReference<BlueprintFeatureReference>());
            var WeaponMastery = Resources.GetBlueprint<BlueprintProgression>("0a4c3556355747241b2d3dcc0a88ec10").GetComponent<PrerequisiteFeaturesFromList>();
            WeaponMastery.m_Features = WeaponMastery.m_Features.AppendToArray(OceansEchoBattleMysteryFeature.ToReference<BlueprintFeatureReference>());
            var BondedMount = Resources.GetBlueprint<BlueprintFeatureSelection>("0234d0dd1cead22428e71a2500afa2e1").GetComponent<PrerequisiteFeaturesFromList>();
            BondedMount.m_Features = BondedMount.m_Features.AppendToArray(OceansEchoNatureMysteryFeature.ToReference<BlueprintFeatureReference>());
            var ManeuverMastery = Resources.GetBlueprint<BlueprintFeatureSelection>("89629bb513a70cb4596d1d780b95ea72").GetComponent<PrerequisiteFeaturesFromList>();
            ManeuverMastery.m_Features = ManeuverMastery.m_Features.AppendToArray(OceansEchoBattleMysteryFeature.ToReference<BlueprintFeatureReference>());

            OceansEchoArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, OracleMysterySelection, OracleRevelationSelection),
                    Helpers.LevelEntry(3, OracleRevelationSelection),
                    Helpers.LevelEntry(15, OracleRevelationSelection)
            };
            OceansEchoArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, InspiringSongFeature, OceansEchoInspireCourageFeature, OceansEchoMysterySelection),
                    Helpers.LevelEntry(3, OceansEchoInspireCompetenceFeature),
                    Helpers.LevelEntry(7, BardMovePerformance),
                    Helpers.LevelEntry(13, BardSwiftPerformance),
                    Helpers.LevelEntry(15, OceansEchoInspireHeroicsFeature)
            };
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Ocean's Echo")) { return; }
            OracleClass.m_Archetypes = OracleClass.m_Archetypes.AppendToArray(OceansEchoArchetype.ToReference<BlueprintArchetypeReference>());
        }
    }
}
