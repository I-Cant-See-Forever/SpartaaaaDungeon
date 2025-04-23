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

        GameItems = new() // List<ItemData>
        {
            new("테스트무기1",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기2",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기3",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기4",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기5",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기6",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기7",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기8",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기9",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기10",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기11",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기12",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기13",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기14",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기15",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기16",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기17",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기18",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기19",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기20",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기21",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기22",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트무기23",GameEnum.ItemType.Weapon , 100, new(1,1,1,1)),
            new("테스트방어구0",GameEnum.ItemType.Armor , 100, new(1,1,1,1)),
            new("테스트방어구1",GameEnum.ItemType.Armor , 100, new(1,1,1,1)),
            new("테스트소모품0",GameEnum.ItemType.Consumable , 100, new(1,1,1,1)),
            new("테스트소모품1",GameEnum.ItemType.Consumable , 100, new(1,1,1,1)),
        };

        InventoryItems = new();

        ShopItems = new()
        {
            new(GetItemData("테스트무기1"), 1),
            new(GetItemData("테스트무기2"), 1),
            new(GetItemData("테스트무기3"), 1),
            new(GetItemData("테스트무기4"), 1),
            new(GetItemData("테스트무기5"), 1),
            new(GetItemData("테스트무기6"), 1),
            new(GetItemData("테스트무기7"), 1),
            new(GetItemData("테스트무기8"), 1),
            new(GetItemData("테스트무기9"), 1),
            new(GetItemData("테스트무기10"), 1),
            new(GetItemData("테스트무기11"), 1),
            new(GetItemData("테스트무기12"), 1),
            new(GetItemData("테스트무기13"), 1),
            new(GetItemData("테스트무기14"), 1),
            new(GetItemData("테스트무기15"), 1),
            new(GetItemData("테스트무기16"), 1),
            new(GetItemData("테스트무기17"), 1),
            new(GetItemData("테스트무기18"), 1),
            new(GetItemData("테스트무기19"), 1),
            new(GetItemData("테스트무기20"), 1),
            new(GetItemData("테스트무기21"), 1),
            new(GetItemData("테스트무기22"), 1),
            new(GetItemData("테스트무기23"), 1),
            new(GetItemData("테스트방어구0"), 1),
            new(GetItemData("테스트방어구1"), 1),
            new(GetItemData("테스트소모품0"), 1),
            new(GetItemData("테스트소모품1"), 1)

        };

        QuestDatas = new()
        {
            new("던전 청소!", 
                "요즘 마을이 너무 위험해.. \n" +
                "몬스터들좀 잡아주이..",
                new(
                    new()
                    {
                        { GetItemData("테스트무기1"), 1}
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
