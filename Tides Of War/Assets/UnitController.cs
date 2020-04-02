using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField]
    GameObject TargetPrefab;
    Dictionary<string, UnitModel> unitsToCreated = new Dictionary<string, UnitModel>();
    List<GameObject> Targets = new List<GameObject>();
    List<UnitModel> allUnitsInWorld = new List<UnitModel>();

    GetTileAlgorithme getTileAlgorithme;
    // Start is called before the first frame update
   public void InitUnits()
    {
        getTileAlgorithme = new GetTileAlgorithme();
        unitsToCreated.Add("InfantryRed", new UnitModel { UnitName = "InfantryRed", UnitMovemtCredits = 2, UnitHitPoints = 10, UnitPower = 3, UnitDefence = 1,unitColorType=UnitModel.UnitColor.RED });
        CreateTargets();
    }

    public void CreateTargets()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject targetobj = Instantiate(TargetPrefab) as GameObject;
            targetobj.SetActive(false);
            Targets.Add(targetobj);
        }
    }


    public void UnitSlected(UnitModel unit)
    {
        UnitDeselect();
        if (unit==null)
        {
            return;
        }
        List<TileModel> tiles = getTileAlgorithme.TilesToMove(unit);
        if(tiles!=null)
        {
            foreach(TileModel t in tiles)
            {
                for (int i = 0; i < Targets.Count; i++)
                {
                    if(Targets[i].activeInHierarchy==false)
                    {
                        Targets[i].transform.position = new Vector3(t.X, t.Y, -1);
                        Targets[i].SetActive(true);
                        break;
                    }
                }
            }
        }
    }

    public void UnitDeselect()
    {
        for (int i = 0; i < Targets.Count; i++)
        {
            Targets[i].SetActive(false);
        }
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
