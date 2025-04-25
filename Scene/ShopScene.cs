using SprtaaaaDungeon;
using System;
using System.Globalization;
using System.Net.NetworkInformation;

public class ShopScene : Scene
{
    int totalWidth = 230; //공백

    bool posShop;

    bool posShopMain;
    bool posShopPurchase;
    bool posShopSell;

    bool purItemOfTypeLineUp;
    bool purCatBuy;

    Shop shop;
    PlayerData playerData;
    List<InventoryItemData> inventoryItemData;

    public ShopScene(SceneController controller) : base(controller)
    {
        shop = new(); //this.shop = new Shop();
    }

    public override void Start() //시작될때 1번
    {
        shop.ShopStart();
        //DrawCategoryItems();
    }

    public override void Update()// 계속 매프레임
    {
        shop.InventoryItemByType();
        shop.EnterShop();
        

        //if(input == "")
        //{
        //    Console.Clear();

        //    shop.currentType = shop.currentType == GameEnum.ItemType.Armor ?
        //        GameEnum.ItemType.Weapon :
        //        GameEnum.ItemType.Armor;

        //    DrawCategoryItems();
        //}
        //else if(input == "1")
        //{

        //    Console.Write(shop.ShopItemDict[shop.currentType][int.Parse(input) -1].ItemData.Name);
        //    Console.WriteLine("이 구매되었습니다.");
        //    Console.WriteLine($"보유 골드 : {playerData.Gold}");
        //}
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
                // ShopItemDict의 Value에 저장되있던(이미 있었던 or 추가된 List<ShopItemData>) 데이터를 변수 shopItemDatas에 넣음

                for (int i = 0; i < shopItemDatas.Count; i++)
                {
                    Console.WriteLine($"{shopItemDatas[i].ItemData.Name}");
                }
            }
        }
    }

    int TestCheckInput(int min, int max)
    {
        int result;

        while (true)
        {
            Console.Write("\n입력 : ");

            string input = Console.ReadLine();
            bool isNumber = int.TryParse(input, out result);
            if (isNumber)
            {
                if (result >= min && result <= max)
                {
                    return result;
                }
            }

            Console.WriteLine("잘못된 입력입니다!!");
            Thread.Sleep(1000);
        }
    }
}
