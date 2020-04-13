using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseWaterLogic
{
    public void RiseWater(TileModel tileModel)
    {
        if (tileModel == null)
            return;
        if (tileModel.TileType == "LowLand")
        {
            if (NextToWaterTile(tileModel))
            {
                tileModel.TileType = "Water";
                tileModel.cbTileChanged(tileModel);
                RiseWater(WorldManger.Instance.World.getTile(tileModel.X, tileModel.Y + 1));
                RiseWater(WorldManger.Instance.World.getTile(tileModel.X, tileModel.Y - 1));
                RiseWater(WorldManger.Instance.World.getTile(tileModel.X+1, tileModel.Y));
                RiseWater(WorldManger.Instance.World.getTile(tileModel.X-1, tileModel.Y ));
            }
        }
    }

    public void LowerWater(TileModel tileModel)
    {
        //I hate to use sstirngs for this but it must be done.
        tileModel.TileType = "LowLand";
        tileModel.cbTileChanged(tileModel);
    }

    bool NextToWaterTile(TileModel model)
    {
     
       if (CheckTile(WorldManger.Instance.World.getTile(model.X, model.Y+1), "DeepWater") || CheckTile(WorldManger.Instance.World.getTile(model.X, model.Y+1), "Water"))
        {
            return true;
        }
        if (CheckTile(WorldManger.Instance.World.getTile(model.X , model.Y-1), "DeepWater") || CheckTile(WorldManger.Instance.World.getTile(model.X , model.Y-1), "Water"))
        {
            return true;
        }
        if (CheckTile(WorldManger.Instance.World.getTile(model.X + 1, model.Y), "DeepWater") || CheckTile(WorldManger.Instance.World.getTile(model.X + 1, model.Y), "Water"))
        {
            return true;
        }
        if (CheckTile(WorldManger.Instance.World.getTile(model.X-1, model.Y ),"DeepWater") || CheckTile(WorldManger.Instance.World.getTile(model.X - 1, model.Y), "Water"))
        {
            return true;
        }
        return false;
    }

    

    bool CheckTile(TileModel tile,string tileType)
    {
        if(tile!=null)
        {
            if (tile.TileType == tileType)
            {
                return true;
            }
        }
        return false;
    }

    void NextToPump()
    {

    }
}
