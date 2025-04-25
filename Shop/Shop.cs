using SprtaaaaDungeon;
using System;
using System.Security.Cryptography.X509Certificates;

public class Shop
{
    List<ShopItemData> shopItems;

    List<InventoryItemData> inventoryItems;

    private Dictionary<GameEnum.ItemType, List<ShopItemData>> shopItemDict = new();
    public Dictionary<GameEnum.ItemType, List<ShopItemData>> ShopItemDict => shopItemDict;

    private Dictionary<GameEnum.ItemType, List<InventoryItemData>> inventoryItemDict = new();
    public Dictionary<GameEnum.ItemType, List<InventoryItemData>> InventoryItemDict => inventoryItemDict;

    //집합체, 아이템 하나를 만든게 아님. 타입을 나눠 분배하는 딕셔너리

    public GameEnum.ItemType currentType;

    public ItemData ItemData;
    PlayerData playerData;

    int totalWidth = 230; //공백

    bool posShop = true;

    bool posShopMain = true;
    bool posShopPurchase = false;
    bool posShopSell = false;

    int categoryCount;
    int itemTypeLength = Enum.GetNames(typeof(GameEnum.ItemType)).Length;
    int typeNumberInput;

    int tarCatNum;
    List<InventoryItemData> inventoryItemDatas;
    string myinvenItemsCount;
    //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    //구매 부분 변수 초기화
    int currentPurPage;
    bool purItemList;
    bool purCatBuy;

    string soldItemCount;
    //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    //판매 부분 변수 초기화
    int currentSellPage;
    bool isSellItemList;
    bool sellCatBuy;

    public Shop()
    {
        shopItems = GameManager.Instance.ShopItems;

        //저장된, 혹은 초기화된 상점아이템 불러오고 순회

        foreach (var shopItem in shopItems)
        {
            GameEnum.ItemType type = shopItem.ItemData.Type;

            if (shopItemDict.ContainsKey(type)) // shopItems의 shopItem의 type(enum)에 키가 있으면
            {
                shopItemDict[type].Add(shopItem); // shopItemDict의 그 shopItem 추가(Add)
            }
            else // shopItems의 shopItem의 type(enum)에 키가 없으면
            {
                shopItemDict.Add(type, new List<ShopItemData>()); // 새 카테고리에 리스트생성, 
                shopItemDict[type].Add(shopItem); // shopItemDict의 그 shopItem 추가(Add)
            }
        }
    }

    public ItemData GetItemData(string findName) => GameManager.Instance.GameItems.FirstOrDefault(item => item.Name == findName);

