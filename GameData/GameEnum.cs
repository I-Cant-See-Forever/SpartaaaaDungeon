using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class GameEnum
{
    public enum ClassType
    {
        Warrior,
        Archer,
        Mage,
        Assassin
    }
    public enum ItemType
    {
        Weapon,
        Armor,
        Consumable
    }

    public enum SkillType
    { 
        Attack,
        Heal
    }

    public enum ProfileType
    {
        Light,
        Hero,
        Clown,
    }
}
