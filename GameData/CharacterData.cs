using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class CharacterData
    {
        public string Name { get; set; }
        public int Level { get; set; }

        public StatData StatData { get; set; }

        public CharacterData()
        {
            StatData = new StatData();
        }
    }
}
