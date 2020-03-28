using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class BuildBuilding
    {
        Tile tileLogic = new Tile();

        public BuildBuilding()
        {
            
        }

        //where string is the name of the building and the type of the class.
        public BuildingModel PlaceBuilding(Dictionary<string, Type> Buildings, TileModel tile,string ObjectName,Dictionary<string,BuildingModel> building)
        {
            BuildingModel obj=null;
            if (IsVaildPosistion(tile) == false)
            {
                //tile is not anvianable.
                return null;
            }
            if (Buildings.ContainsKey(ObjectName))
            {
                //factory
                Type type = Buildings[ObjectName];
                obj = Activator.CreateInstance(type) as BuildingModel;
                obj=obj.BuildProcess(building[ObjectName]);
                obj.Tile = tile;

                if (tileLogic.PlaceObject(obj, tile) == false)
                {
                    //show dialog obstruction
                    return null;
                }
            }
            return obj;
        }

        public bool IsVaildPosistion(TileModel t)
        {
            //make sure tile is floor
            //make sure tile doesn't have other object on it.
            if (t.TileType == "Water" )
            {
                return false;
            }
            if (t.Building != null)
            {
                return false;
            }
            return true;
        }

        public TileModel Delete(TileModel model)
        {
            if (model.Building != null)
            {

                tileLogic.DestroyObject(model);

            }
            return model;
        }
    }


