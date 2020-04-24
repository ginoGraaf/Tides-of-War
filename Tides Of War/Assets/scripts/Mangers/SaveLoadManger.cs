using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
class SaveLoadManger : MonoBehaviour
{
    [SerializeField]
    InputField saveName;
    [SerializeField]
    GameObject canvasObj;
    public static bool loadGame = false;
    public static string saveGameFileName = "";
    public void SaveGame()
    {
        ResetSaveFile();
        SetSaveTiles();
        SetSaveUnits();
        string saveNameData = saveName.text;
        SaveGameController.Save(saveNameData, SaveData.current);
    }

    void ResetSaveFile()
    {
        SaveData.current.TileSaveData = new List<TileSaveData>();
        SaveData.current.unitSaves = new List<UnitSave>();
        
    }

    public void SetSaveTiles()
    {
        for (int x = 0; x < WorldManger.Instance.World.WorldWidth; x++)
        {
            for (int y = 0; y < WorldManger.Instance.World.WorldHight; y++)
            {
                TileModel t = WorldManger.Instance.World.getTile(x, y);
                TileSaveData tileSaveData = new TileSaveData();
                tileSaveData.tileType = t.TileType;
                tileSaveData.x = t.X;
                tileSaveData.y = t.Y;
                SaveData.current.TileSaveData.Add(tileSaveData);
            }
        }
    }

    public void SetSaveUnits()
    {
        for (int i = 0; i < WorldManger.Instance.unitController.GetAllUnits().Count; i++)
        {
            UnitModel unit = WorldManger.Instance.unitController.GetAllUnits()[i];
            UnitSave unitSave = new UnitSave();
            unitSave.UnitName = unit.UnitName;
            unitSave.x = unit.PosX;
            unitSave.y = unit.PosY;
            SaveData.current.unitSaves.Add(unitSave);
        }
    }

    public void LoadGame()
    {
        loadGame = true;
        saveGameFileName = saveName.text;
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoadOnNewGameScreen()
    {
        SaveData loadFile = (SaveData)SaveGameController.Load(Application.dataPath + "/saves/" + saveGameFileName + ".warmap");
        if (loadFile != null)
        {
            SetLoadTiles(loadFile);
            SetLoadUnits(loadFile);
        }
        else
        {
            Debug.Log("ERROR THE GAME MAP COULDN'T BE LOADED CHECK IF THE IF FILE IS NOT CORRUPPTED OR NULL");
            LoadOnNewGameFromMain();
        }
        loadGame = false;
    }

    public void LoadOnNewGameFromMain()
    {
        SaveData loadFile = (SaveData)SaveGameController.Load(SaveLoadManger.saveGameFileName);
        if (loadFile != null)
        {
            SetLoadTiles(loadFile);
            SetLoadUnits(loadFile);
            canvasObj.SetActive(false);
        }
        else
        {
            Debug.Log("ERROR THE GAME MAP COULDN'T BE LOADED CHECK IF THE IF FILE IS NOT CORRUPPTED OR NULL");
        }
        loadGame = false;
    }

    public void SetLoadTiles(SaveData loadFile)
    {
        for (int i = 0; i < loadFile.TileSaveData.Count; i++)
        {
            TileSaveData tileload = loadFile.TileSaveData[i];
            TileModel t = WorldManger.Instance.World.getTile((int)tileload.x, (int)tileload.y);
            t.TileType = tileload.tileType;
        }
    }

    public void SetLoadUnits(SaveData loadFile)
    {
        for (int i = 0; i < loadFile.unitSaves.Count; i++)
        {

            TileModel t = WorldManger.Instance.World.getTile((int)loadFile.unitSaves[i].x, (int)loadFile.unitSaves[i].y);

            WorldManger.Instance.World.CreateCharacter(t, WorldManger.Instance.unitController.GetUnitFromCreateList(loadFile.unitSaves[i].UnitName));

        }
    }
}

