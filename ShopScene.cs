using SprtaaaaDungeon;
using System;
using System.Globalization;
using System.Net.NetworkInformation;

public class ShopScene : Scene
{
	private Shop shop;
    private PlayerData playerData;
    public ShopScene(SceneController controller) : base(controller)
    {
        shop = new(); //this.shop = new Shop();
    }

    public override void Start() //시작될때 1번
    {
        playerData = GameManager.Instance.PlayerData;

        shop.currentType = GameEnum.ItemType.Weapon;

        DrawCategoryItems();
    }

    public override void Update()// 계속 매프레임
    {
        string input = Console.ReadLine();

        if(input == "")
        {
            Console.Clear();

            shop.currentType = shop.currentType == GameEnum.ItemType.Armor ?
                GameEnum.ItemType.Weapon :
                GameEnum.ItemType.Armor;

            DrawCategoryItems();
        }
        else if(input == "1")
        {
            
            Console.Write(shop.ShopItemDict[shop.currentType][int.Parse(input) -1].ItemData.Name);
            Console.WriteLine("이 구매되었습니다.");
            Console.WriteLine($"보유 골드 : {playerData.Gold}");
        }
    }

    public override void End() //나갈때 한번!
    {
    }

    void DrawCategoryItems()
    {
        foreach (var item in shop.ShopItemDict)
        {
            if (item.Key == shop.currentType)
            {
                List<ShopItemData> shopItemDatas = item.Value;

                for (int i = 0; i < shopItemDatas.Count; i++)
                {
                    Console.WriteLine($"{shopItemDatas[i].ItemData.Name}");
                }
            }
        }
    }
}
