using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class StatData
    {
        public float MaxHealth { get; set; }
        public float MaxMP { get; set; }
        public float CurrentHealth { get; set; }
        public float CurrentMP { get; set; }
        public float Attack { get; set; }
        public float Defense { get; set; }



        public void AddStat(StatData targetStat)
        {
            MaxHealth += targetStat.MaxHealth;
            MaxMP += targetStat.MaxMP;
            CurrentHealth += targetStat.CurrentHealth;
            CurrentMP += targetStat.CurrentMP;
            Attack += targetStat.Attack;
            Defense += targetStat.Defense;


            if (MaxHealth < CurrentHealth)
            {
                CurrentHealth = MaxHealth;
            }

            if (MaxMP < CurrentMP)
            {
                CurrentMP = MaxMP;
            }
        }


        public void RemoveStat(StatData targetStat)
        {
            MaxHealth -= targetStat.MaxHealth;
            MaxMP -= targetStat.MaxMP;
            CurrentHealth -= targetStat.CurrentHealth;
            CurrentMP -= targetStat.CurrentMP;
            Attack -= targetStat.Attack;
            Defense -= targetStat.Defense;
        }
    }
}
