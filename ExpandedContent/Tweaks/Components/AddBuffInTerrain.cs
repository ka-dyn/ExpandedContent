using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.Enums;
using Kingmaker.Blueprints.Area;
using Kingmaker.View;
using BlueprintCore.Utils;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace ExpandedContent.Tweaks.Components {
    [ComponentName("Add permanent buff in terrain")]
    [AllowMultipleComponents]
    [AllowedOn(typeof(BlueprintUnitFact), false)]
    public class AddBuffInTerrain : UnitFactComponentDelegate, ITeleportHandler, IGlobalSubscriber, ISubscriber {
        public AreaSetting Terrain;
        public BlueprintBuffReference m_Buff;
        public BlueprintBuff Buff => m_Buff?.Get();

        public bool CurrentAreaPartIsFavoredTerrain => AreaService.Instance.CurrentAreaSetting == Terrain;

        public override void OnTurnOn() {
            UpdateModifiers();
        }

        public override void OnTurnOff() {
            RemoveBuff();
        }

        public void UpdateModifiers() {
            if (CurrentAreaPartIsFavoredTerrain) {
                ApplyBuff();
            } else {
                RemoveBuff();
            }
        }


        public void ApplyBuff() {
            base.Owner.AddBuff(Buff, base.Owner)?.MakePermanent();

        }
        public void RemoveBuff() {
            base.Owner.RemoveFact(Buff);
        }
        public void HandlePartyTeleport(AreaEnterPoint enterPoint) {
            UpdateModifiers();
        }

    }
}
