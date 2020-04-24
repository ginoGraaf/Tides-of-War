using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveLoaderController : MonoBehaviour
{

    string[] savefiles;
    public GameObject loadButtonPrefab;
    public Transform loadAre;
    static string loadFromMenu="";
    // Start is called before the first frame update
    void GetLoadFiles()
    {
        if(!Directory.Exists(Application.dataPath + "/saves/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/saves/");
        }
        savefiles = Directory.GetFiles(Application.dataPath + "/saves/");

    }

    public void ShowsOnScreen()
    {
        GetLoadFiles();
        for (int i = 0; i < savefiles.Length; i++)
        {
            GameObject loadButton = Instantiate(loadButtonPrefab) as GameObject;
            loadButton.transform.SetParent(loadAre);
            var index = i;
            loadButton.GetComponent<Button>().onClick.AddListener(() => 
            { SaveLoadManger.saveGameFileName = savefiles[index]; Application.LoadLevel(1); SaveLoadManger.loadGame = true; 
            }); 
            loadButton.GetComponentInChildren<Text>().text = savefiles[index].Replace(Application.dataPath + "/saves/", "");
        }
        
    }

}
