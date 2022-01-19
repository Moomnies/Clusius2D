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
        string path = Application.persistentDataPath + "//playerProfile.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameDataPlayer data = formatter.Deserialize(stream) as GameDataPlayer;
            stream.Close();

            return data;

        } else
        {
            Debug.LogError("Ididnot fine file for GameDataPlayer" + path);
            return null;
        }
    }
}
