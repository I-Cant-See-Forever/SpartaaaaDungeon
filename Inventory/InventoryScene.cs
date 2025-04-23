using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class InventoryScene : Scene
    {
        private InventoryController inventoryController;
        public InventoryScene(SceneController controller) : base(controller)
        {
            inventoryController = new InventoryController();
        }

        public override void Start()
        {
            Console.Clear();
            Console.WriteLine("인벤토리\n보유중인 아이템을 관리할 수 있습니다.\n");

            if (inventoryController.Items.Count == 0)
            {
                Console.WriteLine("보유한 아이템이 없습니다.");
            }
            else
            {
                Console.WriteLine("[아이템 목록]");
                inventoryController.PrintItems();
            }
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
        }

        public override void End()
        {
            Console.Clear();
            
        }

        public override void Update()
        {
            string input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    End();
                    break;

                case "1":
                    Console.Clear();
                    inventoryController.PrintItems();
                    Console.Write("\n장착할 아이템 번호를 입력하세요\n>> ");
                    string select = Console.ReadLine();
                    int index;

                    if (int.TryParse(select, out index))
                    {
                        index -= 1;
                        if (index >= 0 && index < inventoryController.Items.Count)
                        {
                            InventoryItemData item = inventoryController.Items[index];

                            if (item.ItemData.Type == GameEnum.ItemType.Consumable)
                            {
                                Console.WriteLine("소비 아이템은 장착할 수 없습니다.");
                            }
                            else
                            {
                                item.ToggleEquip();
                                Console.WriteLine(item.IsEquip
                                    ? $"'{item.ItemData.Name}' 장착 완료"
                                    : $"'{item.ItemData.Name}' 장착 해제됨");
                            }
                        }
                        else
                        {
                            Console.WriteLine("존재하지 않는 아이템 번호입니다");
                        }
                    }
                    else
                    {
                        Console.WriteLine("숫자를 입력해주세요");
                    }
                    Console.ReadLine();
                    Start();
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ReadLine();
                    Start();
                    break;
            }
        }
    }
}
