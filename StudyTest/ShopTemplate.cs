using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon.StudyTest
{
    public class ShopTemplate
    {
        List<ShopItemData> shopItems;

        Dictionary<GameEnum.ItemType, List<ShopItemData>> shopItemDict = new(); //카테고리 상점아이템

        public ShopTemplate() //초기 1회 실행
        {
            shopItems = GameManager.Instance.ShopItems;

            //저장된, 혹은 초기화된 상점아이템 불러오고 순회

            foreach (var shopItem in shopItems)
            {
                GameEnum.ItemType type = shopItem.ItemData.Type;

                if (shopItemDict.ContainsKey(type))
                {
                    shopItemDict[type].Add(shopItem);
                }
                else
                {
                    shopItemDict.Add(type, new List<ShopItemData>()); // 새 카테고리에 리스트생성
                    shopItemDict[type].Add(shopItem);
                }
            }
        }
    }
}
