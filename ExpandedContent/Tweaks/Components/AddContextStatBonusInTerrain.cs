using Kingmaker.Blueprints.Area;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.UnitLogic;
using Kingmaker.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Utility;
using static Kingmaker.GameModes.GameModeType;

namespace ExpandedContent.Tweaks.Components {
    [ComponentName("Add stat bonus in terrain")]
    [AllowMultipleComponents]
    [AllowedOn(typeof(BlueprintUnitFact), false)]
    public class AddContextStatBonusInTerrain : UnitFactComponentDelegate, ITeleportHandler, IGlobalSubscriber, ISubscriber {
        public AreaSetting Terrain;
        public ContextValue Value;
        public StatType Stat;
        public ModifierDescriptor Descriptor;
        public bool HasMinimal = false;
        public int Minimal;


        public bool CurrentAreaPartIsFavoredTerrain => AreaService.Instance.CurrentAreaSetting == Terrain;

        public override void OnTurnOn() {
            //base.OnTurnOn();
            UpdateModifiers();
        }

        public override void OnTurnOff() {
            //base.OnTurnOff();
            DeactivateModifier();
        }

        public void UpdateModifiers() {
            if (CurrentAreaPartIsFavoredTerrain) {
                ActivateModifier();
            } else {
                DeactivateModifier();
            }
        }

        public void ActivateModifier() {

            ModifiableValue stat = base.Owner.Stats.GetStat(Stat);
            int num = Value.Calculate(Fact.MaybeContext);

            if (HasMinimal) {
                stat.AddModifier(Math.Max(num, Minimal), base.Runtime, Descriptor);
            } 
            else {
                stat.AddModifier(num, base.Runtime, Descriptor);
            }

        }

        public void DeactivateModifier() {

            base.Owner.Stats.GetStat(Stat)?.RemoveModifiersFrom(base.Runtime);

        }

        public void HandlePartyTeleport(AreaEnterPoint enterPoint) {
            UpdateModifiers();
        }
    }
}
