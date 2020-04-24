using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingView : MonoBehaviour
{
    [SerializeField]
    BuildingButton buildingButton;
    [SerializeField]
    List<TilesGraphic> tileGraphics;
    Dictionary<BuildingModel, GameObject> buildingsInWorld=new Dictionary<BuildingModel, GameObject>();
    Dictionary<string, Sprite> buildingSprite = new Dictionary<string, Sprite>();
    // Start is called before the first frame update
   public void SetUpBuildings()
    {
        LoadBuildingSprites();
        WorldManger.Instance.World.RegisterCreateBuilding(OnCreateBuilding);
    }

    void OnCreateBuilding(BuildingModel model)
    {
        GameObject building = new GameObject(model.ObjectType);
        building.transform.position = new Vector3(model.Tile.X, model.Tile.Y,-0.1f);
        building.AddComponent<SpriteRenderer>().sprite = buildingSprite[model.ObjectSprite];
        buildingsInWorld.Add(model, building);
    }

    void OnSpriteChange(BuildingModel model)
    {
        if (buildingsInWorld.ContainsKey(model) == false)
        {
            Debug.LogError("No such Building");
            return;
        }
        GameObject building = buildingsInWorld[model];
        building.GetComponent<SpriteRenderer>().sprite = buildingSprite[model.ObjectSprite];
    }

    void LoadBuildingSprites()
    {
        foreach(TilesGraphic t in tileGraphics)
        {
            for (int i = 0; i < t.TileSprite.Count; i++)
            {
                buildingSprite.Add(t.TileSprite[i].nameOfTile, t.TileSprite[i].TileImage);
                buildingButton.CreateButton(t.TileSprite[i]);
            }
        }
    }

    void DestroyGameObject(BuildingModel model)
    {
        if (buildingsInWorld.ContainsKey(model) == false)
        {
            // Debug.LogError("There is no building visual in this list ");
            return;
        }

        GameObject buildGameObject = buildingsInWorld[model];
        Destroy(buildGameObject);
        buildingsInWorld.Remove(model);
    }
}
