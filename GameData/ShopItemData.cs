using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class ShopItemData
    {
        public ItemData ItemData { get; set; }
        public int Count { get; set; }
        public bool IsSoldOut { get; set; }

        public ShopItemData(ItemData itemData, int count, bool isSoldOut)
        {
            ItemData = itemData;
            Count = count;
            IsSoldOut = isSoldOut;
        }
    }
}
