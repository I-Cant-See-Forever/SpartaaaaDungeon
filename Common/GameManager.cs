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


    //Component
    public QuestController QuestController { get; private set; }
    public SceneController SceneController { get; private set; }
    public DungeonController DungeonController { get; set; }


    //data
    public List<ItemData> GameItems {get; private set;}
    public List<ShopItemData> ShopItems {get; private set;}
    public List<InventoryItemData> InventoryItems{get; private set; }
    public PlayerData PlayerData{ get; private set; }
    public List<QuestData> QuestDatas { get; private set; }

    public void StartGame(bool isNewGame)
    {
        if (isNewGame) NewGame();


        InitComponents();
    }

    public ItemData GetItemData(string findName) => GameItems.FirstOrDefault(item => item.Name == findName);


    void NewGame()
    {
        PlayerData = 
            new("테스트이름", GameEnum.ClassType.Warrior, 1, 1500, new(100, 100, 10, 5));


        GameItems = new()
        {
            new("테스트무기0",GameEnum.ItemType.Weapon , 100, new(1,1,1,1))//,
            //new("테스트무기1",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            //new("테스트방어구0",GameEnum.ItemType.Armor , 100, new(1,1,1,1)),
            //new("테스트방어구1",GameEnum.ItemType.Armor , 100, new(1,1,1,1))
        };

        InventoryItems = new();

        ShopItems = new()
        {
            new(GetItemData("테스트무기0"), 1)//,
            //new(GetItemData("테스트무기1"), 1),
            //new(GetItemData("테스트방어구0"), 1),
            //new(GetItemData("테스트방어구1"), 1)
        };

        QuestDatas = new()
        {
            new("던전 청소!", 
                "요즘 마을이 너무 위험해.. \n" +
                "몬스터들좀 잡아주이..",
                new(
                    new()
                    {
                        { GetItemData("테스트무기0"), 1}
                    }, 
                    100, 
                    100),
                new HuntQuestCondition(
                    "123",
                    "아무 몬스터 잡기",
                    3))
        };
    }

    void InitComponents()
    {
        QuestController = new(QuestDatas);
        SceneController = new();
    }
}
