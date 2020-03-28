using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BuildingController :MonoBehaviour
{
    List<BuildingModel> buildings = new List<BuildingModel>();
    public List<BuildingModel> Buildings
    {
        get
        {
            return buildings;
        }

        set
        {
            buildings = value;
        }
    }

    void Updater()
    {
        for (int i = 0; i < buildings.Count; i++)
        {
       
            buildings[i].cbBuildingOnChange(buildings[i]);
        }
    }

    public void AddBuilding(BuildingModel building)
    {
        buildings.Add(building);
    }

    public void RemoveBuilding(BuildingModel building)
    {
        buildings.Remove(building);
    }

}
