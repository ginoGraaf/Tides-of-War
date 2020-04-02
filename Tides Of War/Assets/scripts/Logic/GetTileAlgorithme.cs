using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTileAlgorithme 
{
    HashSet<TileModel> Tiles;
    public List<TileModel> TilesToMove(UnitModel unit)
    {
        Tiles = new HashSet<TileModel>();
        GetTiles(unit.CurrentTile, unit.UnitMovemtCredits);
        List<TileModel> tilesToReturn = new List<TileModel>();
        foreach(TileModel t in Tiles)
        {
            tilesToReturn.Add(t);
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
            Tiles.Add(northTile);
            north--;//change this later.
            GetTiles(northTile, north);
        }
        if (CheckTile(southTile) && south > 0)
        {
            Tiles.Add(southTile);
            south--;//change this later.
            GetTiles(southTile, south);
        }
        if (CheckTile(westTile) && west > 0)
        {
            Tiles.Add(westTile);
            west--;//change this later.
            GetTiles(westTile, west);
        }
        if (CheckTile(eastTile) && east > 0)
        {
            Tiles.Add(eastTile);
            east--;//change this later.
            GetTiles(eastTile, east);
        }
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
