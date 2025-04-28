using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaaaaDungeon
{
    public static class GamePath
    {
        public const string SaveRoot = "SaveData";
        public const string PlayerDataPath = SaveRoot + "/player_data.json";
        public const string InventoryItemDataPath = SaveRoot + "/inventory_data.json";
        public const string ShopItemDataPath = SaveRoot + "/shop_data.json";
        public const string PlayerQuestDataPath = SaveRoot + "/player_quest_data.json";

        public const string GameRoot = "GameJsonData";
        public const string ItemDataPath = GameRoot + "/item_data.json";
        public const string QuestDataPath = GameRoot + "/quest_data.json";
        public const string MonsterDataPath = GameRoot + "/monster_data.json";
        public const string DungeonDataPath = GameRoot + "/dungeon_data.json";
        public const string SkillDataPath = GameRoot + "/skill_data.json";
    }
}
