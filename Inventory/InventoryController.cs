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
                if (item.ItemData == null)
                {
                    Console.WriteLine("[경고] ItemData가 null인 InventoryItemData가 감지됨. 무시합니다.");
                    continue;
                }

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
            int index = 1;
            foreach (KeyValuePair<GameEnum.ItemType, List<InventoryItemData>> pair in typeItemDict)
            {
                if(pair.Key == targetType)
                {
                    Console.WriteLine("[" + pair.Key + "]");
                    foreach (InventoryItemData item in pair.Value)
                    {
                        if (item.ItemData == null)
                        {
                            Console.WriteLine($"{index}. [잘못된 아이템 데이터]");
                            index++;
                            continue;
                        }

                        string prefix = IsEquipped(item) ? "[E] " : "   ";
                        string effect = item.ItemData.Type switch
                        {
                            GameEnum.ItemType.Weapon => "공격력 +" + item.ItemData.StatData.Attack,
                            GameEnum.ItemType.Armor => "방어력 +" + item.ItemData.StatData.Defense,
                            GameEnum.ItemType.Consumable => "회복력 +" + item.ItemData.StatData.Defense,
                            _ => ""
                        };
                        Console.WriteLine($"{index}. {prefix}{item.ItemData.Name} | {effect}");
                        index++;
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
            if (item.ItemData == null)
            {
                Console.WriteLine("[경고] 장착 시도한 아이템에 ItemData가 없습니다.");
                return false;
            }

            if (!itemEquipStates.ContainsKey(item)) return false;

            bool newState = !itemEquipStates[item];
            itemEquipStates[item] = newState;

            UpdatePlayerStats();

            return newState;
        }
        public bool IsEquipped(InventoryItemData item)
        {
            return itemEquipStates.TryGetValue(item, out var isEquip) && isEquip;
        }

        private void UpdatePlayerStats()
        {
            PlayerData player = GameManager.Instance.PlayerData;

            player.StatData.Attack = 0;
            player.StatData.Defense = 0;

            foreach (KeyValuePair<InventoryItemData, bool> entry in itemEquipStates)
            {
                if (!entry.Value) continue;

                InventoryItemData equippedItem = entry.Key;

                if (equippedItem.ItemData == null) continue;

                GameEnum.ItemType type = equippedItem.ItemData.Type;
                StatData stats = equippedItem.ItemData.StatData;

                if (type == GameEnum.ItemType.Weapon)
                {
                    player.StatData.Attack += stats.Attack;
                }
                else if (type == GameEnum.ItemType.Armor)
                {
                    player.StatData.Defense += stats.Defense;
                }
            }
        }
    }
}
