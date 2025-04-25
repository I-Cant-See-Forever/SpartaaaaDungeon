using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public class InventoryController
    {
        PlayerData playerData;
        private Dictionary<GameEnum.ItemType, List<InventoryItemData>> categoryItemDict;

        //이미 inveontroyItemData에 장착정보가 있어요
        //private Dictionary<InventoryItemData, bool> itemEquipStates = new();

        //여기서 쓰려고 캐싱하는거니 여기서만!
        //public List<InventoryItemData> Items => items;
        public Dictionary<GameEnum.ItemType, List<InventoryItemData>> CategoryItemDict => categoryItemDict;

        /// ///////////////////////////////////////민혁 <summary>
        /// ///////////////////////////////////////민혁
        /// 


        private List<InventoryItemData> inveontroyItemList;

        public List<ItemData> EquipItems = new();

        public InventoryController()
        {
            inveontroyItemList = GameManager.Instance.InventoryItems;
            playerData = GameManager.Instance.PlayerData;


            /*categoryItemDict = new Dictionary<GameEnum.ItemType, List<InventoryItemData>>();

            foreach (InventoryItemData item in inveontroyItemList)
            {
                if (item.ItemData == null)
                {
                    Console.WriteLine("[경고] ItemData가 null인 InventoryItemData가 감지됨. 무시합니다.");
                    continue;
                }

                GameEnum.ItemType type = item.ItemData.Type;

                if (categoryItemDict.ContainsKey(type))
                {
                    categoryItemDict[type].Add(item);
                }
                else
                {
                    categoryItemDict[type] = new List<InventoryItemData> { item };
                }

                if(item.IsEquip)// 불러온 아이템이 장착중이라면
                {
                    EquipItems.Add(item.ItemData);
                }
            }*/
        }

        public List<InventoryItemData> SetCurrentItemList(GameEnum.ItemType itemType)
        {
            var currentItemList = new List<InventoryItemData>();

            foreach (var item in inveontroyItemList)
            {
                if(item.Data.Type == itemType)
                {
                    currentItemList.Add(item);
                }
            }

            return currentItemList;
        }

        //이렇게 한곳에서 실행해야 씬에서 간단히 쓸수 있어요.
        //어떤 아이템인지는 씬에서 입력번호로 검사
        public void SelectItem(List<InventoryItemData> targetList, int itemIndex) 
        {
            var targetInveontroyItem = targetList[itemIndex];

            switch (targetInveontroyItem.Data.Type)
            {
                case GameEnum.ItemType.Weapon:
                case GameEnum.ItemType.Armor:

                    if(EquipItems.Contains(targetInveontroyItem.Data)) 
                    {
                        Unequip(targetInveontroyItem);
                    }
                    else
                    {
                        Equip(targetInveontroyItem);
                    }
                    break;

                case GameEnum.ItemType.Consumable:
                    Consume(targetInveontroyItem);
                    break;
            }
        }

        void Equip(InventoryItemData item)
        {
            playerData.StatData.AddStat(item.Data.StatData);

            EquipItems.Add(item.Data);

            for (int i = 0; i < inveontroyItemList.Count; i++)
            {
                if (inveontroyItemList[i].Data.Type == item.Data.Type)
                {
                    if (inveontroyItemList[i].IsEquip)
                    {
                        Unequip(inveontroyItemList[i]);
                        break;
                    }
                }
            }

            for (int i = 0; i < inveontroyItemList.Count; i++)
            {
                if (item == inveontroyItemList[i])
                {
                    inveontroyItemList[i].IsEquip = true;
                    break;
                }
            }
        }

        void Unequip(InventoryItemData item)
        {
            playerData.StatData.RemoveStat(item.Data.StatData);

            EquipItems.Remove(item.Data);


            for (int i = 0; i < inveontroyItemList.Count; i++)
            {
                if (item == inveontroyItemList[i])
                {
                    inveontroyItemList[i].IsEquip = false;
                    break;
                }
            }
        }

        void Consume(InventoryItemData item)
        {
            playerData.StatData.AddStat(item.Data.StatData);

            for (int i = 0; i < inveontroyItemList.Count; i++)
            {
                if (item == inveontroyItemList[i])
                {
                    inveontroyItemList[i].Count -= 1;

                    if (inveontroyItemList[i].Count <= 0)
                    {
                        inveontroyItemList.Remove(inveontroyItemList[i]);

                        break;
                    }
                }
            }
        }




        public bool ToggleEquipState(InventoryItemData item)
        {
            /*if (item.ItemData == null)
            {
                Console.WriteLine("[경고] 장착 시도한 아이템에 ItemData가 없습니다.");
                return false;
            }

            if (!itemEquipStates.ContainsKey(item)) return false;

            bool newState = !itemEquipStates[item];
            itemEquipStates[item] = newState;

            UpdatePlayerStats();

            return newState;*/

            return false;
        }

        public bool IsEquipped(InventoryItemData item) 
        {
            //return itemEquipStates.TryGetValue(item, out var isEquip) && isEquip;

            return false;
        }

        private void UpdatePlayerStats()
        {
           /* PlayerData player = GameManager.Instance.PlayerData;

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
            }*/
        }




        public void PrintItems(GameEnum.ItemType targetType) //Scene 으로 이전
        {
           /* int index = 1;
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
            }*/
        }
        public void PrintItemNames() //Scene 으로 이전
        {
           /* for (int i = 0; i < items.Count; i++)
            {
                InventoryItemData item = items[i];
                string prefix = IsEquipped(item) ? "[E] " : "   ";
                Console.WriteLine((i + 1).ToString() + ". " + prefix + item.ItemData.Name);
            }*/
        }
    }
}
