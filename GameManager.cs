using SprtaaaaDungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameManager
{
    public static GameManager Instance { get; set; }
    
    public GameManager()
    {
        Instance = this;
    }


    public List<ItemData> GameItems{get;set;}
    public List<ShopItemData> ShopItems{get;set;}
    public List<InventoryItemData> InventoryItems{get;set;}
    public PlayerData PlayerData{get;set; }

    public void StartGame(bool isNewGame)
    {
        if (isNewGame) NewGame();

    }


    void NewGame()
    {
        PlayerData = 
            new("테스트이름", GameEnum.ClassType.Warrior, 1, 1500, new(100, 100, 10, 5));

        GameItems = new()
        {
            new("테스트무기",GameEnum.ItemType.Weapon , 100, new(1,1,1,1))
        };

        ShopItems = new();
        InventoryItems = new();
    }
}