    public void ShopStart()
    {
        playerData = GameManager.Instance.PlayerData;
        inventoryItems = GameManager.Instance.InventoryItems;

        foreach (var inventoryItem in inventoryItems)
        {
            GameEnum.ItemType type = inventoryItem.ItemData.Type;

            if (inventoryItemDict.ContainsKey(type)) // inventoryItems의 inventoryItem의 type(enum)에 키가 있으면
            {
                inventoryItemDict[type].Add(inventoryItem); // inventoryItemDict의 그 inventoryItem 추가(Add)
            }
            else // inventoryItems의 inventoryItem의 type(enum)에 키가 없으면
            {
                inventoryItemDict.Add(type, new List<InventoryItemData>()); // 새 카테고리에 리스트생성, 
                inventoryItemDict[type].Add(inventoryItem); // inventoryItemDict의 그 inventoryItem 추가(Add)
            }
        }

        posShop = true;
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
    public void EnterShop()
    {
        while (posShop)
        {
            Console.Clear();
            Console.WriteLine("                                                <상점>");
            Console.WriteLine($"보유 골드 : {playerData.Gold}".PadLeft(totalWidth)); // PadLeft : 텍스트 오른쪽 이동

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
                        ShowCategorySelect();
                        
                        typeNumberInput = TestCheckInput(0, itemTypeLength);

                        switch (typeNumberInput)
                        {
                            case 0:
                                posShopPurchase = false;
                                break;
                            default:
                                purItemList = true;
                                currentPurPage = 1;

                                while (purItemList)
                                {
                                    Console.Clear();
                                    Console.WriteLine("                                        <아이템 목록>");
                                    Console.WriteLine($"보유 골드 : {playerData.Gold}".PadLeft(totalWidth)); // PadLeft : 텍스트 오른쪽 이동

                                    tarCatNum = typeNumberInput - 1;

                                    currentType = (GameEnum.ItemType)tarCatNum;

                                    foreach (var inventoryitem in InventoryItemDict)
                                    {
                                        if (inventoryitem.Key == currentType)
                                        {
                                            inventoryItemDatas = inventoryitem.Value;
                                        }
                                    }

                                    foreach (var item in ShopItemDict)
                                    {
                                        if (item.Key == currentType)
                                        {
                                            List<ShopItemData> shopItemDatas = item.Value;
                                            // ShopItemDict의 Value에 저장되있던(이미 있었던 or 추가된 List<ShopItemData>) 데이터를 변수 shopItemDatas에 넣음
                                            // shop.currentType라는 Key값을 가진 데이터들의 Value만 shopItemDatas에 저장

                                            int maxPurPage = 1;
                                            int limitPurPageCount = 0;

                                            int i = 9 * (currentPurPage - 1);

                                            while (i < shopItemDatas.Count)
                                            {
                                                myinvenItemsCount = (shopItemDatas[i].ItemData.Type == GameEnum.ItemType.Consumable) ? $" {inventoryItemDatas[i].Count}개 " : " ";
                                                soldItemCount = (shopItemDatas[i].ItemData.Type == GameEnum.ItemType.Consumable) ? $" {shopItemDatas[i].Count}개 구매 가능" : " ";

                                                Console.WriteLine($"[{i + 1}. {shopItemDatas[i].ItemData.Name}] -{soldItemCount}" +
                                                    $" {(inventoryItems.Any(item => item.ItemData.Name == shopItemDatas[i].ItemData.Name) ? $"-{myinvenItemsCount}보유중" : " ")}");
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
                                                if (shopItemDatas.Count > 18)
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

                                            Console.WriteLine("\n0. 뒤로 가기");

                                            Console.Write("\n입력 : ");

                                            string itemInput = Console.ReadLine();

                                            switch (int.Parse(itemInput))
                                            {
                                                case 0:
                                                    purItemList = false;
                                                    break;
                                                case 1:
                                                    purCatBuy = true;
                                                    int buyNum;
                                                    while (purCatBuy) // 아이템 구매
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine("                                        <아이템 구매>");
                                                        Console.WriteLine($"보유 골드 : {playerData.Gold}".PadLeft(totalWidth)); // PadLeft : 텍스트 오른쪽 이동

                                                        limitPurPageCount = 0;
                                                        i = 9 * (currentPurPage - 1);
                                                        buyNum = 1;

                                                        while (i < shopItemDatas.Count)
                                                        {
                                                            myinvenItemsCount = (shopItemDatas[i].ItemData.Type == GameEnum.ItemType.Consumable) ? $" {inventoryItemDatas[i].Count}개 " : " ";
                                                            soldItemCount = (shopItemDatas[i].ItemData.Type == GameEnum.ItemType.Consumable) ? $" {shopItemDatas[i].Count}개 구매 가능" : " ";

                                                            Console.WriteLine($"{buyNum}. [{shopItemDatas[i].ItemData.Name}] -{soldItemCount}" +
                                                                $" {(inventoryItems.Any(item => item.ItemData.Name == shopItemDatas[i].ItemData.Name) ? $"-{myinvenItemsCount}보유중" : " ")}");
                                                            i++;
                                                            buyNum++;
                                                            limitPurPageCount++;
                                                            if (limitPurPageCount > 8)
                                                            {
                                                                break;
                                                            }
                                                        }
                                                        Console.WriteLine($"\n< 현재 페이지 : {currentPurPage} / {maxPurPage} >");
                                                        Console.WriteLine($"\n(1 ~ {(currentPurPage == maxPurPage ? shopItemDatas.Count % 9 : "9")}). 아이템 구매하기");

                                                        Console.WriteLine("\n0. 뒤로 가기");
                                                        int purItemInput = TestCheckInput(0, limitPurPageCount);

                                                        switch (purItemInput)
                                                        {
                                                            case 0:
                                                                purCatBuy = false;
                                                                break;
                                                            default:

                                                                int targetItem = purItemInput - 1 + (9 * (currentPurPage - 1));

                                                                if (inventoryItems.Any(item => item.ItemData.Name == shopItemDatas[targetItem].ItemData.Name))
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
                                                                        GameManager.Instance.InventoryItems.Add(new(GetItemData(shopItemDatas[targetItem].ItemData.Name), 1, false));
                                                                        playerData.Gold -= shopItemDatas[targetItem].ItemData.Price;
                                                                        Thread.Sleep(1500);
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
                    SellItemType();
                    break;
                default:
                    break;
            }
        }
    }
    private void SellItemType()
    {
        while(posShopSell)
        {
            ShowCategorySelect();

            typeNumberInput = TestCheckInput(0, itemTypeLength);

            switch (typeNumberInput)
            {
                case 0:
                    posShopSell = false;
                    break;
                default:
                    isSellItemList = true;
                    currentSellPage = 1;
                    SellItemList();
                    break;
            }
        }
    }
    private void SellItemList()
    {
        while (isSellItemList)
        {
            ShowSellItemList();

            tarCatNum = typeNumberInput - 1;

            currentType = (GameEnum.ItemType)tarCatNum;

            foreach (var item in InventoryItemDict)
            {
                if (item.Key == currentType)
                {
                    inventoryItemDatas = item.Value;
                    // ShopItemDict의 Value에 저장되있던(이미 있었던 or 추가된 List<ShopItemData>) 데이터를 변수 shopItemDatas에 넣음
                    // shop.currentType라는 Key값을 가진 데이터들의 Value만 shopItemDatas에 저장

                    //$"{(currentType == GameEnum.ItemType.Consumable) ? currentCount =

                    int maxSellPage = 1;
                    int limitSellPageCount = 0;

                    int i = 9 * (currentSellPage - 1);

                    while (i < inventoryItemDatas.Count)
                    {
                        myinvenItemsCount = (inventoryItemDatas[i].ItemData.Type == GameEnum.ItemType.Consumable) ? $" {inventoryItemDatas[i].Count}개 " : " ";

                        Console.WriteLine($"[{i + 1}. {inventoryItemDatas[i].ItemData.Name}] " +
                            $"{(inventoryItems.Any(item => item.ItemData.Name == inventoryItemDatas[i].ItemData.Name) ? $"-{myinvenItemsCount}보유중" : " ")}");
                        i++;
                        limitSellPageCount++;
                        if (limitSellPageCount > 8)
                        {
                            break;
                        }
                    }

                    if (inventoryItemDatas.Count > 9)
                    {
                        maxSellPage = 2;
                        if (inventoryItemDatas.Count > 18)
                        {
                            maxSellPage = 3;
                        }
                    }

                    Console.WriteLine($"\n<현재 페이지 : {currentSellPage} / {maxSellPage} >");

                    Console.WriteLine("\n1. 판매 창 보기");

                    if (inventoryItemDatas.Count > 9)
                    {
                        if (currentSellPage == 1)
                        {
                            Console.WriteLine("\n ");
                            Console.WriteLine("3. 페이지 다음");
                        }
                        else if (currentSellPage == 2)
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

                    Console.WriteLine("\n0. 뒤로 가기");

                    Console.Write("\n입력 : ");

                    string itemInput = Console.ReadLine();

                    switch (int.Parse(itemInput))
                    {
                        case 0:
                            isSellItemList = false;
                            break;
                        case 1:
                            sellCatBuy = true;
                            int sellsNum;
                            while (sellCatBuy) // 아이템 판매
                            {
                                Console.Clear();
                                Console.WriteLine("                                        <아이템 판매>");
                                Console.WriteLine($"보유 골드 : {playerData.Gold}".PadLeft(totalWidth)); // PadLeft : 텍스트 오른쪽 이동

                                limitSellPageCount = 0;
                                i = 9 * (currentSellPage - 1);
                                sellsNum = 1;

                                while (i < inventoryItemDatas.Count)
                                {
                                    Console.WriteLine($"{sellsNum}. [{inventoryItemDatas[i].ItemData.Name}] " +
                                        $"{(inventoryItems.Any(item => item.ItemData.Name == inventoryItemDatas[i].ItemData.Name) ? $"-{myinvenItemsCount}보유중" : " ")}");
                                    i++;
                                    sellsNum++;
                                    limitSellPageCount++;
                                    if (limitSellPageCount > 8)
                                    {
                                        break;
                                    }
                                }
                                Console.WriteLine($"\n< 현재 페이지 : {currentSellPage} / {maxSellPage} >");
                                Console.WriteLine($"\n(1 ~ {(currentSellPage == maxSellPage ? inventoryItemDatas.Count % 9 : "9")}). 아이템 판매하기");

                                Console.WriteLine("\n0. 뒤로 가기");
                                int SellInput = TestCheckInput(0, limitSellPageCount);

                                switch (SellInput)
                                {
                                    case 0:
                                        sellCatBuy = false;
                                        break;
                                    default:

                                        int targetItem = SellInput - 1 + (9 * (currentSellPage - 1));

                                        Console.WriteLine(inventoryItemDatas[targetItem].ItemData.Name + " 를 판매했습니다!");
                                        Console.WriteLine($"획득 골드 : {inventoryItemDatas[targetItem].ItemData.Price} G");

                                        playerData.Gold += inventoryItemDatas[targetItem].ItemData.Price;
                                        if (inventoryItemDatas[targetItem].Count > 1)
                                        {
                                            inventoryItemDatas[targetItem].Count--;
                                        }
                                        else
                                        {
                                            inventoryItemDatas.RemoveAt(targetItem);
                                        }


                                        Thread.Sleep(1500);
                                        break;
                                }
                            }
                            break;
                        case 2:
                            if (currentSellPage > 1)
                            {
                                currentSellPage--;
                            }
                            else
                            {
                                Console.WriteLine("잘못된 입력입니다!!");
                                Thread.Sleep(1000);
                            }
                            break;
                        case 3:
                            if (currentSellPage < maxSellPage)
                            {
                                currentSellPage++;
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
    }
    private void ShowCategorySelect()
    {
        Console.Clear();
        Console.WriteLine("                                        <구매 : 아이템 타입 선택>");
        Console.WriteLine($"보유 골드 : {playerData.Gold}".PadLeft(totalWidth)); // PadLeft : 텍스트 오른쪽 이동

        for (categoryCount = 0; categoryCount < itemTypeLength; categoryCount++)
        {
            Console.WriteLine($"{categoryCount + 1}. {(GameEnum.ItemType)categoryCount}");
        }
        Console.WriteLine("\n0. 뒤로 가기");
    }
    private void ShowSellItemList()
    {
        Console.Clear();
        Console.WriteLine("                                        <아이템 목록>");
        Console.WriteLine($"보유 골드 : {playerData.Gold}".PadLeft(totalWidth)); // PadLeft : 텍스트 오른쪽 이동
    }
}
