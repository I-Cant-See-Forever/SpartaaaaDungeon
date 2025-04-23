using SprtaaaaDungeon;
using System;
using System.Security.Cryptography.X509Certificates;

public class Shop
{
    List<ShopItemData> shopItems;
    private Dictionary<GameEnum.ItemType, List<ShopItemData>> shopItemDict = new ();
    public Dictionary<GameEnum.ItemType, List<ShopItemData>> ShopItemDict => shopItemDict;  

    private Dictionary<GameEnum.ItemType, List<InventoryItemData>> inventoryItemDict = new ();
    
    public Dictionary<GameEnum.ItemType, List<InventoryItemData>> InventoryItemDict => inventoryItemDict;

    //집합체, 아이템 하나를 만든게 아님. 타입을 나눠 분배하는 딕셔너리

    public GameEnum.ItemType currentType;

    public ItemData ItemData;

    public Shop()
	{
        shopItems = GameManager.Instance.ShopItems;

        //저장된, 혹은 초기화된 상점아이템 불러오고 순회

        foreach (var shopItem in shopItems)
        {
            GameEnum.ItemType type = shopItem.ItemData.Type;

            if (shopItemDict.ContainsKey(type)) // shopItems의 shopItem의 type(enum)에 키가 있으면
            {
                shopItemDict[type].Add(shopItem); //shopItemDict의 그 shopItem 추가(Add)
            }
            else // shopItems의 shopItem의 type(enum)에 키가 없으면
            {
                shopItemDict.Add(type, new List<ShopItemData>()); // 새 카테고리에 리스트생성, 
                shopItemDict[type].Add(shopItem); //shopItemDict의 그 shopItem 추가(Add)
            }
        }
    }

    public ItemData GetItemData(string findName) => GameManager.Instance.GameItems.FirstOrDefault(item => item.Name == findName);

}
