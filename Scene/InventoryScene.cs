using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class InventoryScene : Scene
    {
        int currentPage;

        InventoryController inventoryController;

        ItemLayout layout = new();


        //int maxPage; maxPage 는 곧 리스트의 길이이므로 필요없음

        List<InventoryItemData> currentItemList = new();

        public InventoryScene(SceneController controller) : base(controller)
        {
            //게임매니저에서 가져와야함!
            //inventoryController = new InventoryController();


            //카테고리를 이차원 리스트로 바꿨어요~
            //maxPage = inventoryController.CategorizedItems.Count - 1;

            /*foreach (var item in inventoryController.CategorizedItems)
            {
                itemTypeList.Add(item.Key);
            }*/
        }

        public override void Start()
        {
            inventoryController = GameManager.Instance.InventoryController;

            currentPage = 0;

            currentItemList = inventoryController.SetCurrentItemList((GameEnum.ItemType)currentPage);

            Frame();
            DrawTitle();
            DrawItems(currentItemList);
            DrawInputIfo();


            /*Console.Clear();
            itemTypeList.Clear();
            foreach (var item in inventoryController.CategorizedItems)
            {
                itemTypeList.Add(item.Key);
            }

            currentPage = itemTypeList.Count > 0 ? 0 : -1;
            maxPage = itemTypeList.Count - 1;

            // 진단용 디버깅 출력
            Console.WriteLine("===== [디버그 정보] =====");
            Console.WriteLine($"- inventoryController.Items.Count: {inventoryController.Items.Count}");
            Console.WriteLine($"- itemTypeList.Count: {itemTypeList.Count}");
            Console.WriteLine($"- currentPage: {currentPage}");
            Console.WriteLine($"- maxPage: {maxPage}");
            Console.WriteLine($"- CategorizedItems.Count: {inventoryController.CategorizedItems.Count}");
            Console.WriteLine("=========================\n");

            Console.WriteLine("인벤토리\n보유중인 아이템을 관리할 수 있습니다.\n");

            if (inventoryController.Items.Count == 0 || itemTypeList.Count == 0 || currentPage == -1)
            {
                Console.WriteLine("보유한 아이템이 없습니다.");
            }
            else
            {
                Console.WriteLine("[아이템 목록]");
                inventoryController.PrintItems(itemTypeList[currentPage]);
            }
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("2. 목록 전환");
            Console.WriteLine("0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");*/
        }

        public override void End()
        {
        }

        public override void Update()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);

                bool isInventoryInput = false;

                switch (input.Key)
                {
                    case ConsoleKey.LeftArrow:
                        currentPage = GetMoveSelectIndex(currentPage, -1, 2, true);
                        isInventoryInput = true;
                        break;
                    case ConsoleKey.RightArrow:
                        currentPage = GetMoveSelectIndex(currentPage, +1, 2, true);
                        isInventoryInput = true;
                        break;
                    case ConsoleKey.I:
                        controller.ChangeScene(controller.PreviousScene);
                        break;
                    case ConsoleKey.Escape:
                        controller.ChangeScene(controller.PreviousScene);
                        break;
                    default:
                        SelectIndexItem(input);
                        isInventoryInput = true;
                        break;
                }

                if (isInventoryInput)
                {
                    var centerRectToFrame = new Rectangle(layout.Center.X + 1, layout.Center.Y, layout.Center.Width - 3, layout.Center.Height);

                    DrawRemoveRect(centerRectToFrame);


                    currentItemList = inventoryController.SetCurrentItemList((GameEnum.ItemType)currentPage);

                    DrawItems(currentItemList);
                }
            }


            //입력방식이 ReadLine이 아닌 ReadKey 로 할꺼라 바꿀수밖에 없어요 ㅠㅠ
            /*string input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    controller.ChangeScene(controller.PreviousScene);

                    break;
                case "2":
                    if (currentPage < maxPage)
                    {
                        currentPage++;
                    }
                    else
                    {
                        currentPage = 0;
                    }
                    Console.Clear();
                    Console.Write(itemTypeList.Count);
                    Console.Write(itemTypeList[currentPage]);
                    inventoryController.PrintItems(itemTypeList[currentPage]);
                    break;
                case "1":
                    while (true)
                    {
                        Console.Clear();
                        inventoryController.PrintItems(itemTypeList[currentPage]);
                        Console.WriteLine("\na. 목록 전환");
                        Console.WriteLine("\n0. 나가기");
                        Console.Write("\n장착/해제할 아이템 번호를 입력하세요\n>> ");
                        string select = Console.ReadLine();
                        int index;

                        select = select.ToLower();
                        if (select == "ㅁ") select = "a";
                        if (select == "a")
                        {
                            if (currentPage < maxPage)
                            {
                                currentPage++;
                            }
                            else
                            {
                                currentPage = 0;
                            }
                            Console.Clear();
                            inventoryController.PrintItems(itemTypeList[currentPage]);
                        }

                        else if (int.TryParse(select, out index))
                        {
                            if (select == "0")
                            {
                                Console.Clear();
                                Start();
                                break;
                            }

                            index -= 1;

                            if (!inventoryController.CategorizedItems.ContainsKey(itemTypeList[currentPage]))
                            {
                                Console.WriteLine("\n[오류] 현재 카테고리에 아이템이 없습니다.");
                                Console.WriteLine("Enter를 눌러 계속...");
                                Console.ReadLine();
                                continue;
                            }

                            List<InventoryItemData> currentList = inventoryController.CategorizedItems[itemTypeList[currentPage]];
                            if (index >= 0 && index < currentList.Count)
                            {
                                InventoryItemData item = currentList[index];

                                if (index < 0 || index >= currentList.Count)
                                {
                                    Console.WriteLine("\n[오류] 존재하지 않는 아이템 번호입니다.");
                                    Console.WriteLine("Enter를 눌러 계속...");
                                    Console.ReadLine();
                                    continue;
                                }

                                else if (item.ItemData.Type == GameEnum.ItemType.Consumable)
                                {
                                    Console.WriteLine("\n소비 아이템은 장착할 수 없습니다.");
                                    Console.WriteLine("\nEnter를 눌러 계속");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    bool isEquipped = inventoryController.ToggleEquipState(item);
                                    Console.WriteLine(isEquipped
                                        ? $"\n'{item.ItemData.Name}' 장착 완료"
                                        : $"\n'{item.ItemData.Name}' 장착 해제됨");
                                    Console.WriteLine("\nEnter를 눌러 계속");
                                    Console.ReadLine();

                                    Console.Clear();
                                    inventoryController.PrintItems(itemTypeList[currentPage]);
                                }
                            }
                            else
                            {
                                Console.WriteLine("\n존재하지 않는 아이템 번호입니다");
                                Console.WriteLine("\nEnter를 눌러 계속");
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("잘못된 입력입니다.");
                            Console.ReadLine();
                        }
                    }

                    Console.ReadLine();
                    Start();
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadLine();
                    Start();
                    break;
            }*/
        }


        void Frame()
        {
            DrawString($"《x0,y0》┏━《l{Console.WindowWidth - 5}》━《》━┓");

            for (int y = 1; y < Console.WindowHeight - 1; y++)
            {
                DrawString($"《x0,y{y}》┃《x{Console.WindowWidth - 2}》┃");
            }

            DrawString($"《x0,y{Console.WindowHeight - 1}》┗━《l{Console.WindowWidth - 5}》━《》━┛");
        }

        void DrawTitle()
        {
            string title = $"《x{layout.Top.X},y{layout.Top.Y},tgreen》" +
                $"██╗███╗   ██╗██╗   ██╗███████╗███╗   ██╗ ██████╗ ████████╗██████╗ ██╗   ██╗\r\n" +
                $"██║████╗  ██║██║   ██║██╔════╝████╗  ██║██╔═══██╗╚══██╔══╝██╔══██╗╚██╗ ██╔╝\r\n" +
                $"██║██╔██╗ ██║██║   ██║█████╗  ██╔██╗ ██║██║   ██║   ██║   ██████╔╝ ╚████╔╝ \r\n" +
                $"██║██║╚██╗██║╚██╗ ██╔╝██╔══╝  ██║╚██╗██║██║   ██║   ██║   ██╔══██╗  ╚██╔╝  \r\n" +
                $"██║██║ ╚████║ ╚████╔╝ ███████╗██║ ╚████║╚██████╔╝   ██║   ██║  ██║   ██║   \r\n" +
                $"╚═╝╚═╝  ╚═══╝  ╚═══╝  ╚══════╝╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝  ╚═╝   ╚═╝  ";
            string replaceTitle = title.Replace("\r\n", $"\n《x{layout.Top.X},tgreen》");

            DrawString(replaceTitle);
        }

  
        void DrawItems(List<InventoryItemData> itemList)
        {
            DrawString($"《x{layout.Center.X + 10},y{layout.Center.Y},tyellow》{(GameEnum.ItemType)currentPage} ");

            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].IsEquip)
                {
                    DrawString($"《x{layout.Center.X + 7},y{layout.Center.Y + i + 2},tGreen》[E]");
                }

                DrawString($"《y{layout.Center.Y + i + 2}》" +
                    $"《x{layout.Center.X + 10},tmagenta》[{i+1}]《》{itemList[i].Data.Name}" +
                    $"《x{layout.Center.X + 27}》|  {DrawTargetItemStat(itemList[i].Data.StatData)}" +
                    $"《x{layout.Center.X + 92}》|  《tmagenta》{itemList[i].Count} 《》개");
            }
        }

        void SelectIndexItem(ConsoleKeyInfo input)
        {
            if (int.TryParse(input.KeyChar.ToString(), out int selectNum))
            {
                if (selectNum > 0 && selectNum <= currentItemList.Count)
                {
                    inventoryController.SelectItem(currentItemList, selectNum - 1);
                }
            }
        }

        void DrawInputIfo()
        {
            DrawString($"《x{layout.Bottom.Width/2 - 10},y{layout.Bottom.Y},tmagenta》◀ 《》이동《tmagenta》 ▶");
        }

        string DrawTargetItemStat(StatData statData)
        {
            string statString = null;

            if(statData.Attack > 0)
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

        /*  void DrawItemsFrame()
        {
            int left = layout.Center.X + 6;
            int right = layout.Center.Width - left;

            DrawString($"《x{left},y{layout.Center.Y}》┏━《l{layout.Center.Width - left - 11}》━《》━┓");

            for (int y = 1; y < layout.Center.Height - 3; y++)
            {
                DrawString($"《x{left},y{layout.Center.Y + y}》┃《x{right - 2}》┃");
            }

            DrawString($"《x{left},y{layout.Center.Y + layout.Center.Height - 3}》┗━《l{layout.Center.Width - left - 11}》━《》━┛");
        }*/

    }
}
