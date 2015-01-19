using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class Data 
{
    public World Maths;
    public World Physics;
    public World Reflex;
    public World Collect;

    public static bool Updated = false;

    public static Data Load()
    {
        if (!File.Exists(Path.Combine(Application.persistentDataPath, "data.dat")))
        {
            Debug.Log("Writing LevelData");
            WriteData();
        }
        BinaryFormatter bf = new BinaryFormatter();
        using (var file = new FileStream(Path.Combine(Application.persistentDataPath, "data.dat"), FileMode.Open))
        {
            return (Data)bf.Deserialize(file);
        }
    }

    public static void Save( Data _Data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (var file = new FileStream(Path.Combine(Application.persistentDataPath, "data.dat"), FileMode.OpenOrCreate))
        {
            bf.Serialize(file, _Data);
            Updated = true;
        }
    }
    private static void WriteData( )
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (var file = new FileStream(Path.Combine(Application.persistentDataPath, "data.dat"), FileMode.Create))
        {
            Data t = new Data();
            bf.Serialize(file, t);
        }
    }

    /// <summary>
    /// Will be removed
    /// </summary>
    /// <param name="AppData"></param>
    /// <returns></returns>
    public static Data Load(Data AppData)
    {
        BinaryFormatter bf = new BinaryFormatter();

        Save(AppData);
        return Load();
    }

}
