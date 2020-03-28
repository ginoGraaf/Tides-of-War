using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class InitBuildObjects : MonoBehaviour {

    Dictionary<string, BuildingModel> buildingObject;

    public static InitBuildObjects Instance { get; set; }

    public Dictionary<string, BuildingModel> BuildingObject { get { return buildingObject; } set { buildingObject = value; } }
    public Dictionary<string, Type> BuildingTypes = new Dictionary<string, Type>();
    //now I can make this so i can expande this for my game.

    void Start()
    {
        Instance = this;
   
        CreateBuildObjectsPrototype();
        InitBuildingsFactory();
    }

    public void CreateBuildObjectsPrototype()
    {
        buildingObject = new Dictionary<string, BuildingModel>();
        BuildingObject.Add("Cities", new BuildingModel.BasicBuilding { ObjectType = "Cities", ObjectSprite = "Cities", Width = 1, Height = 1, IsBlocked = false});

    }

    public void InitBuildingsFactory()
    {
        List<KeyValuePair<string, BuildingModel>> tempList = buildingObject.ToList();

        var buildingType = Assembly.GetAssembly(typeof(BuildingModel)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(BuildingModel)));

        for (int i = 0; i < tempList.Count; i++)
        {
            foreach (var type in buildingType)
            {
                var temp = Activator.CreateInstance(type) as BuildingModel;
                var b = tempList[i].Value.GetModel();
                if(temp.GetType() == b.GetType())
                {
                    BuildingTypes.Add(tempList[i].Key, type);
                    Debug.Log(temp.GetType()+" building "+b.GetType());
                    break;
                }
            }
        }
    }
}
