using System.Text.Json;
using Newtonsoft.Json;
using System;
using System.IO;


public class SaveManager
{
    JsonSerializerSettings settings;

    static SaveManager _instance;
    public static SaveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SaveManager();
            }
            return _instance;
        }
    }

    public SaveManager() 
    {
        settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        };

    }

    public bool HasSaveFile(string path)
    {
        return Directory.Exists("SaveData");
    }

    public void DeleteSaveFile(string path)
    {
        if (Directory.Exists(path))
        {
            Directory.Delete(path);
        }
    }

    public void SaveGameData<T>(T data, string path)
    {
        if (!Directory.Exists("SaveData"))
        {
            Directory.CreateDirectory("SaveData");
        }

        string json = JsonConvert.SerializeObject(data, Formatting.Indented, settings);
        File.WriteAllText(path, json);
    }

    public T LoadGameData<T>(string path)
    {

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            return JsonConvert.DeserializeObject<T>(json, settings);
        }
        else
        {
            return default;
        }
    }

    
}