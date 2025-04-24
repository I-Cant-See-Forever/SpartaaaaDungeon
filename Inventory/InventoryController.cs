using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class InventoryController
    {
        private List<InventoryItemData> items;
        private Dictionary<GameEnum.ItemType, List<InventoryItemData>> typeItemDict;

        private Dictionary<InventoryItemData, bool> itemEquipStates = new();

        public List<InventoryItemData> Items => items;
        public Dictionary<GameEnum.ItemType, List<InventoryItemData>> CategorizedItems => typeItemDict;

        public InventoryController()
        {
            items = GameManager.Instance.InventoryItems;

            typeItemDict = new Dictionary<GameEnum.ItemType, List<InventoryItemData>>();

            foreach (InventoryItemData item in items)
            {
                GameEnum.ItemType type = item.ItemData.Type;

                if (typeItemDict.ContainsKey(type))
                {
                    typeItemDict[type].Add(item);
                }
                else
                {
                    typeItemDict[type] = new List<InventoryItemData> { item };
                }
                itemEquipStates[item] = false;
            }
        }

        public void PrintItems(GameEnum.ItemType targetType)
        {
            foreach (KeyValuePair<GameEnum.ItemType, List<InventoryItemData>> pair in typeItemDict)
            {
                if(pair.Key == targetType)
                {
                    Console.WriteLine("[" + pair.Key + "]");
                    foreach (InventoryItemData item in pair.Value)
                    {
                        string prefix = IsEquipped(item) ? "[E] " : "   ";
                        string effect = item.ItemData.Type switch
                        {
                            GameEnum.ItemType.Weapon => "공격력 +" + item.ItemData.StatData.Attack,
                            GameEnum.ItemType.Armor => "방어력 +" + item.ItemData.StatData.Defense,
                            GameEnum.ItemType.Consumable => "회복력 +" + item.ItemData.StatData.Defense,
                            _ => ""
                        };
                        Console.WriteLine("- " + prefix + item.ItemData.Name + " | " + effect);
                    }
                    Console.WriteLine();
                }
            }
        }

        public void PrintItemNames()
        {
            for (int i = 0; i < items.Count; i++)
            {
                InventoryItemData item = items[i];
                string prefix = IsEquipped(item) ? "[E] " : "   ";
                Console.WriteLine((i + 1).ToString() + ". " + prefix + item.ItemData.Name);
            }
        }
        public bool ToggleEquipState(InventoryItemData item)
        {
            if (!itemEquipStates.ContainsKey(item)) return false;

            itemEquipStates[item] = !itemEquipStates[item];
            return itemEquipStates[item];
        }
        public bool IsEquipped(InventoryItemData item)
        {
            return itemEquipStates.TryGetValue(item, out var isEquip) && isEquip;
        }
    }
}
