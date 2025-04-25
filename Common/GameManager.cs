using SprtaaaaDungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

public class GameManager
{
    static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }
 


    //Component
    public QuestController QuestController { get; private set; }
    public SceneController SceneController { get; private set; }
    public DungeonController DungeonController { get; private set; }
    public InventoryController InventoryController { get; private set; }


    //data
    public List<ItemData> GameItems {get; private set;}
    public List<QuestData> GameQuestDatas { get; private set; }
    public List<SkillData> SkillDatas { get; private set; }
    public List<DungeonData> DungeonDatas { get; set; }
    public List<MonsterData> MonsterDatas { get; set; }

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



    public void GameOver()
    {
        SaveManager.Instance.DeleteSaveFile(GamePath.SaveRoot);

        SceneController.ChangeScene<GameEndScene>();
    }


    void NewGame()
    {
        PlayerData = new PlayerData
        {
            Level = 1,
            Gold = 1500,
            MaxExp = 10,
            StatData = new StatData
            {
                MaxHealth = 100,
                CurrentHealth = 100,
                MaxMP = 50,
                CurrentMP = 15,
                Attack = 10,
                Defense = 5
            }
        };

        InventoryItems = new()
        {
        };

        ShopItems = new();
    }

    void InitGameData()
    {
        var saveManager = SaveManager.Instance;

        GameItems = saveManager.LoadGameData<List<ItemData>>(GamePath.ItemDataPath);
        GameQuestDatas = saveManager.LoadGameData<List<QuestData>>(GamePath.QuestDataPath);
        DungeonDatas = saveManager.LoadGameData<List<DungeonData>>(GamePath.DungeonDataPath);
        MonsterDatas = saveManager.LoadGameData<List<MonsterData>>(GamePath.MonsterDataPath);
        SkillDatas = saveManager.LoadGameData<List<SkillData>>(GamePath.SkillDataPath);
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
        DungeonController = new();

        QuestController = new();

        SceneController = new();

        InventoryController = new();
    }

  

}
