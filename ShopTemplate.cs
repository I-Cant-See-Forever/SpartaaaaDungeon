using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class ShopTemplate
    {
        Dictionary<GameEnum.ItemType, List<ItemData>> testItemDict = new();

        public void Init()
        {
            foreach (var item in GameManager.Instance.GameItems)
            {
                if(testItemDict.ContainsKey(item.Type))
                {
                    testItemDict[item.Type].Add(item);
                }
                else
                {
                    testItemDict.Add(item.Type, new List<ItemData>());

                    testItemDict[item.Type].Add(item);
                }
            }
        }
    }
}
