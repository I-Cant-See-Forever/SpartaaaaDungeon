using SprtaaaaDungeon;
using System;
using System.Globalization;
using System.Net.NetworkInformation;

public class ShopScene : Scene
{
    bool posShop;

    bool posShopMain;
    bool posShopPurchase;
    bool posShopSell;

    bool PurItemOfType;
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
        playerData = GameManager.Instance.PlayerData;
        inventoryItemData = GameManager.Instance.InventoryItems;

        posShop = true; // 상점 [0단계 진입/나감]
        posShopMain = true;  // 상점 : 기본 출력 (구매 판매) [0단계 출력]


        //DrawCategoryItems();
    }

    public override void Update()// 계속 매프레임
    {
        while (posShop)
        {
            Console.Clear();
            Console.WriteLine("1. 구매\n \n \n");
            Console.WriteLine("2. 판매\n");

            Console.WriteLine("0. 나가기");

            int mainInput;
            mainInput = TestCheckInput(0, 2);

            switch (mainInput)
            {
                case 0:
                    posShop = false;
                    break;
                case 1:
                    posShopPurchase = true;
                    while (posShopPurchase)
                    {
                        int purCatNum;
                        int itemTypeLength = Enum.GetNames(typeof(GameEnum.ItemType)).Length;

                        bool purCatTextdone = true;

                        Console.Clear();

                        if (purCatTextdone)
                        {
                            for (purCatNum = 0; purCatNum < itemTypeLength; purCatNum++)
                            {
                                Console.WriteLine($"{purCatNum + 1}. {(GameEnum.ItemType)purCatNum}");
                            }
                            purCatTextdone = false; //반복 출력 제거
                        }
                        Console.WriteLine("\n0. 나가기");
                        int typeNumberInput = TestCheckInput(0, itemTypeLength);

                        switch (typeNumberInput)
                        {
                            case 0:
                                posShopPurchase = false;
                                break;
                            default:
                                PurItemOfType = true;
                                int currentPurPage = 1;

                                while (PurItemOfType)
                                {
                                    Console.Clear();

                                    int tarCatNum = typeNumberInput - 1;

                                    shop.currentType = (GameEnum.ItemType)tarCatNum;

                                    foreach (var item in shop.ShopItemDict)
                                    {
                                        if (item.Key == shop.currentType)
                                        {
                                            List<ShopItemData> shopItemDatas = item.Value;
                                            // ShopItemDict의 Value에 저장되있던(이미 있었던 or 추가된 List<ShopItemData>) 데이터를 변수 shopItemDatas에 넣음
                                            // shop.currentType라는 Key값을 가진 데이터들의 Value만 shopItemDatas에 저장

                                            int maxPurPage = 1;
                                            int limitPurPageCount = 0;

                                            int i = 9 * (currentPurPage - 1);

                                            while (i < shopItemDatas.Count)
                                            {
                                                Console.WriteLine($"{i + 1}. {shopItemDatas[i].ItemData.Name}");
                                                i++;
                                                limitPurPageCount++;
                                                if (limitPurPageCount > 8)
                                                {
                                                    break;
                                                }
                                            }

                                            if (shopItemDatas.Count > 9)
                                            {
                                                maxPurPage = 2;
                                                if ((int)shopItemDatas.Count > 18)
                                                {
                                                    maxPurPage = 3;
                                                }
                                            }

                                            Console.WriteLine($"\n<현재 페이지 : {currentPurPage} / {maxPurPage} >");

                                            Console.WriteLine("\n1. 구매 창 보기");

                                            if (shopItemDatas.Count > 9)
                                            {
                                                if (currentPurPage == 1)
                                                { 
                                                    Console.WriteLine("\n ");
                                                    Console.WriteLine("3. 페이지 다음");
                                                }
                                                else if (currentPurPage == 2)
                                                {
                                                    Console.WriteLine("\n2. 페이지 이전");
                                                    Console.WriteLine("3. 페이지 다음");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("\n2. 페이지 이전");
                                                    Console.WriteLine(" "); ;
                                                }
                                            }    
                                            
                                            Console.WriteLine("\n0. 나가기");

                                            Console.Write("\n입력 : ");

                                            string itemInput = Console.ReadLine();

                                            switch (int.Parse(itemInput))
                                            {
                                                case 0:
                                                    PurItemOfType = false;
                                                    break;
                                                case 1:
                                                    purCatBuy = true;
                                                    while (purCatBuy) // 아이템 구매
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine("<구매하기>\n");
                                                        while (i < shopItemDatas.Count)
                                                        {
                                                            Console.WriteLine($"{i + 1}. {shopItemDatas[i].ItemData.Name}");
                                                            i++;
                                                            limitPurPageCount++;
                                                            if (limitPurPageCount > 8)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        Console.WriteLine($"\n< 현재 페이지 : {currentPurPage} / {maxPurPage} >");
                                                        Console.WriteLine("\n1. 아이템 구매하기");

                                                        int purItemInput = TestCheckInput(0, limitPurPageCount);
                                                        
                                                        switch (purItemInput)
                                                        {
                                                            case 0:
                                                                purCatBuy = false;
                                                                break;
                                                            default:

                                                                foreach (var invenitem in shop.InventoryItemDict)
                                                                {
                                                                    if (item.Key == shop.currentType)
                                                                    {
                                                                        List<InventoryItemData> invenItemDatas = invenitem.Value;
                                                                        // ShopItemDict의 Value에 저장되있던(이미 있었던 or 추가된 List<ShopItemData>) 데이터를 변수 shopItemDatas에 넣음

                                                                        int targetItem = purItemInput - 1 + (9 * (currentPurPage - 1));

                                                                        if (inventoryItemData.Any(item => item.ItemData.Name == shopItemDatas[targetItem].ItemData.Name))
                                                                        {
                                                                            Console.WriteLine("이미 구매한 아이템입니다!");
                                                                            Thread.Sleep(1000);
                                                                        }
                                                                        else
                                                                        {
                                                                            if (playerData.Gold < shopItemDatas[targetItem].ItemData.Price)
                                                                            {
                                                                                Console.WriteLine("골드가 부족합니다!"); //골드가 부족합니다
                                                                                Thread.Sleep(1000);
                                                                            }
                                                                            else
                                                                            {
                                                                                Console.WriteLine(shopItemDatas[targetItem].ItemData.Name + " 를 구매했습니다!");
                                                                                Thread.Sleep(1500);
                                                                                //invenItemDatas.Add(shopItemDatas[targetItem].ItemData, 1, false);
                                                                                playerData.Gold -= shopItemDatas[targetItem].ItemData.Price;
                                                                            }

                                                                        }
                                                                    }
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                case 2:
                                                    if (currentPurPage > 1)
                                                    {
                                                        currentPurPage--;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("잘못된 입력입니다!!");
                                                        Thread.Sleep(1000);
                                                    }
                                                        break;
                                                case 3:
                                                    if (currentPurPage < maxPurPage)
                                                    {
                                                        currentPurPage++;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("잘못된 입력입니다!!");
                                                        Thread.Sleep(1000);
                                                    }
                                                        break;
                                                default:
                                                    Console.WriteLine("잘못된 입력입니다!!");
                                                    Thread.Sleep(1000);
                                                    break;
                                            }
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case 2:
                    posShopSell = true;
                    posShopMain = false;
                    break;
                default:
                    break;
            }
        }
        

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
