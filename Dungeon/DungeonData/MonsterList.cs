using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    class GoblinWizard : DungeonData
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public float Attack { get; set; }
        public float Health { get; set; }

        public GoblinWizard(int levelup)
        {
            Name = "고블린마법사";
            Level = 1 + levelup;
            Attack = 20 + (levelup * 2);
            Health = 20 + (levelup * 5);
        }
    }

    class Dragon : DungeonData
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public float Attack { get; set; }
        public float Health { get; set; }

        public Dragon(int levelup)
        {
            Name = "용";
            Level = 1 + levelup;
            Attack = 20 + (levelup * 2);
            Health = 20 + (levelup * 5);
        }
    }
    class Golem : DungeonData
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public float Attack { get; set; }
        public float Health { get; set; }

        public Golem(int levelup)
        {
            Name = "골렘";
            Level = 1 + levelup;
            Attack = 20 + (levelup * 2);
            Health = 20 + (levelup * 5);
        }
    }
    class Banddit : DungeonData
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public float Attack { get; set; }
        public float Health { get; set; }

        public Banddit(int levelup)
        {
            Name = "도적";
            Level = 1 + levelup;
            Attack = 20 + (levelup * 2);
            Health = 20 + (levelup * 5);
        }
    }
    class Rich : DungeonData
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public float Attack { get; set; }
        public float Health { get; set; }

        public Rich(int levelup)
        {
            Name = "리치";
            Level = 1 + levelup;
            Attack = 20 + (levelup * 2);
            Health = 20 + (levelup * 5);
        }
    }
    class Vampire : DungeonData
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public float Attack { get; set; }
        public float Health { get; set; }

        public Vampire(int levelup)
        {
            Name = "뱀파이어";
            Level = 1 + levelup;
            Attack = 20 + (levelup * 2);
            Health = 20 + (levelup * 5);
        }
    }
    class WareWolf : DungeonData
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public float Attack { get; set; }
        public float Health { get; set; }

        public WareWolf(int levelup)
        {
            Name = "늑대인간";
            Level = 1 + levelup;
            Attack = 20 + (levelup * 2);
            Health = 20 + (levelup * 5);
        }
    }
    class Zombie : DungeonData
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public float Attack { get; set; }
        public float Health { get; set; }

        public Zombie(int levelup)
        {
            Name = "좀비";
            Level = 1 + levelup;
            Attack = 20 + (levelup * 2);
            Health = 20 + (levelup * 5);
        }
    }
    class SkeletonArchor : DungeonData
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public float Attack { get; set; }
        public float Health { get; set; }

        public SkeletonArchor(int levelup)
        {
            Name = "해골궁수";
            Level = 1 + levelup;
            Attack = 20 + (levelup * 2);
            Health = 20 + (levelup * 5);
        }
    }
    class SkeletonWarrior : DungeonData
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public float Attack { get; set; }
        public float Health { get; set; }

        public SkeletonWarrior(int levelup)
        {
            Name = "해골전사";
            Level = 1 + levelup;
            Attack = 20 + (levelup * 2);
            Health = 20 + (levelup * 5);
        }
    }


}
