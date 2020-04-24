using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class TileModel
    {
        int x, y;
        public bool blocked = false;
        string tileType;
    public int TileDefnece { get; set; }
      //  public NodeT node;
        BuildingModel buildModel;
        public UnitModel UnitOnTile { get; set; }
        public int TileMovementCost { get; set; }
        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public bool Blocked { get { return blocked; } set { blocked = value; } }
        public BuildingModel Building { get { return buildModel; } set { buildModel = value; } }
        public bool OcupyTile { get; set; }
        public Action<TileModel> cbTileChanged;
    public Action<TileModel> CbTileWaterRise;
    public Action<TileModel> CbTileWaterLower;
        public string TileType
        {
            get { return tileType; }
            set
            {
                string oldType = tileType;
                tileType = value;
                if (cbTileChanged != null && oldType != TileType)
                {
                    cbTileChanged(this);
                }
            }
        }
    }

