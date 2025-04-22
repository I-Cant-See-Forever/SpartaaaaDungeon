using SprtaaaaDungeon;
using System;

public class ShopScene
{
	private ItemData ItemData;
	private Shop shop;



    public ShopScene()
	{
		this.shop = new Shop();
	}


	public void posShopText()
	{
        Console.Clear();
        Console.WriteLine("상점\n필요한 아이템을 얻을 수 있는 상점입니다.\n");
        Console.WriteLine($"[보유 골드]\n 1500G\n");

		Console.WriteLine(shop.ItemData);


    }
}
