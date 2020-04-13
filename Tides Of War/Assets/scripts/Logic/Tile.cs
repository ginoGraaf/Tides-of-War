using System;
using System.Collections;
using System.Collections.Generic;



    public class Tile
    {

        public bool DestroyObject(TileModel tile)
        {
            if (tile.Building == null)
            {
                return false;
            }
            BuildingModel buildingObj = tile.Building;

            for (int x_off = tile.X; x_off < (tile.X + buildingObj.Width); x_off++)
            {
                for (int y_Off = tile.Y; y_Off < (tile.Y + buildingObj.Height); y_Off++)
                {
                    TileModel selectedtile = WorldManger.Instance.World.getTile(x_off, y_Off);
                    selectedtile.Building = null;
                   // selectedtile.node.Col = false;
                }
            }
          //  tile.node.Col = false;
            return true;
        }

        public bool PlaceObject(BuildingModel objectInstance, TileModel tile)
        {
            TileModel selectedtile = tile;

            if (objectInstance == null)
            {
                tile.Building = null;
                return true;
            }

            for (int x_off = tile.X; x_off < (tile.X + objectInstance.Width); x_off++)
            {
                for (int y_Off = tile.Y; y_Off < (tile.Y + objectInstance.Height); y_Off++)
                {
                    selectedtile = WorldManger.Instance.World.getTile(x_off, y_Off);
                    if (selectedtile.Building != null || selectedtile.TileType == "Water")
                    {
                        return false;
                    }
                }
            }

            for (int x_off = tile.X; x_off < (tile.X + objectInstance.Width); x_off++)
            {
                for (int y_Off = tile.Y; y_Off < (tile.Y + objectInstance.Height); y_Off++)
                {
                    selectedtile = WorldManger.Instance.World.getTile(x_off, y_Off);
                    selectedtile.Building = objectInstance;
                   // selectedtile.node.Col = objectInstance.IsBlocked;
                }
            }

            return true;
        }


  
    }

