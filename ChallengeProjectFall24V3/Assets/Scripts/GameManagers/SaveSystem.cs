using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static void SaveLevelData(LevelSaveData lsd)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/leveldata.txt";

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            bf.Serialize(fs, lsd);
        }
    }

    public static LevelSaveData LoadLevelData()
    {
        string path = Application.persistentDataPath + "/leveldata.txt";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                return bf.Deserialize(fs) as LevelSaveData;
            }
        }
        else
        {
            Debug.Log("Save file not found in: " + path);
            return null;
        }
    }
}
