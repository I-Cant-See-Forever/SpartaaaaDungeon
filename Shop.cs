using SprtaaaaDungeon;
using System;
using System.Security.Cryptography.X509Certificates;

public class Shop
{
    public ItemData ItemData;

    

    public Shop()
	{
        //GameManager.Instance.GameItems = new()
        //{
        //    new("테스트무기",GameEnum.ItemType.Weapon , 100, new(1,1,1,1))
        //};


        GameManager.Instance.ShopItems = new()
        {
            new(GameManager.Instance.GetItemData("칼날검"), 1),
        };
    }

    public ItemData GetItemData(string findName) => GameManager.Instance.GameItems.FirstOrDefault(item => item.Name == findName);

    public void itemShop()
	{



    }

}
