using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UnitController : MonoBehaviour
{
    [SerializeField]
    GameObject TargetPrefab,AttackTilePrefab;

    public GameObject openUnitOrderMenu;
    Dictionary<string, UnitModel> unitsToCreated = new Dictionary<string, UnitModel>();
    List<GameObject> TargetWalkTile = new List<GameObject>();
    List<GameObject> TargetAttackTile = new List<GameObject>();
    List<UnitModel> allUnitsInWorld = new List<UnitModel>();
    List<TileModel> tiles = new List<TileModel>();
    GetTileAlgorithme getTileAlgorithme;
    Action<UnitModel> DestroyUnitOutView;
    
    // Start is called before the first frame update
   public void InitUnits()
    {
        getTileAlgorithme = new GetTileAlgorithme();
        unitsToCreated.Add("InfantryRed", new UnitModel { UnitName = "InfantryRed", UnitMovemtCredits = 2, UnitHitPoints = 10, UnitPower = 3, UnitDefence = 1,unitColorType=UnitModel.UnitColor.RED,unitType=UnitModel.UnitType.LAND,UnitShotDistance=1 });
        unitsToCreated.Add("InfantryBlue", new UnitModel { UnitName = "InfantryBlue", UnitMovemtCredits = 2, UnitHitPoints = 10, UnitPower = 3, UnitDefence = 1, unitColorType = UnitModel.UnitColor.BLUE, unitType = UnitModel.UnitType.LAND,UnitShotDistance=1 });
        unitsToCreated.Add("TankRed", new UnitModel { UnitName = "TankRed", UnitMovemtCredits = 4, UnitHitPoints = 10, UnitPower = 5, UnitDefence = 3, unitColorType = UnitModel.UnitColor.RED, unitType = UnitModel.UnitType.LAND, UnitShotDistance = 1 });
        unitsToCreated.Add("TankBlue", new UnitModel { UnitName = "TankBlue", UnitMovemtCredits = 4, UnitHitPoints = 10, UnitPower = 5, UnitDefence = 3, unitColorType = UnitModel.UnitColor.BLUE, unitType = UnitModel.UnitType.LAND, UnitShotDistance = 1 });
        CreateTargets();
    }

    public void CreateTargets()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject targetobj = Instantiate(TargetPrefab) as GameObject;
            targetobj.SetActive(false);
            TargetWalkTile.Add(targetobj);
        }
        for (int i = 0; i < 100; i++)
        {
            GameObject attactObj = Instantiate(AttackTilePrefab) as GameObject;
            attactObj.SetActive(false);
            TargetAttackTile.Add(attactObj);
        }
    }


    public void UnitSlected(UnitModel unit)
    {
        UnitDeselect();
        if (unit==null)
        {
            return;
        }
        tiles.Clear();
        tiles = getTileAlgorithme.TilesToMove(unit,GetTileAlgorithme.UnitMove.MOVE);
        if(tiles!=null && !unit.UnitHasMoved)
        {
            foreach(TileModel t in tiles)
            {
                for (int i = 0; i < TargetWalkTile.Count; i++)
                {
                    if(TargetWalkTile[i].activeInHierarchy==false)
                    {
                        TargetWalkTile[i].transform.position = new Vector3(t.X, t.Y, -1);
                        TargetWalkTile[i].SetActive(true);
                        break;
                    }
                }
            }
        }
    }

    public void GetAttackPoints(UnitModel unit)
    {
        DeslectTargets();
        if (unit == null)
        {
            return;
        }
        tiles.Clear();
        tiles = getTileAlgorithme.TilesToMove(unit, GetTileAlgorithme.UnitMove.ATTACK);
        if (tiles != null)
        {
            foreach (TileModel t in tiles)
            {
                for (int i = 0; i < TargetAttackTile.Count; i++)
                {
                    if (TargetAttackTile[i].activeInHierarchy == false)
                    {
                        TargetAttackTile[i].transform.position = new Vector3(t.X, t.Y, -1);
                        TargetAttackTile[i].SetActive(true);
                        break;
                    }
                }
            }
        }
    }

    public void CheckUnitsOnWater()
    {
        for (int i = 0; i < allUnitsInWorld.Count; i++)
        {
            switch(allUnitsInWorld[i].unitType)
            {
                case UnitModel.UnitType.LAND:
                    if (allUnitsInWorld[i].CurrentTile.TileType=="Water")
                    {
                        DestroyUnit(allUnitsInWorld[i]);
                        i = -1;
                    }
                break;
            }
        }
    }
    
    public void OrderStrike(TileModel tile)
    {
        if(tiles.Contains(tile))
        {
            UnitModel unit = tile.UnitOnTile;
            //for now destroyUnit
            DestroyUnit(unit);
            tiles.Clear();
        }
    }

    public void MoveUnit(UnitModel model, TileModel tile)
    {
        if (tiles.Contains(tile) && !model.UnitHasMoved)
        {
            TileModel oldTile = WorldManger.Instance.World.getTile(model.CurrentTile.X, model.CurrentTile.Y);
            oldTile.UnitOnTile = null;
            tile.UnitOnTile = model;
            model.CurrentTile = tile;
            MoveUnitOnMap(model);
            UnitDeselect();
            tiles.Clear();
            model.UnitHasMoved = true;
            model.CbUnitUpdateGraphics(model);
            openUnitOrderMenu.SetActive(true);
        }
    }

    void DestroyUnit(UnitModel unitModel)
    {
        DestroyUnitOutView(unitModel);
        TileModel tile = WorldManger.Instance.World.getTile(unitModel.CurrentTile.X, unitModel.CurrentTile.Y);
        tile.UnitOnTile = null;
        allUnitsInWorld.Remove(unitModel);
    }
    public void UnitDeselect()
    {
        for (int i = 0; i < TargetWalkTile.Count; i++)
        {
            TargetWalkTile[i].SetActive(false);
        }

    }
    public void DeslectTargets()
    {
        for (int i = 0; i < TargetAttackTile.Count; i++)
        {
            TargetAttackTile[i].SetActive(false);
        }
    }
   



    void MoveUnitOnMap(UnitModel model)
    {
        model.PosX = model.CurrentTile.X;
        model.PosY = model.CurrentTile.Y;
    }

    public void AddUnitToList(UnitModel model)
    {
        allUnitsInWorld.Add(model);
    }

    public void DeleteUnitFromWorld(UnitModel model)
    {
        allUnitsInWorld.Remove(model);
    }
    public UnitModel GetUnitFromCreateList(string name )
    {
        if(unitsToCreated.ContainsKey(name))
        {
            return unitsToCreated[name];
        }
        Debug.LogError("Missing unit Model");
        return null;

    }
    public List<UnitModel>GetAllUnits()
    {
        return allUnitsInWorld;
    }
    public void RegisterUnitView(Action<UnitModel>UnitView)
    {
        DestroyUnitOutView+=UnitView;
    }
}
