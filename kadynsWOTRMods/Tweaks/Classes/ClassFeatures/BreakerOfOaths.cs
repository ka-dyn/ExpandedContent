using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kadynsWOTRMods.Tweaks.Classes.ClassFeatures {
    internal class BreakerOfOaths {


        public static void AddBreakerOfOaths() {

            var BreakerOfOaths = Helpers.CreateBlueprint<BlueprintFeature>("BreakerOfOaths", bp => {
                bp.SetName("Breaker of Oaths");
                bp.SetDescription("At 20th level, an Oathbreaker becomes a champion of her own ambition. Her {g|Encyclopedia:Damage_Reduction}DR{/g} increases to 15/lawful or good and whenever " +
                    "she makes a saving throw, she adds her charisma modifier as a bonus.");

                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.m_WeaponType = null;
                    c.Material = Kingmaker.Enums.Damage.PhysicalDamageMaterial.Adamantite;
                    c.MinEnhancementBonus = 1;
                    c.BypassedByAlignment = true;
                    c.Alignment = Kingmaker.Enums.Damage.DamageAlignment.Good;
                    c.Reality = Kingmaker.Enums.Damage.DamageRealityType.Ghost;
                    c.m_CheckedFactMythic = null;
                    c.Value = 5;
                    c.Pool = 12;
                });
                bp.AddComponent<DerivativeStatBonus>(c => {
                    c.BaseStat = Kingmaker.EntitySystem.Stats.StatType.Charisma;
                    c.DerivativeStat = Kingmaker.EntitySystem.Stats.StatType.SaveFortitude;
                });
                bp.AddComponent<DerivativeStatBonus>(c => {
                    c.BaseStat = Kingmaker.EntitySystem.Stats.StatType.Charisma;
                    c.DerivativeStat = Kingmaker.EntitySystem.Stats.StatType.SaveWill;
                });
                bp.AddComponent<DerivativeStatBonus>(c => {
                    c.BaseStat = Kingmaker.EntitySystem.Stats.StatType.Charisma;
                    c.DerivativeStat = Kingmaker.EntitySystem.Stats.StatType.SaveReflex;
                });
            });
        }
    }
}
