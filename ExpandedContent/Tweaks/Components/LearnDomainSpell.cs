using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.FactLogic;
using System.Linq;

namespace ExpandedContent.Tweaks.Components {
    [AllowedOn(typeof(BlueprintUnitFact), false)]
    [AllowMultipleComponents]
    [TypeId("a3db9e6a8bb9403b95ea0f6aacb1d4e3")]
    public class LearnDomainSpell : UnitFactComponentDelegate,
        ILevelUpCompleteUIHandler,
        IGlobalSubscriber,
        ISubscriber {

        public BlueprintCharacterClassReference CharacterClass;
        public BlueprintCharacterClassReference ClericClass;
        public int SpellLevel;
        public BlueprintFeatureSelectionReference DomainSelection;

        public override void OnActivate() => this.AddSpell();

        private void AddSpell() {
            ClassData classData = this.Owner.Progression.GetClassData(CharacterClass);
            BlueprintSpellbook classSpellbook = classData.Spellbook;
            if (classSpellbook == null) return;
            var destinationSpellbook = this.Owner.Spellbooks.Where(x => x.Blueprint == classSpellbook && x.MaxSpellLevel >= SpellLevel).FirstOrDefault();
            if (destinationSpellbook == null) return;

            var allDomainsOwnerHas = DomainSelection.Get().m_AllFeatures
                .Where(x => Owner.HasFact(x))
                .ToList();

            foreach (var domain in allDomainsOwnerHas) {
                var bp = domain.Get();
                var learnSpellListComps = bp.GetComponents<LearnSpellList>();
                var clericLearn = learnSpellListComps.Where(x => x.m_CharacterClass.Guid == ClericClass.Guid).FirstOrDefault();
                if (clericLearn != null) {
                    var domainSpellList = clericLearn.m_SpellList.Get();
                    var spellsToLearn = domainSpellList.SpellsByLevel.Where(x => x.SpellLevel == SpellLevel)
                        .SelectMany(x => x.m_Spells);
                    foreach (var spell in spellsToLearn) {
                        bool isAlreadyKnownSpell = destinationSpellbook.GetKnownSpells(SpellLevel).Any(p => p.Blueprint == spell.Get());
                        if (!isAlreadyKnownSpell) {
                            destinationSpellbook.AddKnown(SpellLevel, spell);
                        }
                    }
                }
            }
        }

        public void HandleLevelUpComplete(UnitEntityData unit, bool isChargen) => this.AddSpell();
    }
}
