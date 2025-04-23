using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    class GoblinWizard : DungeonData
    {

        public GoblinWizard(int levelup) : base
            (name: "고블린마법사", level: 0 + levelup, attack: 2 + levelup * 2, currentHealth: 10 + levelup * 2, maxHealth : 10 + levelup *2)
        {

        }
    }

    class Dragon : DungeonData
    {

        public Dragon(int levelup) : base
            (name: "용", level: 0 + levelup, attack: 2 + levelup * 2, currentHealth: 10 + levelup * 2, maxHealth: 10 + levelup * 2)
        {

        }
    }
    class Golem : DungeonData
    {

        public Golem(int levelup) : base
            (name: "골렘", level: 0 + levelup, attack: 2 + levelup * 2, currentHealth: 10 + levelup * 2, maxHealth: 10 + levelup * 2)
        {

        }
    }
    class Banddit : DungeonData
    {


        public Banddit(int levelup) : base
            (name: "도적", level: 0 + levelup, attack: 2 + levelup * 2, currentHealth: 10 + levelup * 2, maxHealth: 10 + levelup * 2)
        {

        }
    }
    class Rich : DungeonData
    {

        public Rich(int levelup) : base
            (name: "리치", level: 0 + levelup, attack: 2 + levelup * 2, currentHealth: 10 + levelup * 2, maxHealth: 10 + levelup * 2)
        {

        }
    }
    class Vampire : DungeonData
    {


        public Vampire(int levelup) : base
            (name: "뱀파이어", level: 0 + levelup, attack: 2 + levelup * 2, currentHealth: 10 + levelup * 2, maxHealth: 10 + levelup * 2)
        {

        }
    }
    class WareWolf : DungeonData
    {

        public WareWolf(int levelup) : base
            (name: "늑대인간", level: 0 + levelup, attack: 2 + levelup * 2, currentHealth: 10 + levelup * 2, maxHealth: 10 + levelup * 2)
        {

        }
    }
    class Zombie : DungeonData
    {


        public Zombie(int levelup) : base
            (name: "좀비", level: 0 + levelup, attack: 2 + levelup * 2, currentHealth: 10 + levelup * 2, maxHealth: 10 + levelup * 2)
        {

        }
    }
    class SkeletonArchor : DungeonData
    {

        public SkeletonArchor(int levelup) : base
            (name: "해골궁수", level: 0 + levelup, attack: 2 + levelup * 2, currentHealth: 10 + levelup * 2, maxHealth: 10 + levelup * 2)
        {

        }
    }
    class SkeletonWarrior : DungeonData
    {


        public SkeletonWarrior(int levelup) : base
            (name: "해골전사", level: 0 + levelup, attack: 2 + levelup * 2, currentHealth: 10 + levelup * 2, maxHealth: 10 + levelup * 2)
        {

        }
    }


}
