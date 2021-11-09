using kadynsWOTRMods.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kadynsWOTRMods.Tweaks.Classes.ClassFeatures
{
    internal class VindictiveBastardProgression
    {
        private static readonly BlueprintAbility VindictiveBastardVindictiveSmiteAbility = Resources.GetModBlueprint<BlueprintAbility>("VindictiveBastardVindictiveSmiteAbility");
        private static readonly BlueprintArchetype VindictiveBastardArchetype = Resources.GetModBlueprint<BlueprintArchetype>("VindictiveBastardArchetype");
        private static readonly BlueprintFeature VindictiveBastardVindictiveSmiteFeature = Resources.GetModBlueprint<BlueprintFeature>("VindictiveBastardVindictiveSmiteFeature");
        private static readonly BlueprintCharacterClass VindictiveBastardClass = Resources.GetModBlueprint<BlueprintCharacterClass>("VindictiveBastardClass");
        private static readonly BlueprintFeature VindictiveBastardProficiences = Resources.GetModBlueprint<BlueprintFeature>("VindictiveBastardProficiences");

        private static readonly BlueprintCharacterClass PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
        private static readonly BlueprintFeature SmiteEvilFeature = Resources.GetBlueprint<BlueprintFeature>("3a6db57fce75b0244a6a5819528ddf26");
        private static readonly BlueprintAbility SmiteEvilAbility = Resources.GetBlueprint<BlueprintAbility>("7bb9eb2042e67bf489ccd1374423cdec");
        private static readonly BlueprintBuff SmiteEvilBuff = Resources.GetBlueprint<BlueprintBuff>("b6570b8cbb32eaf4ca8255d0ec3310b0");

        private static readonly BlueprintActivatableAbility BattleMeditationAbility = Resources.GetBlueprint<BlueprintActivatableAbility>("a7a3303c8ab81914e8ecca76aedc70ec");
        private static readonly BlueprintFeature InquisitorSoloTacticsFeature = Resources.GetBlueprint<BlueprintFeature>("5602845cd22683840a6f28ec46331051");
        private static readonly BlueprintAbilityResource VindictiveBastardVindictiveSmiteResource = Resources.GetModBlueprint<BlueprintAbilityResource>("VindictiveBastardVindictiveSmiteResource");
        private static readonly BlueprintBuff VindictiveBastardVindictiveSmiteBuff = Resources.GetModBlueprint<BlueprintBuff>("VindictiveBastardVindictiveSmiteBuff");

        private static readonly BlueprintFeature PaladinClassProficiencies = Resources.GetBlueprint<BlueprintFeature>("b10ff88c03308b649b50c31611c2fefb");
        private static readonly BlueprintFeature PaladinLayOnHands = Resources.GetBlueprint<BlueprintFeature>("a1adf65aad7a4f3ba9a7a18e6075a2ec");
        private static readonly BlueprintFeature PaladinDivineBond = Resources.GetBlueprint<BlueprintFeature>("bf8a4b51ff7b41c3b5aa139e0fe16b34");

        public static void AddVindictiveBastardProgression()
        {
            var VindictiveBastardProgression = Helpers.CreateBlueprint<BlueprintProgression>("VindictiveBastardProgression", bp =>
            {
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[1];
                { VindictiveBastardClass.ToReference<BlueprintCharacterClassReference>(); };
                bp.m_UIDeterminatorsGroup = new BlueprintFeatureBaseReference[3]
                {
                    VindictiveBastardProficiences.ToReference<BlueprintFeatureBaseReference>(),
                    VindictiveBastardVindictiveSmiteFeature.ToReference<BlueprintFeatureBaseReference>(),
                    InquisitorSoloTacticsFeature.ToReference<BlueprintFeatureBaseReference>() };
            });
        }
    }
}
