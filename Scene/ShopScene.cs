using SprtaaaaDungeon;
using System;
using System.Drawing;
using System.Globalization;
using System.Net.NetworkInformation;

public class ShopScene : Scene
{
    int currentPage;


    Shop shop;
    PlayerData playerData;
    ItemLayout layout = new();

    List<ShopItemData> shopCurrentItemList = new();
    List<InventoryItemData> inventoryCurrentItemList = new();

    public enum Phase
    { 
        Buy,
        Sell
    }
    Phase currentPhase;


    public ShopScene(SceneController controller) : base(controller)
    {
        shop = GameManager.Instance.Shop;
        playerData = GameManager.Instance.PlayerData;
    }

    public override void Start() //시작될때 1번
    {
        currentPage = 0;
        currentPhase = Phase.Buy;

        shopCurrentItemList = shop.GetBuyItemList((GameEnum.ItemType)currentPage);

        DrawFrame();

        DrawDirectImage(TextContainer.shopTitle, layout.Top.X, layout.Top.Y, ConsoleColor.Green);

        DrawInputIfo();

        SetCurrentPhase(currentPhase, (GameEnum.ItemType)currentPage);
    }

    public override void Update()// 계속 매프레임
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo input = Console.ReadKey(true);

            bool isInitInput = false;

            switch (input.Key)
            {
                case ConsoleKey.LeftArrow:
                    currentPage = GetMoveSelectIndex(currentPage, -1, 2, true);
                    isInitInput = true;
                    break;
                case ConsoleKey.RightArrow:
                    currentPage = GetMoveSelectIndex(currentPage, +1, 2, true);
                    isInitInput = true;
                    break;
                case ConsoleKey.I:
                    controller.ChangeScene<InventoryScene>();
                    break;
                case ConsoleKey.P:
                    controller.ChangeScene<StatScene>();
                    break;
                case ConsoleKey.Escape:
                    controller.ChangeScene<TownScene>();
                    break;
                case ConsoleKey.Tab:
                    currentPhase = currentPhase == Phase.Buy ? Phase.Sell : Phase.Buy;
                    isInitInput = true;
                    break;
                default:
                    switch(currentPhase)
                    {
                        case Phase.Buy: 
                            if(TryBuyIndexItem(input))
                            {
                                isInitInput = true;
                            }
                            break;

                        case Phase.Sell:
                            SellIndexItem(input);
                            isInitInput = true;
                            break;
                    }
                    break;
            }

            if (isInitInput)
            {
                var centerRectToFrame = new Rectangle(layout.Center.X + 1, layout.Center.Y, layout.Center.Width - 3, layout.Center.Height);

                DrawRemoveRect(centerRectToFrame);

                SetCurrentPhase(currentPhase, (GameEnum.ItemType)currentPage);
            }
        }
    }

    public override void End() //나갈때 한번!
    {
    }

    void SetCurrentPhase(Phase phase, GameEnum.ItemType itemType)
    {
        DrawString($"《x{layout.Center.X + 81},y{layout.Center.Y},tdarkgray》보유골드 :  《tyellow》G {playerData.Gold}");

        switch (phase)
        {
            case Phase.Buy:
                shopCurrentItemList = shop.GetBuyItemList(itemType);
                DrawBuyItems(shopCurrentItemList);
                break;
            case Phase.Sell:
                inventoryCurrentItemList = shop.GetSellItemList(itemType);
                DrawSellItems(inventoryCurrentItemList);
                break;
        }
    }

    bool TryBuyIndexItem(ConsoleKeyInfo input)
    {
        if (int.TryParse(input.KeyChar.ToString(), out int selectNum))
        {
            if (selectNum > 0 && selectNum <= shopCurrentItemList.Count)
            {
                if (shop.TryBuyItem(shopCurrentItemList, selectNum - 1))
                {
                    return true;
                }
            }
        }

        return false;
    }

    void SellIndexItem(ConsoleKeyInfo input)
    {
        if (int.TryParse(input.KeyChar.ToString(), out int selectNum))
        {
            if (selectNum > 0 && selectNum <= inventoryCurrentItemList.Count)
            {
                shop.SellItem(inventoryCurrentItemList, selectNum - 1);
            }
        }
    }


    void DrawBuyItems(List<ShopItemData> itemList)
    {
        DrawString($"《x{layout.Center.X + 10},y{layout.Center.Y},tyellow》{(GameEnum.ItemType)currentPage} - 《tblue》구매하기 ");

        for (int i = 0; i < itemList.Count; i++)
        {
            DrawString($"《y{layout.Center.Y + i + 2}》" +
                $"《x{layout.Center.X + 10},tmagenta》[{i + 1}]《》{itemList[i].ItemData.Name}" +
                $"《x{layout.Center.X + 27}》|  {DrawTargetItemStat(itemList[i].ItemData.StatData)}" +
                $"《x{layout.Center.X + 80}》|  《tmagenta》{itemList[i].Count} 《》개" +
                $"《x{layout.Center.X + 90}》|  《tyellow》G {(int)(itemList[i].ItemData.Price)}");
        }
    }
    void DrawSellItems(List<InventoryItemData> itemList)
    {
        DrawString($"《x{layout.Center.X + 10},y{layout.Center.Y},tyellow》{(GameEnum.ItemType)currentPage} - 《tred》판매하기");

        for (int i = 0; i < itemList.Count; i++)
        {
            DrawString($"《y{layout.Center.Y + i + 2}》" +
                $"《x{layout.Center.X + 10},tmagenta》[{i + 1}]《》{itemList[i].Data.Name}" +
                $"《x{layout.Center.X + 27}》|  {DrawTargetItemStat(itemList[i].Data.StatData)}" +
                $"《x{layout.Center.X + 80}》|  《tmagenta》{itemList[i].Count} 《》개"+
                $"《x{layout.Center.X + 90}》|  《tyellow》G {(int)(itemList[i].Data.Price * 0.8f)}");
        }
    }

    string DrawTargetItemStat(StatData statData)
    {
        string statString = null;

        if (statData.Attack > 0)
        {
            statString += $"공격력 + 《tred》{statData.Attack}  ";
        }

        if (statData.Defense > 0)
        {
            statString += $"《》방어력 + 《tyellow》{statData.Defense}  ";
        }

        if (statData.MaxHealth > 0)
        {
            statString += $"《》최대 체력 + 《tgreen》{statData.MaxHealth}  ";
        }

        if (statData.CurrentHealth > 0)
        {
            statString += $"《》현재 체력 + 《tgreen》{statData.CurrentHealth}  ";
        }

        if (statData.MaxMP > 0)
        {
            statString += $"《》최대 마나 + 《tcyan》{statData.MaxMP}  ";
        }

        if (statData.CurrentMP > 0)
        {
            statString += $"《》현재 마나 + 《tcyan》{statData.CurrentMP}  ";
        }

        return statString;
    }


    void DrawInputIfo()
    {
        DrawString($"《x{layout.Bottom.Width / 2 - 32},y{layout.Bottom.Y},tmagenta》[tab]《》 전환");

        DrawString($"《x{layout.Bottom.Width / 2 - 10},y{layout.Bottom.Y},tmagenta》◀ 《》이동《tmagenta》 ▶");
    }
}
