using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class AttackSkillData : SkillData
    {
        public override void UseSkill(CharacterData caster, List<CharacterData> targets, out float resultValue)
        {
            resultValue = caster.StatData.Attack * Multiplier;

            foreach (var item in targets)
            {
                item.StatData.CurrentHealth -= resultValue;
            }
        }
    }
}
