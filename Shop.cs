using SprtaaaaDungeon;
using System;
using System.Security.Cryptography.X509Certificates;

public class Shop
{
    List<ShopItemData> shopItems;
    private Dictionary<GameEnum.ItemType, List<ShopItemData>> shopItemDict = new ();
    public Dictionary<GameEnum.ItemType, List<ShopItemData>> ShopItemDict => shopItemDict;

    public GameEnum.ItemType currentType;

    public ItemData ItemData;

    public Shop()
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

    public ItemData GetItemData(string findName) => GameManager.Instance.GameItems.FirstOrDefault(item => item.Name == findName);

    public void itemShop()
	{



    }

}
