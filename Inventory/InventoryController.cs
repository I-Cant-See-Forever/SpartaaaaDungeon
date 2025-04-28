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

        List<InventoryItemData> inveontroyItemList;

        public List<ItemData> EquipItems = new();

        public InventoryController()
        {
            inveontroyItemList = GameManager.Instance.InventoryItems;
            playerData = GameManager.Instance.PlayerData;

            for (int i = 0; i < inveontroyItemList.Count; i++)
            {
                if (inveontroyItemList[i].IsEquip)
                {
                    EquipItems.Add(inveontroyItemList[i].Data);
                }
            }
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

                    if(EquipItems.Contains(targetInveontroyItem.Data)) //장착여부 판단
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
    }
}
