using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public abstract class SkillData
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public float CostHP { get; set; } 
        public float CostMP { get; set; }

        public float Multiplier { get; set; }

        public int TargetCount { get; set; }
        public abstract void UseSkill(CharacterData caster, List<CharacterData> targets, out float resultValue);
    }
}
