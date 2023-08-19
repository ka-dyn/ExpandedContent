using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using ExpandedContent.Extensions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Utility;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace ExpandedContent.Utilities {
    internal class WitchTools {
        //Add Witch Hex
        public static void RegisterWitchHex(BlueprintFeature witchhex) {            
            var WitchHexSelections = new BlueprintFeatureSelection[] {
                Resources.GetBlueprint<BlueprintFeatureSelection>("9846043cf51251a4897728ed6e24e76f"), //WitchHexSelection
                Resources.GetBlueprint<BlueprintFeatureSelection>("a18b8c3d6251d8641a8094e5c2a7bc78"), //HexcrafterMagusHexMagusSelection
                Resources.GetBlueprint<BlueprintFeatureSelection>("ad6b9cecb5286d841a66e23cea3ef7bf"), //HexcrafterMagusHexArcanaSelection
                Resources.GetBlueprint<BlueprintFeatureSelection>("290bbcc3c3bb92144b853fd8fb8ff452"), //SylvanTricksterTalentSelection
                Resources.GetBlueprint<BlueprintFeatureSelection>("b921af3627142bd4d9cf3aefb5e2610a")  //WinterWitchHexSelection
            };
            foreach (var WitchHexSelection in WitchHexSelections) {
                WitchHexSelection.m_AllFeatures = WitchHexSelection.m_AllFeatures.AppendToArray(witchhex.ToReference<BlueprintFeatureReference>());
            }
        }

        public static void AddClassToHexConfigs(BlueprintCharacterClass characterclass) {
            var WitchHexResources = new BlueprintAbilityResource[] {
                Resources.GetBlueprint<BlueprintAbilityResource>("d7b6b2797253a5c478c957ba30bede47") //AuraOfPurity
            };
            foreach (var WitchHexResource in WitchHexResources) {
                WitchHexResource.m_MaxAmount.m_Class = WitchHexResource.m_MaxAmount.m_Class.AppendToArray(characterclass.ToReference<BlueprintCharacterClassReference>());
            }
            var WitchHexBuffs = new BlueprintBuff[] {
                Resources.GetBlueprint<BlueprintBuff>("da49e3ca7424a4741953ecc4f2fe11bb") //Ward
            };
            foreach (var WitchHexBuff in WitchHexBuffs) {
                WitchHexBuff.GetComponents<ContextRankConfig>().ForEach(c => { c.m_Class = c.m_Class.AppendToArray(characterclass.ToReference<BlueprintCharacterClassReference>()); });
            }
            var WitchHexsWithConfig = new BlueprintAbility[] {
                Resources.GetBlueprint<BlueprintAbility>("4a511e1dfeec46c4b867bf61b15eae2b"), //AnimalServant
                Resources.GetBlueprint<BlueprintAbility>("d560ab2a1b0613649833a0d92d6cfc6b"), //DeathCurse
                Resources.GetBlueprint<BlueprintAbility>("1bb5466b9bfcb5e47b9f667dad5784f9"), //LaytoRest
                Resources.GetBlueprint<BlueprintAbility>("c9097a1d0685f2f468ab63c1138815ad"), //Ameliorating1
                Resources.GetBlueprint<BlueprintAbility>("b1505c6512a0f2c4581dfa6af14e9d5c"), //Ameliorating2
                Resources.GetBlueprint<BlueprintAbility>("43738e1ea3c328549bcdd81883841e05"), //Ameliorating3
                Resources.GetBlueprint<BlueprintAbility>("e98cf086294d6134bb76d35e090cb059"), //Ameliorating4
                Resources.GetBlueprint<BlueprintAbility>("eaf7077a8ff35644883df6d4f7b2084c"), //Fortune
                Resources.GetBlueprint<BlueprintAbility>("ed4fbfcdb0f5dcb41b76d27ed00701af"), //Healing
                Resources.GetBlueprint<BlueprintAbility>("46cf5c995494e784c8d9a1696f9c61a7"), //Misfortune
                Resources.GetBlueprint<BlueprintAbility>("e7ecd11651b4df34897f33271a8d1cfc"), //ProtectiveLuck
                Resources.GetBlueprint<BlueprintAbility>("630ea63a63457ff4f9de059c578c930a"), //Slumber
                Resources.GetBlueprint<BlueprintAbility>("8f0eb58c2d6aeab4e8523ec85b4b2bc7"), //Vulnerability
                Resources.GetBlueprint<BlueprintAbility>("0d38e470e350ce34f869c20002d45763"), //Agony
                Resources.GetBlueprint<BlueprintAbility>("fcaaf1f25440a8f40ae424c7708e9a0f"), //BeastsGiftBite
                Resources.GetBlueprint<BlueprintAbility>("3108e18055b46b84f8a57898ad5ad075"), //BeastsGiftClaw
                Resources.GetBlueprint<BlueprintAbility>("e7489733ac7ccca40917d9364b406adb"), //DeliciousFright
                Resources.GetBlueprint<BlueprintAbility>("7244a24f0c186ce4b8a89fd26feded50"), //Hoarfrost
                Resources.GetBlueprint<BlueprintAbility>("e946e3fdc42ea65408daee0f029b4100"), //MajorAmeliorating1
                Resources.GetBlueprint<BlueprintAbility>("0e8f3d24a2cea644e91273f6c551152d"), //MajorAmeliorating2
                Resources.GetBlueprint<BlueprintAbility>("05bba992bea833349a246fd47042748c"), //MajorAmeliorating3
                Resources.GetBlueprint<BlueprintAbility>("1411baa7c0e58f547b76b6b13e5443f3"), //MajorAmeliorating4
                Resources.GetBlueprint<BlueprintAbility>("3408c351753aa9049af25af31ebef624"), //MajorHealing
                Resources.GetBlueprint<BlueprintAbility>("b5c4c51cc9993b14b9dc0f0c54199d09"), //RegenSinewFastHealing
                Resources.GetBlueprint<BlueprintAbility>("0a6effb356101cc46aa0bed8c3ab6fd4"), //RegenSinewRestoration
                Resources.GetBlueprint<BlueprintAbility>("a69fb167bb41c6f45a19c81ed4e3c0d9") //RestlessSlumber
            };
            foreach (var WitchHexWithConfig in WitchHexsWithConfig) {
                WitchHexWithConfig.GetComponents<ContextRankConfig>().ForEach(c => { c.m_Class = c.m_Class.AppendToArray(characterclass.ToReference<BlueprintCharacterClassReference>()); });
            }
        }

        public static void AddClassWithArchetypeToHexConfigs(BlueprintCharacterClass characterclass, BlueprintArchetype archetype) {
            var WitchHexResources = new BlueprintAbilityResource[] {
                Resources.GetBlueprint<BlueprintAbilityResource>("d7b6b2797253a5c478c957ba30bede47") //AuraOfPurity
            };
            foreach (var WitchHexResource in WitchHexResources) {
                WitchHexResource.m_MaxAmount.m_Class = WitchHexResource.m_MaxAmount.m_Class.AppendToArray(characterclass.ToReference<BlueprintCharacterClassReference>());
                WitchHexResource.m_MaxAmount.m_Archetypes = WitchHexResource.m_MaxAmount.m_Archetypes.AppendToArray(archetype.ToReference<BlueprintArchetypeReference>());
            }
            var WitchHexBuffs = new BlueprintBuff[] {
                Resources.GetBlueprint<BlueprintBuff>("da49e3ca7424a4741953ecc4f2fe11bb") //Ward
            };
            foreach (var WitchHexBuff in WitchHexBuffs) {
                WitchHexBuff.GetComponents<ContextRankConfig>().ForEach(c => { c.m_Class = c.m_Class.AppendToArray(characterclass.ToReference<BlueprintCharacterClassReference>()); });
                WitchHexBuff.GetComponents<ContextRankConfig>().ForEach(c => { c.m_AdditionalArchetypes = c.m_AdditionalArchetypes.AppendToArray(archetype.ToReference<BlueprintArchetypeReference>()); });
            }
            var WitchHexsWithConfig = new BlueprintAbility[] {
                Resources.GetBlueprint<BlueprintAbility>("4a511e1dfeec46c4b867bf61b15eae2b"), //AnimalServant
                Resources.GetBlueprint<BlueprintAbility>("d560ab2a1b0613649833a0d92d6cfc6b"), //DeathCurse
                Resources.GetBlueprint<BlueprintAbility>("1bb5466b9bfcb5e47b9f667dad5784f9"), //LaytoRest
                Resources.GetBlueprint<BlueprintAbility>("c9097a1d0685f2f468ab63c1138815ad"), //Ameliorating1
                Resources.GetBlueprint<BlueprintAbility>("b1505c6512a0f2c4581dfa6af14e9d5c"), //Ameliorating2
                Resources.GetBlueprint<BlueprintAbility>("43738e1ea3c328549bcdd81883841e05"), //Ameliorating3
                Resources.GetBlueprint<BlueprintAbility>("e98cf086294d6134bb76d35e090cb059"), //Ameliorating4
                Resources.GetBlueprint<BlueprintAbility>("eaf7077a8ff35644883df6d4f7b2084c"), //Fortune
                Resources.GetBlueprint<BlueprintAbility>("ed4fbfcdb0f5dcb41b76d27ed00701af"), //Healing
                Resources.GetBlueprint<BlueprintAbility>("46cf5c995494e784c8d9a1696f9c61a7"), //Misfortune
                Resources.GetBlueprint<BlueprintAbility>("e7ecd11651b4df34897f33271a8d1cfc"), //ProtectiveLuck
                Resources.GetBlueprint<BlueprintAbility>("630ea63a63457ff4f9de059c578c930a"), //Slumber
                Resources.GetBlueprint<BlueprintAbility>("8f0eb58c2d6aeab4e8523ec85b4b2bc7"), //Vulnerability
                Resources.GetBlueprint<BlueprintAbility>("0d38e470e350ce34f869c20002d45763"), //Agony
                Resources.GetBlueprint<BlueprintAbility>("fcaaf1f25440a8f40ae424c7708e9a0f"), //BeastsGiftBite
                Resources.GetBlueprint<BlueprintAbility>("3108e18055b46b84f8a57898ad5ad075"), //BeastsGiftClaw
                Resources.GetBlueprint<BlueprintAbility>("e7489733ac7ccca40917d9364b406adb"), //DeliciousFright
                Resources.GetBlueprint<BlueprintAbility>("7244a24f0c186ce4b8a89fd26feded50"), //Hoarfrost
                Resources.GetBlueprint<BlueprintAbility>("e946e3fdc42ea65408daee0f029b4100"), //MajorAmeliorating1
                Resources.GetBlueprint<BlueprintAbility>("0e8f3d24a2cea644e91273f6c551152d"), //MajorAmeliorating2
                Resources.GetBlueprint<BlueprintAbility>("05bba992bea833349a246fd47042748c"), //MajorAmeliorating3
                Resources.GetBlueprint<BlueprintAbility>("1411baa7c0e58f547b76b6b13e5443f3"), //MajorAmeliorating4
                Resources.GetBlueprint<BlueprintAbility>("3408c351753aa9049af25af31ebef624"), //MajorHealing
                Resources.GetBlueprint<BlueprintAbility>("b5c4c51cc9993b14b9dc0f0c54199d09"), //RegenSinewFastHealing
                Resources.GetBlueprint<BlueprintAbility>("0a6effb356101cc46aa0bed8c3ab6fd4"), //RegenSinewRestoration
                Resources.GetBlueprint<BlueprintAbility>("a69fb167bb41c6f45a19c81ed4e3c0d9") //RestlessSlumber
            };
            foreach (var WitchHexWithConfig in WitchHexsWithConfig) {
                WitchHexWithConfig.GetComponents<ContextRankConfig>().ForEach(c => { c.m_Class = c.m_Class.AppendToArray(characterclass.ToReference<BlueprintCharacterClassReference>()); });
                WitchHexWithConfig.GetComponents<ContextRankConfig>().ForEach(c => { c.m_AdditionalArchetypes = c.m_AdditionalArchetypes.AppendToArray(archetype.ToReference<BlueprintArchetypeReference>()); });

            }
        }
    }
}
