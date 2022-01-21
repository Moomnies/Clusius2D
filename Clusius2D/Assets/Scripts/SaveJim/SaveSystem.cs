using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(PlayerProfile playerProfile)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerProfile.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameDataPlayer data = new GameDataPlayer(playerProfile);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameDataPlayer LoadPlayer()
    {
        string path = Application.persistentDataPath + "/playerProfile.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameDataPlayer data = formatter.Deserialize(stream) as GameDataPlayer;
            stream.Close();

            return data;

        }
        else
        {
            Debug.LogError("I did not fine file for GameDataPlayer" + path);
            return null;
        }
    }

    public static void SaveTutorial(Scr_tutorial scr_Tutorial)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/tutorial.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameDataTutorial data = new GameDataTutorial(scr_Tutorial);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameDataTutorial LoadTutorial()
    {
        string path = Application.persistentDataPath + "/tutorial.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameDataTutorial data = formatter.Deserialize(stream) as GameDataTutorial;
            stream.Close();
            return data;

        }
        else
        {
            Debug.LogError("I did not fine file for GameDataTutorial" + path);
            return null;
        }
    }

    public static void SaveInventory()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/inventory.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameDataInventory data = new GameDataInventory();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameDataTutorial LoadInventory()
    {
        string path = Application.persistentDataPath + "/inventory.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameDataInventory data = formatter.Deserialize(stream) as GameDataInventory;
            stream.Close();
            return null;

        }
        else
        {
            Debug.LogError("I did not fine file for GameDataInventory" + path);
            return null;
        }
    }

    public static void DeleteFiles()
    {
        File.Delete(Application.persistentDataPath + "/tutorial.fun");
        File.Delete(Application.persistentDataPath + "/playerProfile.fun");
        File.Delete(Application.persistentDataPath + "/inventory.fun");
    }
}
