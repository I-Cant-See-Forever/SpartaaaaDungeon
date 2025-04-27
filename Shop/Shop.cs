using SprtaaaaDungeon;
using System;

public class Shop
{
    PlayerData playerData;

    List<InventoryItemData> inventoryItemDatas;

    List<ShopItemData> shopItems;


    public Shop()
    {
        shopItems = GameManager.Instance.ShopItems;
        inventoryItemDatas = GameManager.Instance.InventoryItems;
        playerData = GameManager.Instance.PlayerData;

        shopItems = new()
        {
            new(GameManager.Instance.GetItemData("테스트무기0"), 1)
        };

        inventoryItemDatas = new()
        {
            new(GameManager.Instance.GetItemData("테스트무기0"), 1, true),
            new(GameManager.Instance.GetItemData("테스트무기0"), 2, false)
        };
    }


    public List<ShopItemData> GetBuyItemList(GameEnum.ItemType itemType)
    {
        var currentItemList = new List<ShopItemData>();

        foreach (var item in shopItems)
        {
            if (item.ItemData.Type == itemType)
            {
                currentItemList.Add(item);
            }
        }

        return currentItemList;
    }
    public List<InventoryItemData> GetSellItemList(GameEnum.ItemType itemType)
    {
        var currentItemList = new List<InventoryItemData>();

        foreach (var item in inventoryItemDatas)
        {
            if (item.Data.Type == itemType)
            {
                if (item.IsEquip == false)
                {
                    currentItemList.Add(item);
                }
            }
        }

        return currentItemList;
    }


    public bool TryBuyItem(List<ShopItemData> targetList, int itemIndex)
    {
        var targetItem = targetList[itemIndex];

        if (targetItem.Count > 0 && targetItem.ItemData.Price  <= playerData.Gold)
        {
            playerData.Gold -= targetItem.ItemData.Price;
            targetItem.Count -= 1;

            if (targetItem.Count == 0)
            {
                shopItems.Remove(targetItem);
            }

            return true;
        }

        return false;
    }

    public void SellItem(List<InventoryItemData> targetList, int itemIndex)
    {
        var targetItem = targetList[itemIndex];

        playerData.Gold += (int)(targetItem.Data.Price * 0.8f);
        targetItem.Count -= 1;

        if (targetItem.Count == 0)
        {
            inventoryItemDatas.Remove(targetItem);
        }
    }

}
