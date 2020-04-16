using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameMapController 
{

    TileModel[,] tiles;
    Action<TileModel> callbackTileChange;
    Action<BuildingModel> callbackBuildingCreated;
    Action<UnitModel> callbackCharacter;
    RiseWaterLogic riseWaterLogic;
    BuildBuilding buildingLogic;
    int worldWidth = 0, worldHight = 0;
    public int WorldWidth { get { return worldWidth; } set { worldWidth = value; } }
    public int WorldHight { get { return worldHight; } set { worldHight = value; } }

    public RiseWaterLogic RiseWaterLogic { get => riseWaterLogic; set => riseWaterLogic = value; }

    public GameMapController(int width, int height)
    {
        buildingLogic = new BuildBuilding();
        RiseWaterLogic = new RiseWaterLogic();
        tiles = new TileModel[width, height];
        WorldWidth = width;
        WorldHight = height;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new TileModel { X = x, Y = y, blocked = false, TileType = "Grass",TileMovementCost=1 };
            }
        }
    }



    public TileModel getTile(int x, int y)
    {
        if (x >= WorldWidth || x < 0 || y >= WorldHight || y < 0)
        {
            return null;
        }
        return tiles[x, y];
    }

    public void UpdateTile(TileModel tile)
    {
        if (tile == null)
        {
            return;
        }
        callbackTileChange(tile);
    }


    public void PlaceBuildingObj(TileModel tile, string buildObj)
    {
        BuildingModel buildBuildingObj;
        //Debug.Log(buildObj);

        if (InitBuildObjects.Instance.BuildingObject.ContainsKey(buildObj) == false)
        {
            Debug.LogError("There is no object with such name as " + buildObj + " Check if you spell the building name correct");
            return;
        }


        buildBuildingObj = buildingLogic.PlaceBuilding(InitBuildObjects.Instance.BuildingTypes, tile, buildObj, InitBuildObjects.Instance.BuildingObject);


        if (buildBuildingObj == null)
        {
            //replace this with a sound
            // Debug.Log("There is already A Building");
            return;
        }

        if (callbackBuildingCreated != null)
        {
            callbackBuildingCreated(buildBuildingObj);
        }
        //buildBuildingObj.cbBuildingDestroy += BuildMouseCon.instance.DestroyObject;

        WorldManger.Instance.buildingController.AddBuilding(buildBuildingObj);

    }

    public void SetTiles()
    {
        for (int x = 0; x < WorldWidth; x++)
        {
            for (int y = 0; y < WorldHight; y++)
            {
                tiles[x, y].cbTileChanged += UpdateTile;
                tiles[x, y].TileType = "Grass";
                //if (WorldManger.loadWorldMap != null)
                //{
                //    tiles[x, y].TileType = WorldManger.loadWorldMap.savetile[x, y].tileType;
                //}
                //else
                //{
             
                //}

            }
        }

    }

    public void CreateCharacter(TileModel tile, UnitModel unit)
    {
        if (tile != null && unit != null)
        {
        
            UnitModel unitmodel = new UnitModel {UnitName=unit.UnitName,UnitDefence=unit.UnitDefence,UnitHitPoints=unit.UnitHitPoints,UnitPower=unit.UnitPower,unitColorType=unit.unitColorType,UnitMovemtCredits=unit.UnitMovemtCredits,PosX=tile.X,PosY=tile.Y,CurrentTile=tile,UnitShotDistance=unit.UnitShotDistance,unitType=unit.unitType };
            tile.UnitOnTile = unitmodel;
            if (callbackCharacter != null)
            {
                callbackCharacter(unitmodel);
            }
            WorldManger.Instance.unitController.AddUnitToList(unitmodel);
            WorldManger.Instance.turnController.TeamUpdate(WorldManger.Instance.turnController.GetTeam());
        }
    }

    //[Callback for graphics].
    public void RegisterTileChange(Action<TileModel> tilechangerview)
    {
        callbackTileChange += tilechangerview;
    }

    public void RegisterCreateBuilding(Action<BuildingModel> building)
    {
        callbackBuildingCreated += building;
    }

    public void RegisterCharacterToWorld(Action<UnitModel> character)
    {
        callbackCharacter += character;
    }

    public void UnRegisterCharacterToWorld(Action<UnitModel> character)
    {
        callbackCharacter -= character;
    }

}
