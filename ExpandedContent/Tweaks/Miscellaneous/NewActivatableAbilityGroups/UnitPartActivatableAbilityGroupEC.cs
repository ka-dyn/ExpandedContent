using Kingmaker.UnitLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExpandedContent.Tweaks.Miscellaneous.NewActivatableAbilityGroups.NewActivatableAbilityGroupAdder;

namespace ExpandedContent.Tweaks.Miscellaneous.NewActivatableAbilityGroups {
    internal class UnitPartActivatableAbilityGroupEC : OldStyleUnitPart {
        public void IncreaseGroupSize(ECActivatableAbilityGroup group) {
            if (m_GroupsSizeIncreases.ContainsKey(group)) {
                this.m_GroupsSizeIncreases[group] += 1;
            } else {
                m_GroupsSizeIncreases.Add(group, 1);
            }
        }

        public void DecreaseGroupSize(ECActivatableAbilityGroup group) {
            if (m_GroupsSizeIncreases.ContainsKey(group)) {
                this.m_GroupsSizeIncreases[group] -= 1;
            }
        }

        public int GetGroupSize(ECActivatableAbilityGroup group) {
            this.m_GroupsSizeIncreases.TryGetValue(group, out int result);
            return result + 1;
        }

        private SortedDictionary<ECActivatableAbilityGroup, int> m_GroupsSizeIncreases = new SortedDictionary<ECActivatableAbilityGroup, int>();
    }
}
