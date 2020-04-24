using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTileAlgorithme 
{
    HashSet<TileModel> Tiles;
    UnitModel model;
    public enum UnitMove { MOVE,ATTACK};
    List<TileModel> tilesToReturn;
    UnitMove unitMoveType=UnitMove.MOVE;
    public List<TileModel> TilesToMove(UnitModel unit,UnitMove moveType)
    {
        model = unit;
        Tiles = new HashSet<TileModel>();

        tilesToReturn = new List<TileModel>();
        if (moveType == UnitMove.MOVE)
        {
            GetTiles(unit.CurrentTile, unit.UnitMovemtCredits);
            tilesToReturn.Add(unit.CurrentTile);
            foreach (TileModel t in Tiles)
            {
                tilesToReturn.Add(t);
            }
        }
        else if(moveType==UnitMove.ATTACK)
        {
            GetAttackRange(unit.CurrentTile, unit.UnitShotDistance, unit);
           
        }
        return tilesToReturn;
    }

    //recursion.
    public void GetTiles(TileModel tile,int movementCredits)
    {
        int north = movementCredits;
        int south = movementCredits;
        int west = movementCredits;
        int east = movementCredits;

        TileModel northTile = WorldManger.Instance.World.getTile(tile.X, tile.Y + 1);
        TileModel southTile = WorldManger.Instance.World.getTile(tile.X, tile.Y - 1);
        TileModel westTile = WorldManger.Instance.World.getTile(tile.X-1, tile.Y);
        TileModel eastTile = WorldManger.Instance.World.getTile(tile.X+1, tile.Y );
        
        if(CheckTile(northTile) && north>0)
        {
            north = ChangeMovemtBonus(model, north, northTile);
            GetTiles(northTile, north);
        }
        if (CheckTile(southTile) && south > 0)
        {
            south  = ChangeMovemtBonus(model, south, southTile);
            GetTiles(southTile, south);
        }
        if (CheckTile(westTile) && west > 0)
        {
        
            west = ChangeMovemtBonus(model, west, westTile);
            GetTiles(westTile, west);
        }
        if (CheckTile(eastTile) && east > 0)
        {
            east = ChangeMovemtBonus(model, east, eastTile);
            GetTiles(eastTile, east);
        }
    }

    void GetAttackRange(TileModel tile,int attackCredits,UnitModel unit)
    {
        int north = attackCredits;
        int south = attackCredits;
        int west = attackCredits;
        int east = attackCredits;

        TileModel northTile = WorldManger.Instance.World.getTile(tile.X, tile.Y + 1);
        TileModel southTile = WorldManger.Instance.World.getTile(tile.X, tile.Y - 1);
        TileModel westTile = WorldManger.Instance.World.getTile(tile.X - 1, tile.Y);
        TileModel eastTile = WorldManger.Instance.World.getTile(tile.X + 1, tile.Y);

        if(CheckTile(northTile) && north>=0)
        {
            if(CheckUnitOnTile(northTile,unit))
            {
                tilesToReturn.Add(northTile);
            }
            north--;
            GetAttackRange(northTile, north,unit);
        }

        if (CheckTile(southTile) && south >= 0)
        {
            if (CheckUnitOnTile(southTile, unit))
            {
                tilesToReturn.Add(southTile);
            }
            south--;
            GetAttackRange(southTile, south, unit);
        }

        if (CheckTile(westTile) && west >= 0)
        {
            if (CheckUnitOnTile(westTile, unit))
            {
                tilesToReturn.Add(westTile);
            }
            west--;
            GetAttackRange(westTile, west, unit);
        }

        if (CheckTile(eastTile) && east >= 0)
        {
            if (CheckUnitOnTile(eastTile, unit))
            {
                tilesToReturn.Add(eastTile);
            }
            east--;
            GetAttackRange(eastTile, east, unit);
        }

    }
     
    bool CheckUnitOnTile(TileModel tile,UnitModel unit)
    {
        if(tile.UnitOnTile==null)
        {
            return false;
        }
        if(tile.UnitOnTile.unitColorType!=unit.unitColorType)
        {
            return true;
        }
        return false;
    }

    int ChangeMovemtBonus(UnitModel model,int movementpointsLeft,TileModel tile)
    {
        switch(model.unitType)
        {
            case UnitModel.UnitType.LAND:
                if (tile.TileType == "Water" || tile.TileType=="DeepWater")
                {
                    movementpointsLeft = -1;
                }
                else
                {
                    if (tile.UnitOnTile == null)
                    {
                        Tiles.Add(tile);
                    }
                    movementpointsLeft-=tile.TileMovementCost;
                }
           
            break;
            case UnitModel.UnitType.WATER:
                break;
            case UnitModel.UnitType.TANK:
                if (tile.TileType == "Water" || tile.TileType == "DeepWater" || tile.TileType== "Mountains")
                {
                    movementpointsLeft = -1;
                }
                else
                {
                    if (tile.UnitOnTile == null)
                    {
                        Tiles.Add(tile);
                    }
                    movementpointsLeft--;
                }
                break;
        }
        return movementpointsLeft;
    }

    bool CheckTile(TileModel tile)
    {
        if(tile!=null)
        {
            return true;
        }
        return false;
    }


}
