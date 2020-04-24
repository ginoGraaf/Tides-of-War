using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class UnitController : MonoBehaviour
{
    [SerializeField]
    GameObject TargetPrefab,AttackTilePrefab;
    [SerializeField]
    TurnController turnController;
    [SerializeField]
    UnitOrderMenu unitorderMenu;

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
        unitsToCreated.Add("InfantryRed", new UnitModel { UnitName = "InfantryRed", UnitMovemtCredits = 2, UnitHitPoints = 10, UnitPower = 3, UnitDefence = 1,unitColorType=UnitModel.UnitColor.RED,unitType=UnitModel.UnitType.LAND,UnitShotDistance=0 });
        unitsToCreated.Add("InfantryBlue", new UnitModel { UnitName = "InfantryBlue", UnitMovemtCredits = 2, UnitHitPoints = 10, UnitPower = 3, UnitDefence = 1, unitColorType = UnitModel.UnitColor.BLUE, unitType = UnitModel.UnitType.LAND,UnitShotDistance=0 });
        unitsToCreated.Add("TankRed", new UnitModel { UnitName = "TankRed", UnitMovemtCredits = 4, UnitHitPoints = 10, UnitPower = 6, UnitDefence = 4, unitColorType = UnitModel.UnitColor.RED, unitType = UnitModel.UnitType.TANK, UnitShotDistance = 0 });
        unitsToCreated.Add("TankBlue", new UnitModel { UnitName = "TankBlue", UnitMovemtCredits = 4, UnitHitPoints = 10, UnitPower = 6, UnitDefence = 4, unitColorType = UnitModel.UnitColor.BLUE, unitType = UnitModel.UnitType.TANK, UnitShotDistance = 0 });
        unitsToCreated.Add("RocketRed", new UnitModel { UnitName = "RocketRed", UnitMovemtCredits = 4, UnitHitPoints = 10, UnitPower = 7, UnitDefence = 2, unitColorType = UnitModel.UnitColor.RED, unitType = UnitModel.UnitType.TANK, UnitShotDistance = 2 });
        unitsToCreated.Add("RocketBlue", new UnitModel { UnitName = "RocketBlue", UnitMovemtCredits = 4, UnitHitPoints = 10, UnitPower = 7, UnitDefence = 2, unitColorType = UnitModel.UnitColor.BLUE, unitType = UnitModel.UnitType.TANK, UnitShotDistance = 2 });
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

    //this is not DRY.
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
                case UnitModel.UnitType.TANK:
                    if (allUnitsInWorld[i].CurrentTile.TileType == "Water")
                    {
                        DestroyUnit(allUnitsInWorld[i]);
                        i = -1;
                    }
                break;
            }
        }
        CheckGameWon();
    }
    
    public void OrderStrike(TileModel tile,UnitModel selectedUnit)
    {
        if(tiles.Contains(tile))
        {
            UnitModel unit = tile.UnitOnTile;
            int hit= (selectedUnit.UnitPower - unit.UnitDefence - tile.TileDefnece) + 1;
            PlayShootSound(selectedUnit);
            if (hit<=0)
            {
                hit = 1;
            }
            unit.UnitHitPoints -= hit; 
            unit.CbHealthBar(unit);
            //for now destroyUnit
            if (unit.UnitHitPoints <= 0)
            {
                DestroyUnit(unit);
            }
            tiles.Clear();
            CheckGameWon();
        }
    }
    public void CheckGameWon()
    {
        int teamExist = 0;
        Color teamColor=new Color();
        string teamName="";
        if(UnitsLeft(UnitModel.UnitColor.RED)>=1)
        {
            teamExist++;
            teamColor = new Color(255, 0, 0);
            teamName = "Red Team Has Won";
        }
        if(UnitsLeft(UnitModel.UnitColor.BLUE) >= 1)
        {
            teamExist++;
            teamColor = new Color(0, 0, 255);
            teamName = "Blue Team Has Won";
        }
        if (teamExist==1)
        {
            turnController.GameWon(teamName,teamColor);
        }
    }

    public int UnitsLeft(UnitModel.UnitColor unitColor)
    {
        int unitsOnField = 0;
        for (int i = 0; i < allUnitsInWorld.Count; i++)
        {
            if(allUnitsInWorld[i].unitColorType==unitColor)
            {
                unitsOnField++;
            }
        }
        return unitsOnField;
    }

    public void MoveUnit(UnitModel unit, TileModel tile)
    {
        if (tiles.Contains(tile) && !unit.UnitHasMoved)
        {
            TileModel oldTile = WorldManger.Instance.World.getTile(unit.CurrentTile.X, unit.CurrentTile.Y);
            oldTile.UnitOnTile = null;
            tile.UnitOnTile = unit;
            unit.CurrentTile = tile;
            MoveUnitOnMap(unit);
         //   PlayWalkSound(unit);
            UnitDeselect();
            tiles.Clear();
            unit.UnitHasMoved = true;
            unit.CbUnitUpdateGraphics(unit);
            OpenMenus();
            unit.CbHealthBar(unit);
        }
    }

    void OpenMenus()
    {
        openUnitOrderMenu.SetActive(true);
        unitorderMenu.DisableEndTurn();
    }

    void PlayWalkSound(UnitModel unit)
    {
        switch(unit.unitType)
        {
            case UnitModel.UnitType.LAND:
                SoundManger.Instance.UnitSoundController.PlayUnitWalk();
                break;
            case UnitModel.UnitType.TANK:
                SoundManger.Instance.UnitSoundController.PlayTankWalk();
                break;
        }
    }

    void PlayShootSound(UnitModel unit)
    {
        switch (unit.unitType)
        {
            case UnitModel.UnitType.LAND:
                SoundManger.Instance.UnitSoundController.PlayUnitShoot();
                break;
            case UnitModel.UnitType.TANK:
                SoundManger.Instance.UnitSoundController.PlayTankShoot();
                break;
        }
    }

    void DestroyUnit(UnitModel unit)
    {
        DestroyUnitOutView(unit);
        TileModel tile = WorldManger.Instance.World.getTile(unit.CurrentTile.X, unit.CurrentTile.Y);
        tile.UnitOnTile = null;
        allUnitsInWorld.Remove(unit);
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
