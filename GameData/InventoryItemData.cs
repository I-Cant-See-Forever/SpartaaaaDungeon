using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class InventoryItemData
    {
        public ItemData Data { get; set; }
        public int Count { get; set; }
        public bool IsEquip { get; set; }


        public InventoryItemData(ItemData data, int count, bool isEquip) 
        {
            Data = data;
            Count = count;
            IsEquip = isEquip;
        }
    }
}
