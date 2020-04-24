using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveGameController : MonoBehaviour
{
    // Start is called before the first frame update
public static bool Save(string savename,object saveData)
    {
        BinaryFormatter formatter = GetBinaryFromatter();
        if (!Directory.Exists(Application.dataPath + "/saves/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/saves/");
        }
        string PathBuilder = Application.dataPath + "/saves/";
        
        string path = PathBuilder+savename+".warmap";
        FileStream file = File.Create(path);
        formatter.Serialize(file, saveData);
        file.Close();
        
        return true;
    
    
    }

    public static object Load(string path)
    {
        if(!File.Exists(path))
        {
            return null;
        }
        BinaryFormatter formatter = GetBinaryFromatter();
        FileStream file = File.Open(path, FileMode.Open);
        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat("Failed to load file at {0},", path);
            file.Close();
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFromatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        return formatter;
    }
}
