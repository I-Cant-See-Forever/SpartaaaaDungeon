using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon.StudyTest.Shop
{
    public class StudyShopScene : Scene
    {
        int totalWidth = 230; //공백

        bool posShop;

        bool posShopMain;
        bool posShopPurchase;
        bool posShopSell;

        bool purItemOfTypeLineUp;
        bool purCatBuy;

        List<InventoryItemData> inventoryItemData;


        public StudyShopScene(SceneController controller) : base(controller)
        {
        }

        public override void Start()
        {
            //shop.ShopStart();
            //DrawCategoryItems();
        }

        public override void Update()
        {
            /* shop.InventoryItemByType();
        shop.EnterShop();*/


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

        public override void End()
        {
        }

        /* int TestCheckInput(int min, int max)
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
 }*/
        /*  void DrawCategoryItems()
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
          }*/
    }

  
}
