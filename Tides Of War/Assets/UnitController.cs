using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    Dictionary<string, UnitModel> unitsToCreated = new Dictionary<string, UnitModel>();
    List<UnitModel> allUnitsInWorld = new List<UnitModel>(); 
    // Start is called before the first frame update
   public void InitUnits()
    {
        unitsToCreated.Add("InfantryRed", new UnitModel { UnitName = "InfantryRed", UnitMovemtCredits = 3, UnitHitPoints = 10, UnitPower = 3, UnitDefence = 1,unitColorType=UnitModel.UnitColor.RED });
    }


    public void UnitSlected(UnitModel unit)
    {
       
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
}
