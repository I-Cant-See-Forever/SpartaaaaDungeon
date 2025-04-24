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
    public List<QuestData> GameQuestDatas { get; private set; }

    //save
    public List<PlayerQuestData> PlayerQuestDatas { get; private set; } = new();
    public List<ShopItemData> ShopItems {get; private set;}
    public List<InventoryItemData> InventoryItems{get; private set; }
    public PlayerData PlayerData{ get; private set; }

    public void StartGame()
    {
        InitGameData();

        if (SaveManager.Instance.HasSaveFile(GamePath.SaveRoot))
        {
            LoadGame();
        }
        else
        {
            NewGame();
        }

        InitComponents();
    }

    public ItemData GetItemData(string findName) => GameItems.FirstOrDefault(item => item.Name == findName);
    public QuestData GetQuestData(string findName) => GameQuestDatas.FirstOrDefault(item => item.Title == findName);


    void NewGame()
    {
        PlayerData =
            new("테스트이름", GameEnum.ClassType.Warrior, 1, 1500, new(100, 100, 10, 5));

        InventoryItems = new(); // 병합 시 아이템들만 삭제(괄호도 지움)

        GameItems = new() // 병합 시 삭제
        {
            new("테스트무기0", GameEnum.ItemType.Weapon, 100, new(1, 1, 1, 1)),
            new("테스트소모품0", GameEnum.ItemType.Consumable, 100, new(1, 1, 1, 1))

        };

        ShopItems = new() // 병합 시 아이템 테스트무기0 빼고 다 삭제
        {
            new(GetItemData("테스트무기0"), 1),
            new(GetItemData("테스트소모품0"), 5)
        };
    }

    public void SaveGame()
    {
        var saveManager = SaveManager.Instance;

        saveManager.SaveGameData(PlayerData, GamePath.PlayerDataPath);
        saveManager.SaveGameData(InventoryItems, GamePath.InventoryItemDataPath);
        saveManager.SaveGameData(ShopItems, GamePath.ShopItemDataPath);
        saveManager.SaveGameData(PlayerQuestDatas, GamePath.PlayerQuestDataPath);
    }


    public void LoadGame()
    {
        var saveManager = SaveManager.Instance;

        PlayerData = saveManager.LoadGameData<PlayerData>(GamePath.PlayerDataPath);
        InventoryItems = saveManager.LoadGameData<List<InventoryItemData>>(GamePath.InventoryItemDataPath);
        ShopItems = saveManager.LoadGameData<List<ShopItemData>>(GamePath.ShopItemDataPath);
        PlayerQuestDatas = saveManager.LoadGameData<List<PlayerQuestData>>(GamePath.PlayerQuestDataPath);
    
    }



    void InitComponents()
    {

        DungeonController = new DungeonController (PlayerData);


        QuestController = new();

        SceneController = new();
    }

    void InitGameData()
    {
        var saveManager = SaveManager.Instance;

        GameItems = saveManager.LoadGameData<List<ItemData>>(GamePath.ItemDataPath);
        GameQuestDatas = saveManager.LoadGameData<List<QuestData>>(GamePath.QuestDataPath);
    }
}
