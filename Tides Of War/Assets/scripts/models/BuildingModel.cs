using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public abstract partial class BuildingModel
    {
        public abstract BuildingModel BuildProcess(BuildingModel model);

        public Action<BuildingModel> cbBuildingOnChange;
        public Action<BuildingModel> cbBuildingDestroy;

        public int Width { get; set; }
        public int Height { get; set; }
        public string ObjectType { get; set; }
        public string ObjectSprite { get; set; }
        public TileModel Tile { get; set; }
        public bool IsBlocked { get; set; }

        public virtual void UpdateState(int newState)
        {

        }

        public virtual int GetStateInt()
        {
            return -1;
        }

        public virtual BuildingModel GetModel()
        {
            BasicBuilding basic = new BasicBuilding();
            return basic;
        }
    }

