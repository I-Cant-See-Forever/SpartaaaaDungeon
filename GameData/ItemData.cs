using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class ItemData
    {
        public string Name { get; set; }
        public GameEnum.ItemType Type { get; set; }

        public float Price { get; set; }

        public StatData StatData { get; set; }

    }
}
