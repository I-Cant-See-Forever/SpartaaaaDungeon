using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class PlayerData : CharacterData
    {
        public GameEnum.ClassType ClassType { get; set; }
        public float Gold { get; set; }

        public float CurrentExp { get; set; }
        public float MaxExp { get; set; }


        public void addExp(float exp)
        {
            CurrentExp += exp;

            if(CurrentExp >= MaxExp)
            {
                Level++;
                CurrentExp = 0;

                StatData.Attack += 0.5f;
                StatData.Defense += 1f;

                switch(Level)
                {
                   case 2:MaxExp = 35;break;
                   case 3:MaxExp = 65;break;
                   case 4:MaxExp = 100;break;
                }
            }
        }

    }
}
