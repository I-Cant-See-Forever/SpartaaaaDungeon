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


        List<InventoryItemData> currentItemList = new();

        public InventoryScene(SceneController controller) : base(controller) { }


        public override void Start()
        {
            inventoryController = GameManager.Instance.InventoryController;


            currentPage = 0;

            currentItemList = inventoryController.SetCurrentItemList((GameEnum.ItemType)currentPage);


            DrawFrame();

            DrawDirectImage(TextContainer.inventoryTitle, layout.Top.X, layout.Top.Y, ConsoleColor.Green);

            DrawItems(currentItemList);

            DrawInputIfo();
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


    }
}
