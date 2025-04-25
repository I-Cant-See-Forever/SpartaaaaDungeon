using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class InventoryItemData
    {
        public ItemData ItemData { get; set; }
        public int Count { get; set; }
        public bool IsEquip { get; private set; }


        public InventoryItemData(ItemData itemData, int count, bool isEquip) 
        {
            ItemData = itemData;
            Count = count;
            IsEquip = isEquip;
        }
    }
}
