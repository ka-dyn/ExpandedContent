using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Mechanics;

namespace ExpandedContent.Tweaks.Components {
    [ComponentName("Disable all Morale Bonuses")]
    [AllowedOn(typeof(BlueprintUnitFact), false)]
    public class DisableAllMoraleBonuses : UnitFactComponentDelegate {

        public override void OnTurnOn() {
            base.Owner.Stats.AC.MoraleDisable();
            base.Owner.Stats.AdditionalAttackBonus.MoraleDisable();
            base.Owner.Stats.AdditionalCMB.MoraleDisable();
            base.Owner.Stats.AdditionalCMD.MoraleDisable();
            base.Owner.Stats.AdditionalDamage.MoraleDisable();
            base.Owner.Stats.Charisma.MoraleDisable();
            base.Owner.Stats.CheckBluff.MoraleDisable();
            base.Owner.Stats.CheckDiplomacy.MoraleDisable();
            base.Owner.Stats.CheckIntimidate.MoraleDisable();
            base.Owner.Stats.Constitution.MoraleDisable();
            base.Owner.Stats.Dexterity.MoraleDisable();
            base.Owner.Stats.HitPoints.MoraleDisable();
            base.Owner.Stats.Initiative.MoraleDisable();
            base.Owner.Stats.Intelligence.MoraleDisable();
            base.Owner.Stats.SaveFortitude.MoraleDisable();
            base.Owner.Stats.SaveReflex.MoraleDisable();
            base.Owner.Stats.SaveWill.MoraleDisable();
            base.Owner.Stats.SkillAthletics.MoraleDisable();
            base.Owner.Stats.SkillKnowledgeArcana.MoraleDisable();
            base.Owner.Stats.SkillKnowledgeWorld.MoraleDisable();
            base.Owner.Stats.SkillLoreNature.MoraleDisable();
            base.Owner.Stats.SkillLoreReligion.MoraleDisable();
            base.Owner.Stats.SkillMobility.MoraleDisable();
            base.Owner.Stats.SkillPerception.MoraleDisable();
            base.Owner.Stats.SkillPersuasion.MoraleDisable();
            base.Owner.Stats.SkillStealth.MoraleDisable();
            base.Owner.Stats.SkillThievery.MoraleDisable();
            base.Owner.Stats.SkillUseMagicDevice.MoraleDisable();
            base.Owner.Stats.Speed.MoraleDisable();
            base.Owner.Stats.Strength.MoraleDisable();
            base.Owner.Stats.TemporaryHitPoints.MoraleDisable();
            base.Owner.Stats.Wisdom.MoraleDisable();
        }

        public override void OnTurnOff() {
            base.Owner.Stats.AC.MoraleEnable();
            base.Owner.Stats.AdditionalAttackBonus.MoraleEnable();
            base.Owner.Stats.AdditionalCMB.MoraleEnable();
            base.Owner.Stats.AdditionalCMD.MoraleEnable();
            base.Owner.Stats.AdditionalDamage.MoraleEnable();
            base.Owner.Stats.Charisma.MoraleEnable();
            base.Owner.Stats.CheckBluff.MoraleEnable();
            base.Owner.Stats.CheckDiplomacy.MoraleEnable();
            base.Owner.Stats.CheckIntimidate.MoraleEnable();
            base.Owner.Stats.Constitution.MoraleEnable();
            base.Owner.Stats.Dexterity.MoraleEnable();
            base.Owner.Stats.HitPoints.MoraleEnable();
            base.Owner.Stats.Initiative.MoraleEnable();
            base.Owner.Stats.Intelligence.MoraleEnable();
            base.Owner.Stats.SaveFortitude.MoraleEnable();
            base.Owner.Stats.SaveReflex.MoraleEnable();
            base.Owner.Stats.SaveWill.MoraleEnable();
            base.Owner.Stats.SkillAthletics.MoraleEnable();
            base.Owner.Stats.SkillKnowledgeArcana.MoraleEnable();
            base.Owner.Stats.SkillKnowledgeWorld.MoraleEnable();
            base.Owner.Stats.SkillLoreNature.MoraleEnable();
            base.Owner.Stats.SkillLoreReligion.MoraleEnable();
            base.Owner.Stats.SkillMobility.MoraleEnable();
            base.Owner.Stats.SkillPerception.MoraleEnable();
            base.Owner.Stats.SkillPersuasion.MoraleEnable();
            base.Owner.Stats.SkillStealth.MoraleEnable();
            base.Owner.Stats.SkillThievery.MoraleEnable();
            base.Owner.Stats.SkillUseMagicDevice.MoraleEnable();
            base.Owner.Stats.Speed.MoraleEnable();
            base.Owner.Stats.Strength.MoraleEnable();
            base.Owner.Stats.TemporaryHitPoints.MoraleEnable();
            base.Owner.Stats.Wisdom.MoraleEnable();
        }
    }
}
